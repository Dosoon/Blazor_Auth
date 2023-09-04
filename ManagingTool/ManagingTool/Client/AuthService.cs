using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Net.Http.Json;
using ManagingTool.Shared.DTO;

namespace ManagingTool.Client;

public class AuthService
{
    public static HttpClient _httpClient { get; set; }

    public async Task<ErrorCode> CheckToken(string accessToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var response = await _httpClient.GetAsync("api/Auth");
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            return ErrorCode.Unauthorized;
        }

        return ErrorCode.None;
    }

    public async Task<ManagingLoginResponse> Login(string email, string pwd)
    {
        var request = new ManagingLoginRequest
        {
            Email = email,
            Password = pwd
        };

        var response = await _httpClient.PostAsJsonAsync("api/Auth/Login", request);
        var responseDTO = await response.Content.ReadFromJsonAsync<ManagingLoginResponse>();

        return responseDTO;
    }
}
