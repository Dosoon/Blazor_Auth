namespace WebAPIServer.Controllers.ManagingController;

using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using ManagingTool.Shared.DTO;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http.Headers;
using System.Net;

[ApiController]
[Route("api/[controller]")]
public class MailData : ControllerBase
{
    readonly ILogger<MailData> _logger;
    readonly HttpClient _httpClient;

    public MailData(ILogger<MailData> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    [HttpPost("SendMail")]
    public async Task<SendMailResponse> Post(SendMailRequest request)
    {
        AttachTokensToRequestHeader();

        var response = await _httpClient.PostAsJsonAsync("Managing/MailData/SendMail", request);
        AttachNewTokenToResponseIfPresent(ref response);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            return new SendMailResponse { errorCode = ErrorCode.Unauthorized };
        }

        var responseDTO = await response.Content.ReadFromJsonAsync<SendMailResponse>();

        if (responseDTO == null)
        {
            // errorlog
        }

        return responseDTO;
    }

    [HttpPost("GetUserMailList")]
    public async Task<GetUserMailListResponse> Post(GetUserMailListRequest request)
    {
        AttachTokensToRequestHeader();

        var response = await _httpClient.PostAsJsonAsync("Managing/MailData/GetUserMailList", request);
        AttachNewTokenToResponseIfPresent(ref response);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            return new GetUserMailListResponse { errorCode = ErrorCode.Unauthorized };
        }

        var responseDTO = await response.Content.ReadFromJsonAsync<GetUserMailListResponse>();

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