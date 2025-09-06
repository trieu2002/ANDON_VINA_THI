using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ANDON_Domain.Consts;
using ANDON_Domain.Exceptions;
using Microsoft.IdentityModel.Tokens;

namespace ANDON.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }
        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path.Value?.ToLower();
            if (path == RouterPublic.LoginRoute)
            {
                await _next(context);
                return;
            }

            var token = context.Request.Headers[AppJwtMiddleware.AuthorizationHeader].FirstOrDefault()?.Split(" ").Last();
            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthoriedException(401, "Đăng nhập thất bại", "Đăng nhập thất bại");
            }

            if (!string.IsNullOrEmpty(token))
            {
                context.User = DecodedToken(token);
            }

            await _next(context);
        }
        private ClaimsPrincipal DecodedToken(string token)
        {
            try
            {
                var jwtSettings = _configuration.GetSection("JwtSettings");
                var key = Encoding.UTF8.GetBytes(jwtSettings["Secret"]);
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidateAudience = true,
                    ValidAudience = jwtSettings["Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                if (!tokenHandler.CanReadToken(token))
                {
                    throw new UnauthoriedException(401, "Token không hợp lệ", "Token không hợp lệ");
                }

                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

                if (validatedToken is not JwtSecurityToken)
                {
                    throw new UnauthoriedException(401, "Token không hợp lệ", "Token không hợp lệ");
                }

                return principal;
            }
            catch (SecurityTokenExpiredException)
            {
                throw new UnauthoriedException(401, "Token không hợp lệ", "Token không hợp lệ");
            }
            catch (SecurityTokenInvalidSignatureException)
            {
                throw new UnauthoriedException(401, "Token không hợp lệ", "Token không hợp lệ");
            }
            catch (SecurityTokenException)
            {
                throw new UnauthoriedException(401, "Token không hợp lệ", "Token không hợp lệ");
            }
            catch (Exception)
            {
                throw new UnauthoriedException(401, "Token không hợp lệ", "Token không hợp lệ");
            }
        }

    }
}
