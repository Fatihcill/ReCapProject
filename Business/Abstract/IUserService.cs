using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
   public interface IUserService:IService<User>
   {
       IResult UpdateInfos(User user);
       List<OperationClaim> GetClaims(User user);
       User GetByMail(string email);
   }
}
