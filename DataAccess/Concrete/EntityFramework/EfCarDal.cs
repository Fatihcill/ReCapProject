using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, RentACarContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails()
        {
            using (RentACarContext context=new RentACarContext())
            {
                var result = from c in context.Cars
                    join cl in context.Colors on c.ColorId equals cl.ColorId
                    join b in context.Brands on c.BrandId equals b.BrandId
                    select new CarDetailDto()
                    {
                        CarId = c.CarId,
                        Descriptions = c.Description,
                        DailyPrice = c.DailyPrice,
                        BrandName = b.BrandName,
                        ColorName = cl.ColorName
                    };
                return result.ToList();
            }
        }
    }
}