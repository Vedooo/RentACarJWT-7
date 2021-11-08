using Business.Abstract;
using Core.Entity.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class EfUserClaimManager : IUserClaimService
    {
        IUserClaimDal _userClaimDal;

        public EfUserClaimManager(IUserClaimDal userClaimDal)
        {
            _userClaimDal = userClaimDal;
        }

        public void Add(UserClaim userClaim)
        {
            _userClaimDal.Add(userClaim);
        }

        public UserClaim GetByMail(string mail)
        {
            return _userClaimDal.Get(u => u.Email == mail);
        }

        public List<OperationClaim> GetClaims(UserClaim userClaim)
        {
            return _userClaimDal.GetClaims(userClaim);
        }
    }
}
