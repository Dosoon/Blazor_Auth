
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPIServer.DbOperations;

namespace WebAPIServer.Util;

public class TokenManager
{
    private static string _signingKey;

    public static void Init(IConfiguration configuration)
    {
        _signingKey = configuration["SigningKey"]!;
    }

    public static Tuple<string, string> CreateTokens(Int64 accountId)
    {
        var accessToken = CreateToken(true, accountId);
        var refreshToken = CreateToken(false, accountId);

        return new Tuple<string, string>(accessToken, refreshToken);
    }

    public static string CreateToken(bool isAccessToken, Int64 accountId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_signingKey); // 서명 키

        var expires = isAccessToken ? DateTime.UtcNow.AddHours(1) : DateTime.UtcNow.AddHours(6);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
            new Claim("AccountId", accountId.ToString()), // 사용자 이름 또는 ID
            }),
            Expires = expires, // 토큰 만료 시간 설정
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public static Int64 GetClaim(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var refreshToken = handler.ReadJwtToken(token);

        // AccountId Claim 추출
        var accountIdClaim = refreshToken.Claims.FirstOrDefault(claim => claim.Type.Equals("AccountId"));

        if (accountIdClaim != null)
        {
            return Int64.Parse(accountIdClaim.Value);
        }

        return 0;
    }

    public async Task OnAuthenticationFailedHandler(AuthenticationFailedContext context, JwtBearerOptions options)
    {
        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
        {
            // 리프레시 토큰 가져오기
            var refreshToken = context.Request.Headers["refresh_token"].FirstOrDefault();
            if (refreshToken == null)
            {
                Console.WriteLine("RTNull");
                context.Response.StatusCode = 401; // Unauthorized
                return;
            }

            try
            {
                new JwtSecurityTokenHandler().ValidateToken(refreshToken, options.TokenValidationParameters,
                                                            out var validatedToken);

                // 리프레시 토큰의 만료 시간 확인
                if (validatedToken.ValidTo < DateTime.UtcNow)
                {
                    Console.WriteLine("RTExpired");
                    context.Response.StatusCode = 401; // Unauthorized
                    return;
                }
            }
            catch
            {
                context.Response.StatusCode = 401; // Unauthorized
                return;
            }

            // 리프레시 토큰에서 AccountId 가져오기
            var accountId = TokenManager.GetClaim(refreshToken);
            if (accountId == 0)
            {
                Console.WriteLine("AccountId");
                context.Response.StatusCode = 401; // Unauthorized
                return;
            }

            // DB의 RefreshToken과 비교
            var managingDb = context.HttpContext.RequestServices.GetRequiredService<IManagingDb>();
            var DBRefreshToken = await managingDb.GetRefreshTokenByAccountId(accountId);
            if (DBRefreshToken != refreshToken)
            {
                context.Response.StatusCode = 401; // Unauthorized
                return;
            }

            string newAccessToken = TokenManager.CreateToken(true, accountId);
            context.Response.Headers.Add("X-NEW-ACCESS-TOKEN", newAccessToken);

            ClaimsIdentity claims = new ClaimsIdentity(new[]
            {
                            new Claim("AccountId", accountId.ToString()),
                        }, JwtBearerDefaults.AuthenticationScheme);

            context.Principal = new ClaimsPrincipal(new ClaimsIdentity[] { claims });
            context.Success();
        }
    }
}