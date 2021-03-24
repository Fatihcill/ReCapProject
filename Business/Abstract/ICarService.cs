using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Core.Utilities.Results;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface ICarService : IService<Car>
    {


        IDataResult<List<CarDetailDto>> GetCarDetails(Expression<Func<Car, bool>> filter = null);
        IDataResult<List<CarDetailDto>> GetCarDetailsById(int carId);
        IDataResult<List<CarDetailDto>> GetCarsDetailByBrandId(int brandId);
        IDataResult<List<CarDetailDto>> GetCarsDetailByColorId(int colorId);
        IDataResult<List<CarDetailDto>> GetCarDetailsByBrandAndColor(int brandId, int colorId);

    }
}
