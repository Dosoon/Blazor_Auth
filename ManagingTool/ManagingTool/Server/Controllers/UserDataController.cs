namespace WebAPIServer.Controllers.ManagingController;

using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using ManagingTool.Shared.DTO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using Microsoft.AspNetCore.Authorization;

[Authorize]
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
        var sessionToken = HttpContext.Request.Headers["Authorization"];
        _httpClient.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(sessionToken);

        var response = await _httpClient.PostAsJsonAsync("Managing/UserData/GetUserBasicInfo", request);
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
        var sessionToken = HttpContext.Request.Headers["Authorization"];
        _httpClient.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(sessionToken);

        var response = await _httpClient.PostAsJsonAsync("Managing/UserData/GetMultipleUserBasicInfo", request);
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
        var sessionToken = HttpContext.Request.Headers["Authorization"];
        _httpClient.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(sessionToken);

        var response = await _httpClient.PostAsJsonAsync("Managing/UserData/UpdateUserBasicInfo", request);
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
}
