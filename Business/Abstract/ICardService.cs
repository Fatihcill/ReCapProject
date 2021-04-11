using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICardService:IService<Card>
    {
        IDataResult<List<Card>> GetCardByCustomerId(int customer);
    }
}
