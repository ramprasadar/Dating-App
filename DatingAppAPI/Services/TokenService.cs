using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingAppAPI.Entities;
using DatingAppAPI.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace DatingAppAPI.Services
{
  public class TokenService : ITokenService
  {
    private readonly SymmetricSecurityKey _key;
    
    public TokenService(IConfiguration config)
    {   
        //converting string "TokenKey" to Byte Array and passing to SymmetricSecurityKey class.
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
    }

    public string CreateToken(AppUser user)
    {
      var claims = new List<Claim>
      {
        new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
      };

      var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject= new ClaimsIdentity(claims),
        Expires= DateTime.Now.AddDays(7),
        SigningCredentials=creds
      };

      var tokenHandler = new JwtSecurityTokenHandler();

      var token= tokenHandler.CreateToken(tokenDescriptor);
      
      return tokenHandler.WriteToken(token);
    }
  }
}