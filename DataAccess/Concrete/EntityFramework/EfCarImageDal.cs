using System.Collections.Generic;
using System.Linq;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{

    public class EfCarImageDal : EfEntityRepositoryBase<CarImage, RentACarContext>, ICarImageDal
    {

    }
}