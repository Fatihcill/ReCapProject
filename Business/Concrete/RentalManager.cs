using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        private IRentalDal _rentalDal;
        private ICarService _iCarService;
        private ICustomerService _iCustomerService;

        public RentalManager(IRentalDal rentalDal,ICarService iCarService,ICustomerService iCustomerService)
        {
            _rentalDal = rentalDal;
            _iCarService=iCarService;
            _iCustomerService = iCustomerService;
        }


        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(),  Messages.RentalsListed);
        }

        public IResult Add(Rental rental)
        {
            var result = BusinessRules.Run(WillLeasedCarAvailable(rental.CarId), FindeksScoreCheck(rental.CustomerId,rental.CarId));
            if (result!=null)
            {
                return result;
            }
            _rentalDal.Add(rental);
            return new Result(true);
        }


        
        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.RentalUpdated);
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.RentalDeleted);
        }

        
        public IDataResult<List<RentalDetailDto>> GetRentalsDetail()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.getRentalsDetailsDto(), Messages.RentalsListed);
        }

        public IDataResult<List<RentalsByCustomerDto>> GetRentalsByCustomerIdDto(int customerId)
        {
            return new SuccessDataResult<List<RentalsByCustomerDto>>(
                _rentalDal.getRentalsByCustomerIdDto(p => p.CustomerId == customerId),  Messages.RentalsListed);
        }

        public IDataResult<Rental> GetById(int rentalId)
        {
            var result = _rentalDal.Get(p => p.RentalId == rentalId);
            return new SuccessDataResult<Rental>(result,Messages.RentalsListed);
        }

        public IDataResult<List<Rental>> GetRentalByCarId(int carId)
        {
            var result = _rentalDal.GetAll(p => p.CarId == carId);
            return new SuccessDataResult<List<Rental>>(result);
        }


        private IResult WillLeasedCarAvailable(int carId)
        {
            if (_rentalDal.Get(p => p.CarId == carId && p.ReturnDate == null) != null)
                return new ErrorResult(Messages.CarNotAvailable);
            else
                return new SuccessResult();
        }

        private IResult CanARentalCarBeReturned(int carId)
        {
            if (_rentalDal.Get(p => p.CarId == carId && p.ReturnDate == null) == null)
                return new ErrorResult(Messages.CarNotAvailable);
            else
                return new SuccessResult();
        }

        private IResult FindeksScoreCheck(int customerId, int carId)
        {
            var customerFindexPoint = _iCustomerService.GetById(customerId).Data.CustomerFindexPoint;

            if (customerFindexPoint == 0)
                return new ErrorResult(Messages.FindexPointInvalid);

            var carFindexPoint = _iCarService.GetById(carId).Data.CarFindexPoint;

            if (customerFindexPoint < carFindexPoint)
                return new ErrorResult(Messages.FindexPointLimitError);

            return new SuccessResult();
        }

    }

}

