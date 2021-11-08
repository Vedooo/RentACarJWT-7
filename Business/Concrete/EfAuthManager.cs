using Business.Abstract;
using Business.Constants.Message;
using Core.Entity.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Results.DataResultOptions.DataOptions;
using Core.Utilities.Results.ResultOptions.Options;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class EfAuthManager : IAuthService
    {
        private IUserClaimService _userClaimService;
        private ITokenHelper _tokenHelper;

        public EfAuthManager(IUserClaimService userClaimService, ITokenHelper tokenHelper)
        {
            _userClaimService = userClaimService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<AccessToken> CreateAccessToken(UserClaim userClaim)
        {
            var claims = _userClaimService.GetClaims(userClaim);
            var accessToken = _tokenHelper.CreateToken(userClaim, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

        public IDataResult<UserClaim> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userClaimService.GetByMail(userForLoginDto.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<UserClaim>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<UserClaim>(Messages.PasswordError);
            }
            return new SuccessDataResult<UserClaim>(Messages.SuccessfulLogin);
        }

        public IDataResult<UserClaim> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new UserClaim
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            _userClaimService.Add(user);
            return new SuccessDataResult<UserClaim>(user, Messages.UserRegistered);
        }

        public IResult UserExists(string email)
        {
            if (_userClaimService.GetByMail(email) != null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }
    }
}
