namespace WebAPIServer.Controllers.ManagingController;

using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using ManagingTool.Shared.DTO;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using System.Net.Http.Headers;

[ApiController]
[Route("api/[controller]")]
public class Auth : ControllerBase
{
    readonly ILogger<ItemData> _logger;
    readonly HttpClient _httpClient;

    public Auth(ILogger<ItemData> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    [HttpGet]
    public async Task<ErrorCode> CheckToken()
    {
        AttachTokensToRequestHeader();

        var response = await _httpClient.GetAsync("CheckToken");
        AttachNewTokenToResponseIfPresent(ref response);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            return ErrorCode.Unauthorized;
        }

        return ErrorCode.None;
    }

    [HttpPost("Login")]
    public async Task<ManagingLoginResponse> Login(ManagingLoginRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("ManagingLogin", request);
        var responseDTO = await response.Content.ReadFromJsonAsync<ManagingLoginResponse>();

        if (responseDTO == null)
        {
            // errorlog
        }

        return responseDTO;
    }
    void AttachTokensToRequestHeader()
    {
        var accessToken = HttpContext.Request.Headers["Authorization"];
        var refreshToken = HttpContext.Request.Headers["refresh_token"].FirstOrDefault();
        _httpClient.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(accessToken);
        _httpClient.DefaultRequestHeaders.Remove("refresh_token");
        _httpClient.DefaultRequestHeaders.Add("refresh_token", refreshToken);
    }
    void AttachNewTokenToResponseIfPresent(ref HttpResponseMessage res)
    {
        if (res.Headers.TryGetValues("X-NEW-ACCESS-TOKEN", out var newAccessTokenEnum))
        {
            var newAccessToken = newAccessTokenEnum.FirstOrDefault();
            if (newAccessToken != null || newAccessToken != string.Empty)
            {
                _httpClient.DefaultRequestHeaders.Remove("X-NEW-ACCESS-TOKEN");
                _httpClient.DefaultRequestHeaders.Add("X-NEW-ACCESS-TOKEN", newAccessToken);
            }
        }
    }
}