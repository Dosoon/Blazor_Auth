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
public class MailData : BaseController
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
        var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Managing/MailData/SendMail");
        SerializeReqBody(ref requestMessage, request);
        AttachTokensToRequestHeader(ref requestMessage);

        var response = await _httpClient.SendAsync(requestMessage);
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
        var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Managing/MailData/GetUserMailList");
        SerializeReqBody(ref requestMessage, request);
        AttachTokensToRequestHeader(ref requestMessage);

        var response = await _httpClient.SendAsync(requestMessage);
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
}