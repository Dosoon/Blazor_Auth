using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Net.Http.Json;
using ManagingTool.Shared.DTO;

namespace ManagingTool.Client;

public class AuthService
{
    public static HttpClient _httpClient { get; set; }
    private readonly TokenManager _tokenManager;

    public AuthService(TokenManager tokenManager)
    {
        _tokenManager = tokenManager;
    }

    public async Task<ErrorCode> CheckToken()
    {
        var (accessToken, refreshToken) = await _tokenManager.GetTokensFromSessionStorage();
        AttachTokensToRequestHeader(accessToken, refreshToken);

        var response = await _httpClient.GetAsync("api/Auth");
        await _tokenManager.UpdateAccessTokenIfPresent(response);

        if (response.StatusCode != HttpStatusCode.OK)
        {
            return ErrorCode.Unauthorized;
        }

        var errorCode = await response.Content.ReadAsStringAsync();

        return (ErrorCode)UInt16.Parse(errorCode);
    }

    public async Task<ManagingLoginResponse> Login(string email, string pwd)
    {
        var request = new ManagingLoginRequest
        {
            Email = email,
            Password = pwd
        };

        var response = await _httpClient.PostAsJsonAsync("api/Auth/Login", request);
        await _tokenManager.UpdateAccessTokenIfPresent(response);

        var responseDTO = await response.Content.ReadFromJsonAsync<ManagingLoginResponse>();

        return responseDTO;
    }

    void AttachTokensToRequestHeader(string accessToken, string refreshToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        _httpClient.DefaultRequestHeaders.Remove("refresh_token");
        _httpClient.DefaultRequestHeaders.Add("refresh_token", refreshToken);
    }
}
