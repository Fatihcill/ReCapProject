using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
   /* public class InMemoryCarDal : ICarDal
    {
        List<Car> _cars;
        public InMemoryCarDal()
        {
            _cars = new List<Car>
            {
                new Car{CarId=1,BrandId=1,ColorId=2,DailyPrice=9999,Description="Hello World",ModelYear=2001},
                new Car{CarId=2,BrandId=2,ColorId=2,DailyPrice=5000,Description="CAR 2 ",ModelYear=2020},
                new Car{CarId=3,BrandId=2,ColorId=1,DailyPrice=4350,Description="CAR 3 ",ModelYear=2005},
                new Car{CarId=4,BrandId=3,ColorId=1,DailyPrice=2000,Description="CAR 4 ",ModelYear=2008},
                new Car{CarId=5,BrandId=3,ColorId=1,DailyPrice=1500,Description="CAR 5 ",ModelYear=2015}
            };
        }
        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Delete(Car car)
        {
            Car carToDelete = _cars.SingleOrDefault(p => p.CarId == car.CarId);
            _cars.Remove(carToDelete);
        }

        public List<Car> GetAll()
        {
            return (_cars);
        }

        public List<Car> GetById(int Id)
        {
            return _cars.Where(p => p.CarId == Id).ToList();
        }

        public void Update(Car car)
        {
            Car carToUpdate = _cars.SingleOrDefault(p => p.CarId == car.CarId);
            carToUpdate.BrandId = car.BrandId;
            carToUpdate.ColorId= car.ColorId;
            carToUpdate.DailyPrice= car.DailyPrice;
            carToUpdate.Description= car.Description;
            carToUpdate.ModelYear= car.ModelYear;

        }
    }*/
}
