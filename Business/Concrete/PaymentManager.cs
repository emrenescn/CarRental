using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PaymentManager : IPaymentService
    {
        IPaymentDal _paymentDal;
        ICustomerService _customerService;
        public PaymentManager(IPaymentDal paymentDal,ICustomerService customerService)
        {
            _paymentDal = paymentDal;
            _customerService = customerService;
        }
        [ValidationAspect(typeof(PaymentValidator))]
        public IResult Add(Payment payment)
        {
            var result = BusinessRules.Run(CheckPaymentAdd(payment));
            if (result != null)
            {
                return result;
            }
            _paymentDal.Add(payment);
            return new SuccessResult(Messages.Succes);
        }

        public IResult Delete(Payment payment)
        {
            _paymentDal.Delete(payment);
            return new SuccessResult(Messages.Succes);
        }

        public IDataResult<List<Payment>> GetAll()
        {
            var result=_paymentDal.GetAll();
            return new SuccessDataResult<List<Payment>>(result);
        }

        public IDataResult<List<Payment>> GetPaymentsByCustomerId(int customerId)
        {
            var result=_paymentDal.GetAll(c=>c.CustomerId== customerId);
            return new SuccessDataResult<List<Payment>>(result);
        }

        public IResult Update(Payment payment)
        {
            _paymentDal.Update(payment);
            return new SuccessResult(Messages.Succes);
        }
        private IResult CheckPaymentAdd(Payment payment)
        {
            if (payment.CardNumber.Length != 16 ||(payment.CVV >= 1000 || payment.CVV <= 99)||payment.Year<2013||(payment.Mount<0||payment.Mount>12))
            {
                return new ErrorResult("enter the form information correctly");
            }
            return new SuccessResult();
           
        }
    }
}
