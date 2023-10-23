using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarService
    {
        IDataResult<List<Car>> GetAll();
        IDataResult<List<CarDetailsDto>> GetCarDetailByColorId(int colorId);
        IDataResult<List<CarDetailsDto>> GetCarDetailByBrandId(int brandId);
        IDataResult<List<CarDetailsDto>> GetCarDetailByCarId(int carId);
        IDataResult<List<CarDetailsDto>> GetCarDetails();
        IDataResult<List<CarNameListDto>> GetCarNameList();
        IDataResult<List<CarDetailsDto>>GetCarByBrandIdAndColorId(int brandId, int colorId);
        IDataResult<List<CarByImageDto>> GetCarImagesByCarId(int carId);
        IResult Add(Car car);
        IResult Delete(Car car);
        IResult Update(Car car);
        IResult AddTransactionalTest(Car car);
    }
}
