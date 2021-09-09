using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sms.Api.Controllers
{

    [Route("api/identity")]
    [ApiController]
    [OpenApiTag("认证接口", Description = "认证接口")]
    public class IdentityController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public IdentityController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        [HttpGet("test")]
        [Authorize]
        public string Get()
        {
            return "testAuthorize";
        }
        /// <summary>
        /// Token
        /// </summary>
        /// <returns></returns>
        [HttpGet("token")]
        [AllowAnonymous]
        public async Task<RestfulData<TokenModel>> Token()
        {
            var result = new RestfulData<TokenModel>();
            try
            {
                //if (string.IsNullOrEmpty(user)) throw new ArgumentNullException("user", "用户名不能为空！");
                //if (string.IsNullOrEmpty(password)) throw new ArgumentNullException("password", "密码不能为空！");
                ////var userInfo = await _UserService.CheckUserAndPassword(user,  password);
                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name,"yande"),
                    new Claim(ClaimTypes.NameIdentifier,"yande"),
                };

                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));
                var expires = DateTime.Now.AddDays(30);
                var token = new JwtSecurityToken(
                            issuer: _configuration["Issuer"],
                            audience: _configuration["Audience"],
                            claims: claims,
                            notBefore: DateTime.Now,
                            expires: expires,
                            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
              
                string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);  //生成Token
                result.code = 200;
                result.data = new TokenModel() { Token = jwtToken, Expires = expires.ToFileTimeUtc() };
                result.message = "授权成功!";
                return result;
            }
            catch (Exception ex)
            {
                result.message = ex.Message;
                result.code = 400;
                return result;
            }
        }
    }
}
