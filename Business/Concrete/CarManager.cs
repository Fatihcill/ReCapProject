using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _iCarDal;

        public CarManager(ICarDal iCarDal)
        {
            _iCarDal = iCarDal;
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int brandId)
        {
            return new SuccessDataResult<List<Car>>(_iCarDal.GetAll(p => p.BrandId == brandId));
        }

        public IDataResult<Car> GetById(int carId)
        {
            return new SuccessDataResult<Car>(_iCarDal.Get(c => c.CarId == carId), Messages.CarsListed);
        }

        public IDataResult<CarDetailDto> GetDetailById(int carId)
        {
            return new SuccessDataResult<CarDetailDto>(_iCarDal.GetCarDetailById(c => c.CarId == carId));
        }

        [SecuredOperation("car.add,admin")]
        //[ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {
            _iCarDal.Add(car);
            return new SuccessResult(Messages.CarAdded);

        }

        public IResult Delete(Car car)
        {
            _iCarDal.Delete(car);
            return new SuccessResult(Messages.CarDeleted);
        }

        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_iCarDal.GetAll(), Messages.CarsListed);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetail()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_iCarDal.GetCarDetails());
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_iCarDal.GetCarDetails());
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailsByBrand(int brandId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_iCarDal.GetCarDetails(c => c.BrandId == brandId));
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailsByColor(int colorId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_iCarDal.GetCarDetails(c => c.ColorId == colorId));

        }

        public IDataResult<List<CarDetailDto>> GetCarDetailsByColorAndByBrand(int colorId, int brandId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_iCarDal.GetCarDetails(c => c.ColorId == colorId && c.BrandId == brandId));

        }

        public IDataResult<List<CarDetailDto>> GetCarDetailsByCar(int carId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_iCarDal.GetCarDetails(c => c.CarId == carId));
        }

        public IDataResult<CarDetailDto> GetCarDetailById(int id)
        {
            return new SuccessDataResult<CarDetailDto>(_iCarDal.GetCarDetailById(c => c.CarId == id));
        }

        [CacheAspect]
        public IDataResult<List<Car>> GetCarsByColorId(int colorId)
        {
            return new SuccessDataResult<List<Car>>(_iCarDal.GetAll(c => c.ColorId == colorId), Messages.CarsListed);
        }

        public IResult TransactionalOperation(Car car)
        {
            _iCarDal.Update(car);
            _iCarDal.Add(car);
            return new SuccessResult(Messages.CarUpdated);
        }

        public IResult Update(Car car)
        {
            if (car.Description.Length <= 2)
            {
                return new ErrorResult(Messages.CarDescriptionInvalid);

            }
            else if (car.DailyPrice <= 1)
            {
                return new ErrorResult(Messages.CarPriceInvalid);

            }
            else
            {
                _iCarDal.Update(car);
                return new SuccessResult(Messages.CarAdded);

            }
        }

        public IResult TransactionalTest(Car car)
        {
            _iCarDal.Update(car);
            _iCarDal.Add(car);
            return new SuccessResult(Messages.CarAdded);
        }
    }
}
