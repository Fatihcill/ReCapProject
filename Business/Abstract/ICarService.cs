using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface ICarService : IService<Car>
    {

        IDataResult<List<Car>> GetCarsByColorId(int id);
        IDataResult<List<Car>> GetCarsByBrandId(int id);
        IDataResult<List<Car>> GetCarsByDailyPrice(decimal min, decimal max);


        IDataResult<List<CarDetailDto>> GetCarDetailsByBrandId(int brandId);
        IDataResult<List<CarDetailDto>> GetCarDetailsByColorId(int colorId);
        IDataResult<List<CarDetailDto>> GetCarDetailsByModelYear(short min, short max);
        IDataResult<List<CarDetailDto>> GetCarDetail(int carId);
        IDataResult<List<CarDetailDto>> GetCarDetails();

        IResult AddTransactionalTest(Car car);
        //RESTFUL ---> HTTP --->
    }
}
