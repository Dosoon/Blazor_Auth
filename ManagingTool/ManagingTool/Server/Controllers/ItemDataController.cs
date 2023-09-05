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
public class ItemData : ControllerBase
{
    readonly ILogger<ItemData> _logger;
    readonly HttpClient _httpClient;

    public ItemData(ILogger<ItemData> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    [HttpPost("GetItemTable")]
	public async Task<GetItemTableResponse> Post(GetItemTableRequest request)
	{
        AttachTokensToRequestHeader();

        var response = await _httpClient.PostAsJsonAsync("Managing/ItemData/GetItemTable", request);
        AttachNewTokenToResponseIfPresent(ref response);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            return new GetItemTableResponse();
        }

        var responseDTO = await response.Content.ReadFromJsonAsync<GetItemTableResponse>();

        if (responseDTO == null)
        {
            // errorlog
        }

        return responseDTO;
    }

    [HttpPost("GetUserItemList")]
    public async Task<GetUserItemListResponse> Post(GetUserItemListRequest request)
    {
        AttachTokensToRequestHeader();

        var response = await _httpClient.PostAsJsonAsync("Managing/ItemData/GetUserItemList", request);
        AttachNewTokenToResponseIfPresent(ref response);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            return new GetUserItemListResponse { errorCode = ErrorCode.Unauthorized };
        }

        var responseDTO = await response.Content.ReadFromJsonAsync<GetUserItemListResponse>();

        if (responseDTO == null)
        {
            // errorlog
        }

        return responseDTO;
    }

    [HttpPost("RetrieveUserItem")]
    public async Task<RetrieveUserItemResponse> Post(RetrieveUserItemRequest request)
    {
        AttachTokensToRequestHeader();

        var response = await _httpClient.PostAsJsonAsync("Managing/ItemData/RetrieveUserItem", request);
        AttachNewTokenToResponseIfPresent(ref response);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            return new RetrieveUserItemResponse { errorCode = ErrorCode.Unauthorized };
        }

        var responseDTO = await response.Content.ReadFromJsonAsync<RetrieveUserItemResponse>();

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
