using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICarDal:IEntityRepository<Car>
    {
        List<CarDetailsDto> GetCarDetails();
        List<CarDetailsDto> GetCarDetailsByBrandId(int brandId);
        List<CarDetailsDto> GetCarDetailsByColorId(int colorId);
        List<CarDetailsDto> GetCarDetailsByCarId(int carId);
        List<CarNameListDto> GetCarNameList();
        List<CarDetailsDto> GetCarByBrandIdAndColorId(int brandId, int colorId);
        List<CarByImageDto> GetCarImagesByCarId(int carId);
    }
}
