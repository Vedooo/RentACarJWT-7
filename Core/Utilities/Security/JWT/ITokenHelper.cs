using Core.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(UserClaim userClaim, List<OperationClaim> operationClaims); //JWT createor 
    }
}
