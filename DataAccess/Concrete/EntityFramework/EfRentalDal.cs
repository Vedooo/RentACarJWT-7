using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, CarContext>, IRentalDal
    {
        public List<CarRentDetailDto> GetCarsDetails()
        {
            using (CarContext context = new CarContext())
            {
                var result = from r in context.Rentals
                             join c in context.Cars on r.CarId equals c.CarId
                             join cu in context.CustomerOfRents on r.CustomerId equals cu.CustomerId
                             select new CarRentDetailDto
                             {
                                 CarId = c.CarId,
                                 CarName = c.Description,
                                 CustomerId = cu.CustomerId,
                                 RentId = r.RentId,
                                 ModelYear = c.ModelYear,
                                 RentDate = r.RentDate,
                                 ReturnDate = r.ReturnDate
                             };
                return result.ToList();
            }
        }

        public List<CarRentDetailDto> GetCarsDetailsById(int carId)
        {
            var resultList = GetCarsDetails();
            return resultList.Where(c => c.CarId == carId).ToList();
        }
    }
}
