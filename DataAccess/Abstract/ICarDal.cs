using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Core.DataAccess;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface ICarDal:IEntityRepository<Car>
    {
        List<CarDetailDto> GetCarDetails(Expression<Func<Car, bool>> filter = null);
        List<CarDetailDto> GetCarDetailById(int carId);
        List<CarDetailDto> GetCarDetailsByBrandAndColor(int brandId, int colorId);
    }
}
