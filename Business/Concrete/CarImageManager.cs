using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.FileHelper;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        private ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }



        public IDataResult<List<CarImage>> GetByCarId(int carId)
        {
            var getListByCarId = _carImageDal.GetAll(car => car.CarId == carId);

            if (getListByCarId.Count > 0)
                return new SuccessDataResult<List<CarImage>>(getListByCarId);

            var path = Environment.CurrentDirectory + @"\wwwroot\Images\default.png";
            var defaultImage = new List<CarImage> { new CarImage { ImagePath = path } };
            return new SuccessDataResult<List<CarImage>>(defaultImage);
        }


        public IResult Add(IFormFile file,CarImage carImage)
        {
            var result = BusinessRules.Run(CheckCarImageCount(carImage.CarId));

            if (result != null)
            {
                return result;
            }

            carImage.Date = DateTime.Now;
            carImage.ImagePath = FileHelper.AddFile(file);
            _carImageDal.Add(carImage);
            return new SuccessResult(Messages.CarImageAdded);
        }


        [SecuredOperation("admin,carimage.delete")]
        [CacheRemoveAspect("ICarImageService.Get")]
        public IResult Delete(CarImage carImage)
        {
            var image = _carImageDal.Get(c => c.CarImageId == carImage.CarImageId);

            if (image == null)
            {
                return new ErrorResult(Messages.CarImageNotFound);
            }

            FileHelper.DeleteFile(image.ImagePath);

            _carImageDal.Delete(carImage);

            return new SuccessResult(Messages.CarImageDeleted);
        }


        [SecuredOperation("admin,carimage.update")]
        [ValidationAspect(typeof(CarImageValidator))]
        [CacheRemoveAspect("ICarImageService.Get")]
        public IResult Update(IFormFile file,CarImage carImage)
        {
            var oldImage = _carImageDal.Get(c => c.CarImageId == carImage.CarImageId);

            if (oldImage == null)
            {
                return new ErrorResult(Messages.CarImageNotFound);
            }

            carImage.Date = DateTime.Now;
            carImage.ImagePath = FileHelper.UpdateFile(file, oldImage.ImagePath);

            _carImageDal.Update(carImage);

            return new SuccessResult(Messages.CarImageUpdated);
        }

        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }




        // Business Rules Methods
        private IResult CheckCarImageCount(int carId)
        {
            if (_carImageDal.GetAll(ci => ci.CarId == carId).Count >= 5)
            {
                return new ErrorResult(Messages.CarImageLimitExceeded);
            }

            return new SuccessResult();
        }
    }
}