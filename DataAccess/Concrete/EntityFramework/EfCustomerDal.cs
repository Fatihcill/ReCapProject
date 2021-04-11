using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Core.DataAccess;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Concrete.EntityFramework
{
  public  class EfCustomerDal:EfEntityRepositoryBase<Customer,RentACarContext>,ICustomerDal
    {

      

        public List<CustomerDetailDto> getCustomerDetails(Expression<Func<Customer, bool>> filter=null)
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result = from customer in filter is null? context.Customers: context.Customers.Where(filter)
                    join user in context.Users
                        on customer.UserId equals user.UserId
                    select new CustomerDetailDto
                    {
                        CompanyName = customer.CompanyName,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        UserId = user.UserId,
                        CustomerId = customer.CustomerId,
                        Status = user.Status,
                        CustomerFindexPoint = (int)customer.CustomerFindexPoint
                    };
                return result.ToList();
            }
        }

        public CustomerDetailDto getCustomerDetailById(Expression<Func<Customer, bool>> filter)
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result = from customer in filter is null ? context.Customers : context.Customers.Where(filter)
                    join user in context.Users
                        on customer.UserId equals user.UserId
                    select new CustomerDetailDto
                    {
                        CompanyName = customer.CompanyName,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        UserId = user.UserId,
                        CustomerId = customer.CustomerId,
                        Status = user.Status,
                        CustomerFindexPoint = (int) customer.CustomerFindexPoint
                    };
                return result.FirstOrDefault();
            }
        }

       public CustomerDetailDto getCustomerByEmail(Expression<Func<CustomerDetailDto, bool>> filter)
        {
            using (RentACarContext context=new RentACarContext())
            {
                var result = from customer in context.Customers
                    join user in context.Users
                        on customer.UserId equals user.UserId
                    select new CustomerDetailDto
                    {
                        CustomerId = customer.CustomerId,
                        UserId = user.UserId,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        CompanyName = customer.CompanyName,
                        CustomerFindexPoint = (int) customer.CustomerFindexPoint
                    };
                return result.SingleOrDefault(filter);
            }
        }

       
    }
}
