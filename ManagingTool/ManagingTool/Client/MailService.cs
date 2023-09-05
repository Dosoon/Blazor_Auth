using System.Net.Http.Headers;
using System.Net.Http.Json;
using ManagingTool.Shared.DTO;
using Microsoft.JSInterop;

namespace ManagingTool.Client;

public class MailService
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

        var (accessToken, refreshToken) = await _tokenManager.GetTokensFromSessionStorage();
        AttachTokensToRequestHeader(accessToken, refreshToken);

        var response = await _httpClient.PostAsJsonAsync("api/MailData/SendMail", request);
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

        var (accessToken, refreshToken) = await _tokenManager.GetTokensFromSessionStorage();
        AttachTokensToRequestHeader(accessToken, refreshToken);

        var response = await _httpClient.PostAsJsonAsync("api/MailData/GetUserMailList", request);
        await _tokenManager.UpdateAccessTokenIfPresent(response);

        var responseDTO = await response.Content.ReadFromJsonAsync<GetUserMailListResponse>();

        return responseDTO;
    }

    void AttachTokensToRequestHeader(string accessToken, string refreshToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        _httpClient.DefaultRequestHeaders.Remove("refresh_token");
        _httpClient.DefaultRequestHeaders.Add("refresh_token", refreshToken);
    }
}

