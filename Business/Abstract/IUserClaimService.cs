using Core.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUserClaimService
    {
        List<OperationClaim> GetClaims(UserClaim userClaim);
        void Add(UserClaim userClaim);
        UserClaim GetByMail(string mail);
    }
}
