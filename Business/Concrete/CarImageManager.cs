using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Business.BusinessAspects.Autofac;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.FileHelper;
using Entities.DTOs;

namespace Business.Concrete
{
  public class CarImageManager : ICarImageService
    {
        private readonly ICarImageDal _carImageDal;
        private ICarService _carService;
        public CarImageManager(ICarImageDal carImageDal, ICarService carService)
        {
            _carImageDal = carImageDal;
            _carService = carService;
        }

        [CacheAspect]
        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<CarImage> Get(int id)
        {
            var carImage = _carImageDal.Get(ci => ci.CarImageId == id);
            if (carImage == null) return new ErrorDataResult<CarImage>();
            return new SuccessDataResult<CarImage>(carImage);
        }

        public IDataResult<List<CarDetailDto>> GetByCarId(int carId)
        {
            var result = _carService.GetCarDetails(ci => ci.CarId == carId);
            if (result.Data.Any()) return new SuccessDataResult<List<CarDetailDto>>(result.Data);
            return new SuccessDataResult<List<CarDetailDto>>(new List<CarDetailDto>
            {
                new CarDetailDto{ ImagePath = Environment.CurrentDirectory + @"\wwwroot\Images\default.png",WebImagePath = "/Images/default.png", Date = DateTime.Now }
            });
        }

       // [SecuredOperation("CarImage.Add")]
        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Add(IFormFile file,CarImage carImage)
        {
            var result = BusinessRules.Run(CheckCarImagesCount(carImage.CarId));
            if (result != null) return result;
            var img = FileHelper.SaveImageFile("Images", file);
            carImage.ImagePath = img.Item1;
            carImage.WebImagePath = img.Item2;
            carImage.Date = DateTime.Now;
            _carImageDal.Add(carImage);
            return new SuccessResult(Messages.CarImageAdded);
        }

        [ValidationAspect(typeof(CarImageValidator))]
        [SecuredOperation("CarImage.Update")]
        public IResult Update(IFormFile file, CarImage carImage)
        {
            var entity = _carImageDal.Get(ci => ci.CarImageId == carImage.CarImageId);
            if (entity == null) return new ErrorResult();
            FileHelper.DeleteImageFile(entity.ImagePath);
            var img = FileHelper.SaveImageFile("Images", file); //return local and web paths as tuple
            entity.ImagePath = img.Item1; 
            entity.WebImagePath = img.Item2;
            entity.Date = DateTime.Now;
            _carImageDal.Update(entity);
            return new SuccessResult(Messages.CarImageUpdated);
        }

        [SecuredOperation("CarImage.Delete")]
        public IResult Delete(CarImage carImage)
        {
            var entity = _carImageDal.Get(ci => ci.CarImageId == carImage.CarImageId);
            if (entity == null) return new ErrorResult(Messages.CarImageDeleted);
            FileHelper.DeleteImageFile(entity.ImagePath);
            _carImageDal.Delete(carImage);
            return new SuccessResult(Messages.CarImageDeleted);
        }
        private IResult CheckCarImagesCount(int carId)
        {
            var result = _carImageDal.GetAll(ci => ci.CarId == carId).Count < 5;
            if (!result) return new ErrorResult(Messages.CarImageLimitExceeded);
            return new SuccessResult();
        }
    }
}