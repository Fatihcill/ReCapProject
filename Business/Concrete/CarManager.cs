using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using Entities.DTOs;
using FluentValidation;

namespace Business.Concrete
{
       public class CarManager : ICarService
    {
        private readonly ICarDal _carDal;
        private ICarImageDal _carImageDal;
        public CarManager(ICarDal carDal, ICarImageDal carImageDal)
        {
            _carDal = carDal;
            _carImageDal = carImageDal;
        }

        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
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

        //[CacheAspect]
        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.CarsListed);
        }

        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<Car> GetById(int carId)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.CarId == carId));
        }

        //[CacheAspect]
        public IDataResult<List<CarDetailDto>> GetCarDetails(Expression<Func<Car,bool>> filter = null)
        {
                return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails());
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailsByBrandAndColor(int brandId, int colorId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetailsByBrandAndColor(brandId, colorId));
        }
        public IDataResult<List<CarDetailDto>> GetCarDetailsById(int carId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetailById(carId));
        }

        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetCarBrandandColor(int brandId, int colorId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(p=>p.BrandId==brandId && p.ColorId==colorId));
        }

        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetCarsDetailByBrandId(int brandId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(p=>p.BrandId==brandId));
        }

        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetCarsDetailByColorId(int colorId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(p => p.ColorId == colorId));
        }


        //[SecuredOperation("Car.Update")]
        public IResult Update(Car car)
        {
            if (car.DailyPrice > 0)
            {
                _carDal.Update(car);
                return new SuccessResult(Messages.CarUpdated);
            }
            _carDal.Update(car);
            return new SuccessResult(Messages.CarPriceInvalid);
        }
    }
}