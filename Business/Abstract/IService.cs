using System.Collections.Generic;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IService<T>
    {
        IResult Add(T t);
        IResult Update(T t);
        IResult Delete(T t);
        IDataResult<List<T>> GetAll();

        IDataResult<T> GetById(int id);
    }
}