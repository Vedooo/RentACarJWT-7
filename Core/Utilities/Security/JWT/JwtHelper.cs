using Core.Extensions;
using Core.Entity.Concrete;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; }
        private TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;
        
        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }

        public AccessToken CreateToken(UserClaim userClaim, List<OperationClaim> operationClaims)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenOptions, userClaim, signingCredentials, operationClaims);
                                                                                                       
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };
        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, UserClaim userClaim,
            SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            var jwt = new JwtSecurityToken(                 //Creating Token
                issuer: tokenOptions.Issuer,                //appsettings infos
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(userClaim, operationClaims),
                signingCredentials: signingCredentials
            );
            return jwt;
        }

        private IEnumerable<Claim> SetClaims(UserClaim userClaim, List<OperationClaim> operationClaims)     //Helper Metod
        {                                                                                                   //User İnformation claims array
            var claims = new List<Claim>();
            claims.AddNameIdentifier(userClaim.Id.ToString());
            claims.AddEmail(userClaim.Email);
            claims.AddName($"{userClaim.FirstName} {userClaim.LastName}");                                  //We can show two string in same line help with $ sign
            claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());                                 //Burada rol ekliyoruz.

            return claims;
        }
    }
}
