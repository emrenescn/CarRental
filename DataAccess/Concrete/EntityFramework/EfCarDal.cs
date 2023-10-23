using Core.DataAccess.EntityFramework;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, CarDbContext>, ICarDal
    {
        public List<CarDetailsDto> GetCarDetails()
        {
            using (CarDbContext context = new CarDbContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands on c.BrandId equals b.BrandId
                             join co in context.Colors on c.ColorId equals co.ColorId
                             join i in context.CarImages on c.CarId equals i.CarId
                             select new CarDetailsDto
                             {
                                 CarId=c.CarId,
                                 ColorId = c.ColorId,
                                 BrandId = b.BrandId,
                                 BrandName = b.BrandName,
                                 ColorName = co.ColorName,
                                 DailyPrice = c.DailyPrice,
                                 ModelYear = c.ModelYear,
                                 Description = c.Description,
                                 ImagePath=i.ImagePath
                             };
                return result.ToList();
            }
        }
        public List<CarDetailsDto> GetCarDetailsByBrandId(int brandId)
        {
            using (CarDbContext context=new CarDbContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands on c.BrandId equals b.BrandId
                             join co in context.Colors on c.ColorId equals co.ColorId
                             where b.BrandId== brandId
                             select new CarDetailsDto
                             {
                                 ColorId=co.ColorId,
                                 BrandId = b.BrandId,
                                 BrandName= b.BrandName,
                                 DailyPrice= c.DailyPrice,
                                 Description = c.Description,
                                 ModelYear= c.ModelYear,
                                 ColorName=co.ColorName
                                 
                             };
                return result.ToList();
            }
        }
        public List<CarDetailsDto> GetCarDetailsByColorId(int colorId)
        {
            using (CarDbContext context=new CarDbContext())
            {
                var result = from c in context.Cars
                             join co in context.Colors on c.ColorId equals co.ColorId
                             join b in context.Brands on c.BrandId equals b.BrandId
                             where co.ColorId== colorId
                             select new CarDetailsDto
                             {
                                 BrandId = b.BrandId,
                                 ColorId= co.ColorId,
                                 ColorName=co.ColorName,
                                 BrandName=b.BrandName,
                                 DailyPrice = c.DailyPrice,
                                 Description = c.Description,
                                 ModelYear = c.ModelYear,

                             };
                return result.ToList();
            }
        }
        public List<CarDetailsDto> GetCarDetailsByCarId(int carId)
        {
            using (CarDbContext context = new CarDbContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands on c.BrandId equals b.BrandId
                             join co in context.Colors on c.ColorId equals co.ColorId
                             join i in context.CarImages on c.CarId equals i.CarId
                             where c.CarId == carId
                             select new CarDetailsDto
                             {
                                 CarId = c.CarId,
                                 ColorId = co.ColorId,
                                 BrandId = b.BrandId,
                                 BrandName = b.BrandName,
                                 DailyPrice = c.DailyPrice,
                                 Description = c.Description,
                                 ModelYear = c.ModelYear,
                                 ColorName = co.ColorName,
                                 ImagePath=i.ImagePath
                             };
                return result.ToList();
            }
        }
        public List<CarNameListDto> GetCarNameList()
        {
            using (CarDbContext context=new CarDbContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands on c.BrandId equals b.BrandId
                             select new CarNameListDto
                             {
                                 CarId = c.CarId,
                                 BrandName = b.BrandName,
                                 Description = c.Description
                    
                            };   
                return result.ToList(); 
            }
             
        }
        public List<CarDetailsDto> GetCarByBrandIdAndColorId(int brandId,int colorId)
        {
            using(CarDbContext context=new CarDbContext()) {
                var result = from c in context.Cars
                             join b in context.Brands on c.BrandId equals b.BrandId
                             join co in context.Colors on c.ColorId equals co.ColorId
                             where c.BrandId == brandId && c.ColorId == colorId
                             select new CarDetailsDto
                             {
                                 CarId = c.CarId,
                                 BrandId = b.BrandId,
                                 ColorId = co.ColorId,
                                 BrandName = b.BrandName,
                                 ColorName = co.ColorName,
                                 DailyPrice = c.DailyPrice,
                                 Description = c.Description,
                                 ModelYear = c.ModelYear
                             };
                return result.ToList();
            }
        }
        public List<CarByImageDto>GetCarImagesByCarId(int carId)
        {
            using(CarDbContext context=new CarDbContext())
            {
                var result = from c in context.Cars
                             join ci in context.CarImages on c.CarId equals ci.CarId
                             where c.CarId == carId
                             select new CarByImageDto
                             {
                                 CarId=c.CarId,
                                 ImagePath=ci.ImagePath,
                                 Id=ci.Id
                             };
                return result.ToList();
            }
        }
    }
}
