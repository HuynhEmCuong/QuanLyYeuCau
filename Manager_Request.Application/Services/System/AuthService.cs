using Manager_Request.Application.Configuration;
using Manager_Request.Application.Const;
using Manager_Request.Application.Dtos;
using Manager_Request.Application.Extensions;
using Manager_Request.Data.Entities;
using Manager_Request.Data.Enums;
using Manager_Request.Ultilities;
using Manager_Request.Utilities.Constants;
using Manager_Request.Utilities.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using QLHB.Data.EF;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Manager_Request.Application.Services.System
{

    public interface IAuthService
    {
        Task<OperationResult> LoginAsync(LoginDto model);

        Task<OperationResult> LogoutAsync();

        Task<OperationResult> ResetPasswordAsync(int id);

        Task<OperationResult> ChangePasswordAsync(int id, string password);
    }
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppDbContext _context;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly JwtSettings _jwtSettings;
        private OperationResult operationResult;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, AppDbContext context, TokenValidationParameters tokenValidationParameters, JwtSettings jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _tokenValidationParameters = tokenValidationParameters;
            _jwtSettings = jwtSettings;
        }

        public async Task<OperationResult> ChangePasswordAsync(int id, string password)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                try
                {
                    // Generate the reset token (this would generally be sent out as a query parameter as part of a 'reset' link in an email)
                    string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

                    // Use the reset token to verify the provenance of the reset request and reset the password.
                    var result = await _userManager.ResetPasswordAsync(user, resetToken, password);
                    if (result.Succeeded)
                    {
                        operationResult = new OperationResult()
                        {
                            Message = "Đặt lại mật khẩu thành công!",
                            Success = true
                        };
                    }
                    else
                    {
                        operationResult = new OperationResult()
                        {
                            StatusCode = StatusCode.BadRequest,
                            Message = "Đặt lại mật khẩu thất bại! \n Mã lỗi: " + string.Join("\n", result.Errors.Select(x => x.Description).ToList()),
                            Success = false
                        };
                    }
                }
                catch (Exception ex)
                {
                    operationResult = new OperationResult
                    {
                        StatusCode = StatusCode.BadRequest,
                        Message = "Lỗi hệ thống",
                        Success = false
                    };
                }
            }
            else
            {
                operationResult = new OperationResult()
                {
                    StatusCode = StatusCode.BadRequest,
                    Message = "Tài khoản không được tìm thấy!",
                    Success = false
                };
            }
            return operationResult;
        }

        public async Task<OperationResult> LoginAsync(LoginDto model)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.Username);
                    if (user.Status == Status.DeActive)
                    {
                        operationResult = new OperationResult
                        {
                            StatusCode = StatusCode.BadRequest,
                            Message = "Tài khoản đã bị khoá ! Vui lòng liên hệ Admin",
                            Success = true
                        };
                    }
                    else
                    {
                        operationResult = await GenerateOperationResultForUserAsync(user, model.Password);
                    }
                }
                else
                {
                    operationResult = new OperationResult
                    {
                        StatusCode = StatusCode.BadRequest,
                        Message = "Tài khoản không được tìm thấy! Vui lòng kiểm tra lại",
                        Success = false
                    };
                }
            }
            catch (Exception ex)
            {

                operationResult = ex.GetMessageError();
            }
            return operationResult;


        }

        public async Task<OperationResult> LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return new OperationResult() { Message = "Đăng xuất thành công !", Success = true };
        }

        public async Task<OperationResult> ResetPasswordAsync(int id)
        {
            return await ChangePasswordAsync(id, Commons.PASSWORD_DEFAULT);
        }

        private async Task<OperationResult> GenerateOperationResultForUserAsync(AppUser user, string password)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                            new Claim(JwtRegisteredClaimNames.Email, user.Email??string.Empty),
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                            new Claim("id", user.Id.ToString()),
                            new Claim("name", user.Name),
                            new Claim("phonenumber", user.PhoneNumber??string.Empty),
                            new Claim("roles",roles.ToJsonString()),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        };
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.Add(_jwtSettings.TokenLifetime),
                //Expires = DateTime.Now.Add(TimeSpan.FromSeconds(15)),
                SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);


            return new OperationResult
            {
                StatusCode = StatusCode.Ok,
                Success = true,
                Data = new
                {
                    Token = tokenHandler.WriteToken(token),
                }
            };
        }
    }
}
