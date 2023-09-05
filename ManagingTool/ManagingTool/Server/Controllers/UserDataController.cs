namespace WebAPIServer.Controllers.ManagingController;

using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using ManagingTool.Shared.DTO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using ManagingTool.Client;

[ApiController]
[Route("api/[controller]")]
public class UserData : ControllerBase
{
    readonly ILogger<UserData> _logger;
    readonly HttpClient _httpClient;

    public UserData(ILogger<UserData> logger, HttpClient httpClient)
    {
		_logger = logger;
        _httpClient = httpClient;
    }

    [HttpPost("GetUserBasicInfo")]
	public async Task<GetUserBasicInfoListResponse> Post(GetUserBasicInfoRequest request)
    {
        AttachTokensToRequestHeader();

        var response = await _httpClient.PostAsJsonAsync("Managing/UserData/GetUserBasicInfo", request);
        AttachNewTokenToResponseIfPresent(ref response);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            return new GetUserBasicInfoListResponse { errorCode = ErrorCode.Unauthorized };
        }

        var responseDTO = await response.Content.ReadFromJsonAsync<GetUserBasicInfoListResponse>();

        if (responseDTO == null)
        {
            // errorlog
        }

        return responseDTO;
    }

	[HttpPost("GetMultipleUserBasicInfo")]
	public async Task<GetUserBasicInfoListResponse> Post(GetMultipleUserBasicInfoRequest request)
    {
        AttachTokensToRequestHeader();

        var response = await _httpClient.PostAsJsonAsync("Managing/UserData/GetMultipleUserBasicInfo", request);
        AttachNewTokenToResponseIfPresent(ref response);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            return new GetUserBasicInfoListResponse { errorCode = ErrorCode.Unauthorized };
        }

        var responseDTO = await response.Content.ReadFromJsonAsync<GetUserBasicInfoListResponse>();

        if (responseDTO == null)
        {
            // errorlog

        }

        return responseDTO;
    }

    [HttpPost("UpdateUserBasicInfo")]
    public async Task<UpdateUserBasicInformationResponse> Post(UpdateUserBasicInformationRequest request)
    {
        AttachTokensToRequestHeader();

        var response = await _httpClient.PostAsJsonAsync("Managing/UserData/UpdateUserBasicInfo", request);
        AttachNewTokenToResponseIfPresent(ref response);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            return new UpdateUserBasicInformationResponse { errorCode = ErrorCode.Unauthorized };
        }

        var responseDTO = await response.Content.ReadFromJsonAsync<UpdateUserBasicInformationResponse>();

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
                HttpContext.Response.Headers.Remove("X-NEW-ACCESS-TOKEN");
                HttpContext.Response.Headers.Add("X-NEW-ACCESS-TOKEN", newAccessToken);
            }
        }
    }
}
