using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Core.DataAccess;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, RentACarContext>, IRentalDal
    {
        public List<RentalDetailDto> getRentalsDetailsDto()
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result = from rental in context.Rentals
                             join cst in context.Customers on rental.CustomerId equals cst.CustomerId
                             join car in context.Cars on rental.CarId equals car.CarId
                             join brand in context.Brands on car.BrandId equals brand.BrandId
                             join user in context.Users on cst.UserId equals user.UserId
                             join color in context.Colors on car.ColorId equals color.ColorId
                             select new RentalDetailDto()
                             {
                                 BrandName = brand.BrandName,
                                 FirstName = user.FirstName,
                                 LastName = user.LastName,
                                 RentDate = rental.RentDate,
                                 ReturnDate = rental.ReturnDate,
                                 ColorName = color.ColorName,
                                 DailyPrice = car.DailyPrice,
                                 ModelYear = car.ModelYear,
                                 CompanyName = cst.CompanyName,
                                 CarDescription = car.Description,
                                 RentalId = rental.RentalId
                             };
                return result.ToList();

            }
        }

        public List<RentalsByCustomerDto> getRentalsByCustomerIdDto(Expression<Func<Rental, bool>> filter)
        {
            using (RentACarContext context = new RentACarContext())
            { 
                var result = from rental in filter is null ? context.Rentals : context.Rentals.Where(filter)
                    join customer in context.Customers on rental.CustomerId equals customer.CustomerId

                    join user in context.Users on customer.UserId equals user.UserId
                    join car in context.Cars on rental.CarId equals car.CarId
                    join color in context.Colors on car.ColorId equals color.ColorId
                    join brand in context.Brands on car.BrandId equals brand.BrandId
                    select new RentalsByCustomerDto()
                    {
                        BrandName = brand.BrandName,
                        ColorName = color.ColorName,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        RentDate = rental.RentDate,
                        ReturnDate = rental.ReturnDate
                    };
                return result.ToList();

            }
        }
    }
}

