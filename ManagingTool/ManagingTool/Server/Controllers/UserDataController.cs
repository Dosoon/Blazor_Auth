namespace WebAPIServer.Controllers.ManagingController;

using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using ManagingTool.Shared.DTO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using ManagingTool.Client;
using System.Text.Json;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class UserData : BaseController
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
        var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Managing/UserData/GetUserBasicInfo");
        SerializeReqBody(ref requestMessage, request);
        AttachTokensToRequestHeader(ref requestMessage);

        var response = await _httpClient.SendAsync(requestMessage);
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
        var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Managing/UserData/GetMultipleUserBasicInfo");
        SerializeReqBody(ref requestMessage, request);
        AttachTokensToRequestHeader(ref requestMessage);

        var response = await _httpClient.SendAsync(requestMessage);
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
        var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Managing/UserData/UpdateUserBasicInfo");
        SerializeReqBody(ref requestMessage, request);
        AttachTokensToRequestHeader(ref requestMessage);

        var response = await _httpClient.SendAsync(requestMessage);
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
}
