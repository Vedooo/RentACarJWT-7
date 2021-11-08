using Core.Entity.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<UserClaim> Login(UserForLoginDto userForLoginDto);
        IDataResult<UserClaim> Register(UserForRegisterDto userForRegisterDto, string password);
        IDataResult<AccessToken> CreateAccessToken(UserClaim userClaim);

        IResult UserExists(string email);
    }
}
