using Core.Utilities.Results;
using Entity.Concrete;
using Entity.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IRentalService
    {
        IResult IsCarEverRented(int carId);
        IResult IsCarReturned(int carId);
        IResult IsCarAvaible(int carId);

        IResult Add(Rental rental);
        IResult Update(Rental rental);
        IResult Delete(Rental rental);

        IDataResult<Rental> GetById(int rentId);
        IDataResult<List<Rental>> GetAll();
        IDataResult<List<CarRentDetailDto>> GetCarDetail();
        IDataResult<List<CarRentDetailDto>> GetCarDetailById(int carId);

        
    }
}
