using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
   
    public class ColorManager : IColorService
    {
        IColorDal _colorDal;
        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }
        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Color color)
        {
            return new SuccessResult(Messages.ColorAdded);
            _colorDal.Add(color);
        }

        public IResult Delete(Color color)
        {
            return new SuccessResult(Messages.ColorDeleted);
            _colorDal.Delete(color);
        }

        public IDataResult<List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll());
        }

        public IDataResult<List<Color>> GetAllByColorId(int colorId)
        {
            var result = _colorDal.GetAll(c=>c.ColorId==colorId).ToList();
            return new SuccessDataResult<List<Color>>(result);
        }

        public IResult Update(Color color)
        {
            return new SuccessResult(Messages.ColorUpdated);
            _colorDal.Update(color);    
        }
    }
}
