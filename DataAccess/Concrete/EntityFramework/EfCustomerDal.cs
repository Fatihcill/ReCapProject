using System.Collections.Generic;
using System.Linq;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCustomerDal:EfEntityRepositoryBase<Customer,RentACarContext>,ICustomerDal
    {
        public List<CustomerDetailDto> GetCustomerDetails()
        {
            using (var context = new RentACarContext())
            {
                var result = from c in context.Customers
                    join u in context.Users on c.UserId equals u.UserId
                    select new CustomerDetailDto
                    {
                        Id = c.UserId,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Email = u.Email,
                        Status = u.Status,
                        CompanyName = c.CompanyName
                    };

                return result.ToList();
            }
        }
        
    }
}