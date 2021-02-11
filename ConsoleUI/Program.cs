using System;
using System.Collections.Generic;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager = new CarManager(new EfCarDal());
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            ColorManager colorManager = new ColorManager(new EfColorDal());
            //Day8Test(carManager, colorManager, brandManager);
            //Day9Test(carManager);

            var result = carManager.getCarDetail();
            if (result.Success)
            {
                Console.WriteLine("Add Test:  \nCar Name\tBrand Name\tColor Name\tDaily Price");
                foreach (var car in result.Data)
                {
                    Console.WriteLine(
                        $"{car.Descriptions}\t\t{car.BrandName}\t\t{car.ColorName}\t\t{car.DailyPrice}");
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }
            Console.WriteLine("\n");

            Console.ReadLine();

        }

        //private static void Day9Test(CarManager carManager)
        //{
        //    Console.WriteLine("GetAll Test: \nCar Name\tBrand Name\tColor Name\tDaily Price");
        //    GetAllWrite(carManager);

        //    //Add test
        //    Car newCar = new Car
        //    {
        //        BrandId = 3,
        //        ColorId = 1,
        //        ModelYear = "2005",
        //        DailyPrice = 5000,
        //        Descriptions = "New Car"
        //    };
        //    carManager.Add(newCar);
        //    Console.WriteLine("Add Test:  \nCar Name\tBrand Name\tColor Name\tDaily Price");
        //    GetAllWrite(carManager);

        //    //Update Test
        //    Car updateCar = new Car
        //    {
        //        CarId = 1019,
        //        BrandId = 4,
        //        ColorId = 3,
        //        ModelYear = "2015",
        //        DailyPrice = 7500,
        //        Descriptions = "Updated Car 1"
        //    };
        //    carManager.Update(newCar);
        //    int id = updateCar.CarId;
        //    Console.WriteLine("GetbyID & Update Test: \nCar ID\t\tCar Name");
        //    Console.WriteLine(
        //        $"{carManager.GetById(id).CarId}\t\t{carManager.GetById(id).Descriptions}");
        //    Console.WriteLine("\n");

        //    //Delete Test
        //    Console.WriteLine("Delete Test:\nCar Name\tBrand Name\tColor Name\tDaily Price");
        //    carManager.Delete(updateCar);
        //    GetAllWrite(carManager);
        //}

        //private static void GetAllWrite(CarManager carManager)
        //{

        //    foreach (var car in carManager.getCarDetail())
        //    {
        //        Console.WriteLine(
        //            $"{car.Descriptions}\t\t{car.BrandName}\t\t{car.ColorName}\t\t{car.DailyPrice}");
        //    }
        //    Console.WriteLine("\n");

        //}

        //private static void Day8Test(CarManager carManager, ColorManager colorManager, BrandManager brandManager)
        //{
        //    Console.WriteLine(
        //        "Brand Id'si 1 olan arabalar: \nId\tColor Name\tBrand Name\tModel Year\tDaily Price\tDescriptions");
        //    foreach (var car in carManager.GetCarsByBrandId(1))
        //    {
        //        Console.WriteLine(
        //            $"{car.CarId}\t{colorManager.GetById(car.ColorId).ColorName}\t\t{brandManager.GetById(car.BrandId).BrandName}\t\t{car.ModelYear}\t\t{car.DailyPrice}\t\t{car.Descriptions}");
        //    }

        //    Console.WriteLine(
        //        "\n\nColor Id'si 3 olan arabalar: \nId\tColor Name\tBrand Name\tModel Year\tDaily Price\tDescriptions");
        //    foreach (var car in carManager.GetCarsByColorId(3))
        //    {
        //        Console.WriteLine(
        //            $"{car.CarId}\t{colorManager.GetById(car.ColorId).ColorName}\t\t{brandManager.GetById(car.BrandId).BrandName}\t\t{car.ModelYear}\t\t{car.DailyPrice}\t\t{car.Descriptions}");
        //    }
        //}
    }
}
