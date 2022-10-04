using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsersManagement.TokenBase.DTOs;

namespace UsersManagement.TokenBase.Extentions;

public class JwtAuthenticationToken
{
    private readonly string _key;
    public JwtAuthenticationToken(string key)
    {
        _key = key;
    }

    public  string GenerateToken(InputUserDto user, int ExpiresDay = 1)
    {
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.ASCII.GetBytes(_key);
        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {

            Subject = new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.Name,user.Name),
                    new Claim(ClaimTypes.NameIdentifier,user.UserId),
                    new Claim(ClaimTypes.Email,user.Email),
                    new Claim(ClaimTypes.GivenName,user.GivenName),
                    new Claim(ClaimTypes.Surname,user.Surname),
                    new Claim(ClaimTypes.MobilePhone,user.Mobile),
            }),
            Expires = DateTime.UtcNow.AddDays(ExpiresDay),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                                                  SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
