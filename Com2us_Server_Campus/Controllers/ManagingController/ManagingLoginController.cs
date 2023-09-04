using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebAPIServer.DbOperations;
using WebAPIServer.ReqRes;
using WebAPIServer.Util;
using ZLogger;

[ApiController]
[Route("[controller]")]
public class ManagingLogin : ControllerBase
{
    readonly ILogger<ManagingLogin> _logger;
    readonly IManagingDb _managingDb;

    public ManagingLogin(ILogger<ManagingLogin> logger, IManagingDb managingDb)
    {
        _logger = logger;
        _managingDb = managingDb;
    }

    [HttpPost]
    public async Task<ManagingLoginResponse> Post(ManagingLoginRequest request)
    {
        var response = new ManagingLoginResponse();
        response.Result = ErrorCode.None;

        // 로그인 유저 데이터 체크
        var (errorCode, userData) = await _managingDb.GetLoginUserData(request.Email, request.Password);
        if (errorCode != ErrorCode.None)
        {
            _logger.ZLogErrorWithPayload(LogManager.MakeEventId(errorCode), request, "Login Error");

            response.Result = errorCode;
            return response;
        }

        // 토큰 생성 및 Response 제공
        var (accessToken, refreshToken) = CreateTokens(userData.AccountId);

        var updateErrorCode = await _managingDb.UpdateRefreshToken(userData.AccountId, refreshToken);
        if (updateErrorCode != ErrorCode.None)
        {
            _logger.ZLogErrorWithPayload(LogManager.MakeEventId(updateErrorCode), request, "UpdateRefreshToken Error");

            response.Result = updateErrorCode;
            return response;
        }

        response.Name = userData.Name;
        response.Email = userData.Email;
        response.AccountId = userData.AccountId;
        response.RefreshToken = refreshToken;
        response.AccessToken = accessToken;

        return response;
    }

    Tuple<string, string> CreateTokens(Int64 accountId)
    {
        var accessToken = CreateToken(true, accountId);
        var refreshToken = CreateToken(false, accountId);
        
        return new Tuple<string, string>(accessToken, refreshToken);
    }

    string CreateToken(bool isAccessToken, Int64 accountId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("Com2usGenieusInternship"); // 서명 키

        var expires = isAccessToken ? DateTime.UtcNow.AddHours(1) : DateTime.UtcNow.AddDays(1);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
            new Claim(ClaimTypes.NameIdentifier, accountId.ToString()), // 사용자 이름 또는 ID
            }),
            Expires = expires, // 토큰 만료 시간 설정
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}