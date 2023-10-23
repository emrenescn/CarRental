using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, CarDbContext>, IRentalDal
    {
        public List<RentalDetailsDto> GetRentalDetails()
        {
            using (CarDbContext context=new CarDbContext())
            {
                var result = from r in context.Rentals
                             join c in context.Customers on r.CustomerId equals c.CustomerId
                             join u in context.Users on c.UserId equals u.Id
                             join b in context.Brands on r.CarId equals b.BrandId
                             select new RentalDetailsDto
                             {
                                 RentDate = r.RentDate,
                                 ReturnDate = r.ReturnDate,
                                 BrandName = b.BrandName,
                                 Id = r.Id,
                                 FirstNameAndLastName = u.FirstName + " " + u.LastName
                             };
                return result.ToList();
            }
        }
    }
}
