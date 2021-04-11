using System.Linq;
using Core.DataAccess.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car,RentACarContext>,ICarDal
    {


        public List<CarDetailDto> getCarDetail()
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result = from ca in context.Cars
                    join b in context.Brands on ca.BrandId equals b.BrandId
                    join co in context.Colors on ca.ColorId equals co.ColorId
                    select new CarDetailDto
                        { BrandName = b.BrandName, ColorName = co.ColorName, DailyPrice = ca.DailyPrice,CarId = ca.CarId,ModelYear = ca.ModelYear,BrandId = ca.BrandId,CarFindexPoint = ca.CarFindexPoint};

                return result.ToList();
            }
        }

      

        public List<CarDetailDto> GetCarDetails(Expression<Func<Car, bool>> filter = null)
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result = from c in filter is null ? context.Cars : context.Cars.Where(filter)
                    join b in context.Brands
                        on c.BrandId equals b.BrandId
                    join co in context.Colors
                        on c.ColorId equals co.ColorId
                        let image = (from carImage in context.CarImages where c.CarId == carImage.CarId select carImage.ImagePath)
                    select new CarDetailDto
                    {
                        CarId = c.CarId,
                        BrandName = b.BrandName,
                        Description = c.Description,
                        ModelYear = c.ModelYear,
                        ColorName = co.ColorName,
                        DailyPrice = c.DailyPrice,
                        BrandId = c.BrandId,
                        ColorId = c.ColorId,
                        CarFindexPoint=c.CarFindexPoint,
                        ImagePath = image.Any() ? image.FirstOrDefault() : new CarImage { ImagePath = Environment.CurrentDirectory + @"\wwwroot\Images\default.png" }.ImagePath
                        
                    };

                return result.ToList();
            }
        }

        public CarDetailDto GetCarDetailById(Expression<Func<Car, bool>> filter)
        {
            using(RentACarContext context = new RentACarContext())
            {
                var result = from c in filter is null ? context.Cars : context.Cars.Where(filter)
                    join b in context.Brands
                        on c.BrandId equals b.BrandId
                    join co in context.Colors
                        on c.ColorId equals co.ColorId
                    let image = (from carImage in context.CarImages where c.CarId == carImage.CarId select carImage.ImagePath)
                    select new CarDetailDto
                    {
                        CarId = c.CarId,
                        BrandName = b.BrandName,
                        Description = c.Description,
                        ModelYear = c.ModelYear,
                        ColorName = co.ColorName,
                        DailyPrice = c.DailyPrice,
                        ColorId = c.ColorId,
                        BrandId = c.BrandId,
                        CarFindexPoint = c.CarFindexPoint,
                        ImagePath = image.FirstOrDefault()
                    };

                return result.FirstOrDefault();
            }
        }
    }
}
