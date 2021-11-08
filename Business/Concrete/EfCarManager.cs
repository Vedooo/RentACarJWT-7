using Business.Abstract;
using Core.Utilities.Business;
using Core.Utilities.Results;
using Core.Utilities.Results.ResultOptions.Options;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.Dto;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Business.Constants.Message;
using Core.Utilities.Results.DataResultOptions.DataOptions;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Business.BusinessAspects;

namespace Business.Concrete
{
    public class EfCarManager : ICarService
    {
        ICarDal _carDal;

        public EfCarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }


        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {
            _carDal.Add(car);
            return new SuccessResult(Messages.CarAdded);
        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.CarDeleted);
        }

        public IDataResult<List<Car>> GetAll()
        {
            if (DateTime.Now.Hour == 1)
            {
                return new ErrorDataResult<List<Car>>(Messages.MaintainanceTimeCar);
            }
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.CarsListed);

        }

        public IDataResult<List<Car>> GetByBrandId(int brandId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == brandId), Messages.CarsListedByBrandId);
        }

        public IDataResult<List<Car>> GetByCarId(int carId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.CarId == carId), Messages.CarsListedById);
        }

        public IDataResult<List<Car>> GetByColorId(int colorId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == colorId), Messages.CarsListedByColorId);
        }

        public IDataResult<List<Car>> GetByDailyPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => (c.DailyPrice >= min && c.DailyPrice <= max)), Messages.CarListedByDailyPrice);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetail()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarsDetails());
        }

        [ValidationAspect(typeof(CarValidator))]
        public IResult Update(Car car)
        {
            IResult result = BusinessRules.Run(CheckCarIdExists(car.CarId));
            if (result != null)
            {
                return new ErrorResult(Messages.CarIsAlreadyExist);
            }
            _carDal.Update(car);
            return new SuccessResult(Messages.CarInfoUpdated);
        }


        //////////////////// Business Rules //////////////////////////////


        private IResult CheckCarIdExists(int carId)
        {
            var result = _carDal.GetAll(c => c.CarId == carId).Count();
            if (result != null)
            {
                return new ErrorResult(Messages.CarIsAlreadyExist);
            }
            return new SuccessResult();
        }

    }
}
