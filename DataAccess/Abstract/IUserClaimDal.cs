using Core.DataAccess;
using Core.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IUserClaimDal : IEntityRepository<UserClaim>
    {
        List<OperationClaim> GetClaims(UserClaim userClaim);
    }
}
