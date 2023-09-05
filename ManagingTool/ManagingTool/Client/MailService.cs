using System.Net.Http.Headers;
using System.Net.Http.Json;
using ManagingTool.Shared.DTO;
using Microsoft.JSInterop;

namespace ManagingTool.Client;

public class MailService : BaseService
{
    public static HttpClient _httpClient { get; set; }
    private readonly TokenManager _tokenManager;

    public MailService(TokenManager tokenManager)
    {
        _tokenManager = tokenManager;
    }

    public async Task<SendMailResponse> SendMail(MailForm mailForm, Int64 userId)
    {
        var request = new SendMailRequest
        {
            MailForm = mailForm,
            UserID = userId
        };

        var requestMessage = new HttpRequestMessage(HttpMethod.Post, "api/MailData/SendMail");
        SerializeReqBody(ref requestMessage, request);

        var (accessToken, refreshToken) = await _tokenManager.GetTokensFromSessionStorage();
        AttachTokensToRequestHeader(ref requestMessage, accessToken, refreshToken);

        var response = await _httpClient.SendAsync(requestMessage);
        await _tokenManager.UpdateAccessTokenIfPresent(response);

        var responseDTO = await response.Content.ReadFromJsonAsync<SendMailResponse>();

        return responseDTO;
    }

    public async Task<GetUserMailListResponse> GetUserMailList(Int64 userId)
    {
        var request = new GetUserMailListRequest
        {
            UserID = userId
        };

        var requestMessage = new HttpRequestMessage(HttpMethod.Post, "api/MailData/GetUserMailList");
        SerializeReqBody(ref requestMessage, request);

        var (accessToken, refreshToken) = await _tokenManager.GetTokensFromSessionStorage();
        AttachTokensToRequestHeader(ref requestMessage, accessToken, refreshToken);

        var response = await _httpClient.SendAsync(requestMessage);
        await _tokenManager.UpdateAccessTokenIfPresent(response);

        var responseDTO = await response.Content.ReadFromJsonAsync<GetUserMailListResponse>();

        return responseDTO;
    }
}

