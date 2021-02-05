using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        public void Add(Car car)
        {
            if (car.DailyPrice > 0)
            {
                _carDal.Add(car);
                Console.WriteLine("Araba eklendi.");
            }
            else
            {
                Console.WriteLine("fiyat 0'dan büyük olmalıdır");
            }
        }
    

        public void Delete(Car car)
        {
            _carDal.Delete(car);
        }

        public List<Car> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetCarsByColorId(int id)
        {
            return _carDal.GetAll(c => c.ColorId == id);
        }

        public List<Car> GetCarsByBrandId(int id)
        {
            return _carDal.GetAll(c => c.BrandId == id);
        }


        public List<Car> GetAll()
        {
            return _carDal.GetAll();
        }

        Car ICarService.GetById(int id)
        {
            return _carDal.Get(c => c.CarId == id);
        }


        public void Update(Car car)
        {
            if (car.DailyPrice > 0)
            {
                _carDal.Update(car);
                Console.WriteLine("Araba Güncellendi.");
            }
            else
            {
                Console.WriteLine("fiyat 0'dan büyük olmalıdır");
            }
        }
    }
}
