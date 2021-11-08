using Core.DataAccess.EntityFramework;
using Core.Entity.Concrete;
using DataAccess.Abstract;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserClaimDal : EfEntityRepositoryBase<UserClaim, CarContext>, IUserClaimDal
    {
        public List<OperationClaim> GetClaims(UserClaim userClaim)
        {
            using (var context = new CarContext())
            {
                var result = from OperationClaim in context.OperationClaims
                             join UserOperationClaim in context.UserOperationClaims on OperationClaim.Id equals UserOperationClaim.OperationClaimId
                             where UserOperationClaim.UserId == userClaim.Id
                             select new OperationClaim
                             {
                                 Id = OperationClaim.Id,
                                 Name = OperationClaim.Name
                             };
                return result.ToList();
            }
        }
    }
}
