using System.Collections.Generic;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IRentalService:IService<Rental>
    {
        IDataResult<List<RentalDetailDto>> GetRentalsDetail();
        IDataResult<List<RentalsByCustomerDto>> GetRentalsByCustomerIdDto(int customerId);
        IDataResult<List<Rental>> GetRentalByCarId(int carId);
    }
}
