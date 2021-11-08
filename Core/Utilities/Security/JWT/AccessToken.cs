using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public class AccessToken
    {
        public string Token { get; set; } //Web Token Value
        public DateTime Expiration { get; set; } //Ending time
    }
}
