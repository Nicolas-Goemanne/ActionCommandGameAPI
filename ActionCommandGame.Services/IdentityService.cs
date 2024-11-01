using ActionCommandGame.Configuration;
using ActionCommandGame.Model;
using ActionCommandGame.Services.Model.Requests;
using ActionCommandGame.Services.Model.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ActionCommandGame.Services.Extensions;
using ActionCommandGame.Services.Model.Core;

namespace ActionCommandGame.Services
{
    public class IdentityService(UserManager<IdentityUser> userManager, JwtSettings jwtSettings)
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly JwtSettings _jwtSettings = jwtSettings;

        //SignIn
        public async Task<AuthenticationResult> SignIn(UserSignInRequest request)
        {
            var result = new AuthenticationResult();

            // Check user by username instead of email
            var existingUser = await _userManager.FindByNameAsync(request.Username);
            if (existingUser is null)
            {
                result.Success = false;
                result.Errors.Add("User not found.");
                return result;
            }

            // Check password
            var hasValidPassword = await _userManager.CheckPasswordAsync(existingUser, request.Password);
            if (!hasValidPassword)
            {
                result.Success = false;
                result.Errors.Add("Invalid password.");
                return result;
            }

            var token = GenerateJwtToken(existingUser);

            result.Token = token;
            result.Success = true;
            result.UserId = existingUser.Id;

            return result;
        }


        //Register
        public async Task<AuthenticationResult> Register(UserRegisterRequest request)
        {
            //Check user
            var existingUser = await _userManager.FindByNameAsync(request.Username);
            if (existingUser is not null)
            {
                var result = new AuthenticationResult();
                result.Messages.Add(new ServiceMessage
                {
                    Code = "RegisterFailed",
                    Message = "User already exists."
                });
                return result;
            }

            var registerUser = new IdentityUser(request.Username);
            var createUserResult = await _userManager.CreateAsync(registerUser, request.Password);
            if (!createUserResult.Succeeded)
            {
                var result = new AuthenticationResult();

                result.Messages = createUserResult.Errors.Select(e => new ServiceMessage
                {
                    Code = e.Code,
                    Message = e.Description
                    
                }).ToList();

                return result;
            }

            var token = GenerateJwtToken(registerUser);

            return new AuthenticationResult() { Token = token };
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Id", user.Id)
            };
            if (!string.IsNullOrWhiteSpace(user.UserName))
            {
                claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.UserName));
            }

            if (!string.IsNullOrWhiteSpace(user.Email))
            {
                claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_jwtSettings.ExpirationTimeSpan),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(securityToken);
        }

    }
}


