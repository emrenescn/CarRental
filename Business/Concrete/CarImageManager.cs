using Business.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using DataAccess.Abstract;
using Core.Utilities.Helper.FileHelper;
using Microsoft.AspNetCore.Http;
using Business.Constants;
using Core.Utilities.Business;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;
        IFileService _fileService;
        public CarImageManager(ICarImageDal carImageDal,IFileService fileService)
        {
            _carImageDal = carImageDal;
            _fileService = fileService;
        }
        public IResult Add(IFormFile file,CarImage carImage)
        {
            IResult result=BusinessRules.Run(CheckCarImageLimit(carImage.CarId));
            if (result != null)
            {
                return result;
            }
            //_fileService.UploadFile(file, CarImagePathConstants.imagePath);
            carImage.ImageDate = DateTime.Now;
            carImage.ImagePath =@"Uploads/CarImages"+ _fileService.UploadFile(file, CarImagePathConstants.imagePath);
            _carImageDal.Add(carImage);
            return new SuccessResult("Image uploaded succesfully");
        }

        public IResult Delete(CarImage carImage)
        {
            _fileService.Delete(CarImagePathConstants.imagePath+carImage);
            _carImageDal.Delete(carImage);
            return new SuccessResult("Image deleted succesfully");
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

        public IDataResult<List<CarImage>> GetAllByCarId(int carId)
        {
            var result=BusinessRules.Run(CheckCarImageCount(carId));
            if (result != null)
            {
                return new ErrorDataResult<List<CarImage>>(GetDefaultCarImage(carId).Data);
            }
           return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(c=>c.CarId==carId));
        }

        public IDataResult<CarImage> GetByImageId(int imageId)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(c=>c.Id==imageId));
        }

        public IResult Update(IFormFile file,CarImage carImage)
        {
            var path = carImage;
            _fileService.Update(file,CarImagePathConstants.imagePath+path, CarImagePathConstants.imagePath);
            _carImageDal.Update(carImage);
            return new SuccessResult();
        }
        private IResult CheckCarImageLimit(int carId)
        {
            var result =_carImageDal.GetAll(c=>c.CarId==carId).Count;
            if (result > 5)
            {
                return new ErrorResult("A car can have five pictures");
            }
            return new SuccessResult();
        }
        private IDataResult<List<CarImage>> GetDefaultCarImage(int carId)
        {
            List<CarImage> defaultImage=new List<CarImage>();
            defaultImage.Add(new CarImage { CarId = carId,ImageDate=DateTime.Now,Id=1});
            return new SuccessDataResult<List<CarImage>>(defaultImage);
        }
        private IResult CheckCarImageCount(int carId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId).Count;
            if (result > 0)
            {
                return new SuccessResult();
            }
            return new ErrorResult(); 
        }
    }
}
