using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, RentACarContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails(Expression<Func<Car, bool>> filter = null)
        {

            CarImage defaultVal = new CarImage
            {
                ImagePath = Environment.CurrentDirectory + @"\wwwroot\Images\default.png",
                WebImagePath = "/Images/default.png",
            };
            using (RentACarContext context = new RentACarContext())
            {
                var result = (from car in filter == null ? context.Cars : context.Cars.Where(filter)
                              join c in context.Colors on car.ColorId equals c.ColorId
                              join d in context.Brands on car.BrandId equals d.BrandId
                              select new CarDetailDto
                              {
                                  BrandId = d.BrandId,
                                  BrandName = d.BrandName,
                                  ColorId = c.ColorId,
                                  ColorName = c.ColorName,
                                  DailyPrice = car.DailyPrice,
                                  Description = car.Description,
                                  ModelYear = car.ModelYear,
                                  CarId = car.CarId,

                                  Date = (from carImage in context.CarImages
                                          where (carImage.CarId == car.CarId)
                                          select carImage).FirstOr(defaultVal).Date,
                                  ImagePath = (from carImage in context.CarImages
                                               where (carImage.CarId == car.CarId)
                                               select carImage).FirstOr(defaultVal).ImagePath,
                                  WebImagePath = (from carImage in context.CarImages
                                                  where (carImage.CarId == car.CarId)
                                                  select carImage).FirstOr(defaultVal).WebImagePath,
                                  ImageId = (from carImage in context.CarImages
                                             where (carImage.CarId == car.CarId)
                                             select carImage).FirstOr(defaultVal).CarImageId,

                              }).ToList();
                return result.GroupBy(p => p.CarId).Select(p => p.FirstOrDefault()).ToList();
            }
        }

        public List<CarDetailDto> GetCarDetailById(int carId)
        {
            CarImage defaultVal = new CarImage
            {
                ImagePath = Environment.CurrentDirectory + @"\wwwroot\Images\default.png",
                WebImagePath = "/Images/default.png",
            };
            using (RentACarContext context = new RentACarContext())
            {
                var result = from car in context.Cars
                             join c in context.Colors on car.ColorId equals c.ColorId
                             join d in context.Brands on car.BrandId equals d.BrandId
                             where car.CarId == carId
                             select new CarDetailDto
                             {
                                 BrandId = d.BrandId,
                                 BrandName = d.BrandName,
                                 ColorId = c.ColorId,
                                 ColorName = c.ColorName,
                                 DailyPrice = car.DailyPrice,
                                 Description = car.Description,
                                 ModelYear = car.ModelYear,
                                 CarId = car.CarId,
                                 Date = (from carImage in context.CarImages
                                         where (carImage.CarId == car.CarId)
                                         select carImage).FirstOr(defaultVal).Date,
                                 ImagePath = (from carImage in context.CarImages
                                              where (carImage.CarId == car.CarId)
                                              select carImage).FirstOr(defaultVal).ImagePath,
                                 WebImagePath = (from carImage in context.CarImages
                                                 where (carImage.CarId == car.CarId)
                                                 select carImage).FirstOr(defaultVal).WebImagePath,
                                 ImageId = (from carImage in context.CarImages
                                            where (carImage.CarId == car.CarId)
                                            select carImage).FirstOr(defaultVal).CarImageId,
                             };
                return result.ToList();
            }
        }

        public List<CarDetailDto> GetCarDetailsByBrandAndColor(int brandId, int colorId)
        {
            CarImage defaultVal = new CarImage
            {
                ImagePath = Environment.CurrentDirectory + @"\wwwroot\Images\default.png",
                WebImagePath = "/Images/default.png",
            };
            using (RentACarContext context = new RentACarContext())
            {
                var result = from car in context.Cars.Where
                        (car => car.BrandId == brandId && car.ColorId == colorId)
                             join brand in context.Brands on car.BrandId equals brand.BrandId
                             join color in context.Colors on car.ColorId equals color.ColorId

                             select new CarDetailDto
                             {
                                 CarId = car.CarId,
                                 BrandName = brand.BrandName,
                                 ColorName = color.ColorName,
                                 DailyPrice = car.DailyPrice,
                                 ModelYear = car.ModelYear,
                                 ImagePath = (from carImage in context.CarImages
                                     where (carImage.CarId == car.CarId)
                                     select carImage).FirstOr(defaultVal).ImagePath,
                                 WebImagePath = (from carImage in context.CarImages
                                     where (carImage.CarId == car.CarId)
                                     select carImage).FirstOr(defaultVal).WebImagePath,
                             };
                return result.ToList();
            }
        }

    }
}