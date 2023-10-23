using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;
        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }
        [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(Rental rental)
        {
            _rentalDal.Add(rental);
            return new SuccessResult(Messages.RentAdded);
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.RentDeleted);
            
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());    
        }

        public IDataResult<Rental> GetById(int Id)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(r=>r.Id==Id));
        }

        public IDataResult<List<RentalDetailsDto>> GetRentalDetails()
        {
            return new SuccessDataResult<List<RentalDetailsDto>>(_rentalDal.GetRentalDetails());
        }
        [ValidationAspect(typeof(RentalValidator))]
        public IResult RulesForAdding(Rental rental)
        {
            var result=BusinessRules.Run(CheckCarRentDateAndReturnDate(rental),CheckIfReturnDateIsBeforeRentDate(rental.ReturnDate,rental.RentDate), CheckIfThisCarIsAlreadyRentedBySelectedDateRange(rental));
            if (result != null)
            {
                return result;
            }
            _rentalDal.Add(rental);
            return new SuccessResult();
        }

        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.RentUpdated);
        }
        private IResult CheckIfThisCarIsAlreadyRentedBySelectedDateRange(Rental rental)
        {
            var result = _rentalDal.Get(r => r.CarId == rental.CarId && (r.RentDate == rental.RentDate
            || (r.RentDate < rental.RentDate && (r.ReturnDate == null || (DateTime)r.ReturnDate.Date>r.RentDate )))); 
            if(result!=null)
            {
                return new ErrorResult(Messages.Error);
            }
            return new SuccessResult();
        }
        private IResult CheckCarRentDateAndReturnDate(Rental rental)
        {
            var result = _rentalDal.Get(r => r.CarId == rental.CarId && r.ReturnDate == null);
            if (result!=null)
            {
                if(rental.ReturnDate>result.RentDate)
                {
                    return new ErrorResult(Messages.Error);
                }
            }
            return new SuccessResult();
        }
        private IResult CheckIfReturnDateIsBeforeRentDate(DateTime returnDate,DateTime rentDate)
        {
            if (returnDate < rentDate)
            {
                return new ErrorResult(Messages.Error);
            }
            return new SuccessResult();
        }
    }
}
