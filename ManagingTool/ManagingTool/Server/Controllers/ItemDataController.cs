namespace WebAPIServer.Controllers.ManagingController;

using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using ManagingTool.Shared.DTO;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http.Headers;
using System.Net;
using System.Text.Json;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class ItemData : BaseController
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
        var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Managing/ItemData/GetItemTable");
        SerializeReqBody(ref requestMessage, request);
        AttachTokensToRequestHeader(ref requestMessage);

        var response = await _httpClient.SendAsync(requestMessage);
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
        var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Managing/ItemData/GetUserItemList");
        SerializeReqBody(ref requestMessage, request);
        AttachTokensToRequestHeader(ref requestMessage);

        var response = await _httpClient.SendAsync(requestMessage);
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
        var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Managing/ItemData/RetrieveUserItem");
        SerializeReqBody(ref requestMessage, request);
        AttachTokensToRequestHeader(ref requestMessage);

        var response = await _httpClient.SendAsync(requestMessage);
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
}
