namespace WebAPIServer.Controllers.ManagingController;

using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using ManagingTool.Shared.DTO;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.Text;
using System.Text.Json;

[ApiController]
[Route("api/[controller]")]
public class Auth : BaseController
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
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, "CheckToken");
        AttachTokensToRequestHeader(ref requestMessage);

        var response = await _httpClient.SendAsync(requestMessage);
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
        var requestMessage = new HttpRequestMessage(HttpMethod.Post, "ManagingLogin");
        SerializeReqBody(ref requestMessage, request);

        var response = await _httpClient.PostAsJsonAsync("ManagingLogin", request);
        var responseDTO = await response.Content.ReadFromJsonAsync<ManagingLoginResponse>();

        if (responseDTO == null)
        {
            // errorlog
        }

        return responseDTO;
    }
}