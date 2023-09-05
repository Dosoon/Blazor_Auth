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

    [Authorize]
    [HttpGet("/CheckToken")]
    public ErrorCode CheckToken()
    {
        return ErrorCode.None;
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

        if (userData == null)
        {
            _logger.ZLogErrorWithPayload(LogManager.MakeEventId(ErrorCode.LoginFailed), request, "Login Failed");

            response.Result = ErrorCode.LoginFailed;
            return response;
        }

        // 토큰 생성 및 Response 제공
        var (accessToken, refreshToken) = TokenManager.CreateTokens(userData.AccountId);

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
}