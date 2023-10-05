using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        public IResult Add(IFormFile file, CarImage carImage);
        public IResult Delete(CarImage carImage);
        public IResult Update(IFormFile file, CarImage carImage);
        public IDataResult<List<CarImage>> GetAll();
        public IDataResult<CarImage> GetByImageId(int imageId);
        public IDataResult <List<CarImage>> GetAllByCarId(int carId);
    }
}
