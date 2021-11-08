using Core.Utilities.Results;
using Entity.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IImageService
    {
        IResult Add(IFormFile formFile, Image image);
        IResult Update(IFormFile formFile, Image image);
        IResult Delete(Image image);

        IDataResult<Image> GetByImageId(int id);
        IDataResult<List<Image>> GetAll();
        IDataResult<List<Image>> GetByCarId(int carId);

    }
}
