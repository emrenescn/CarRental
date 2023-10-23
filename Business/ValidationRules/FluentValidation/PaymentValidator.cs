using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class PaymentValidator:AbstractValidator<Payment>
    {
        public PaymentValidator()
        {
            RuleFor(p => p.CardNumber).NotEmpty();
            RuleFor(p => p.CardNumber).Length(16);
            RuleFor(p => p.CVV).InclusiveBetween(99, 999);
            RuleFor(p => p.CVV).NotEmpty();
            RuleFor(p => p.Mount).InclusiveBetween(1, 12);
            RuleFor(p=>p.Mount).NotEmpty();
            RuleFor(p=>p.Year).NotEmpty();
            RuleFor(p => p.Year).GreaterThan(2013);
        }
    }
}
