using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Net.Http.Json;
using ManagingTool.Shared.DTO;

namespace ManagingTool.Client;

public class AuthService : BaseService
{
    public static HttpClient _httpClient { get; set; }
    private readonly TokenManager _tokenManager;

    public AuthService(TokenManager tokenManager)
    {
        _tokenManager = tokenManager;
    }

    public async Task<ErrorCode> CheckToken()
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, "api/Auth");
        var (accessToken, refreshToken) = await _tokenManager.GetTokensFromSessionStorage();
        AttachTokensToRequestHeader(ref requestMessage, accessToken, refreshToken);

        var response = await _httpClient.SendAsync(requestMessage);
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
        var requestBody = new ManagingLoginRequest
        {
            Email = email,
            Password = pwd
        };

        var request = new HttpRequestMessage(HttpMethod.Post, "api/Auth/Login");
        SerializeReqBody(ref request, requestBody);

        var response = await _httpClient.SendAsync(request);
        await _tokenManager.UpdateAccessTokenIfPresent(response);

        var responseDTO = await response.Content.ReadFromJsonAsync<ManagingLoginResponse>();

        return responseDTO;
    }
}
