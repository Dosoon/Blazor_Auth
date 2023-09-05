using System.Net.Http.Json;
using ManagingTool.Shared.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Headers;

namespace ManagingTool.Client;

public class UserService : BaseService
{
    public static HttpClient _httpClient { get; set; }
    readonly TokenManager _tokenManager;

    public UserService(TokenManager tokenManager)
    {
        _tokenManager = tokenManager;
    }

    public async Task<GetUserBasicInfoListResponse> GetUserBasicInfo(Int64 userId)
    {
        var request = new GetUserBasicInfoRequest
        {
            UserID = userId
        };

        var requestMessage = new HttpRequestMessage(HttpMethod.Post, "api/UserData/GetUserBasicInfo");
        SerializeReqBody(ref requestMessage, request);

        var (accessToken, refreshToken) = await _tokenManager.GetTokensFromSessionStorage();
        AttachTokensToRequestHeader(ref requestMessage, accessToken, refreshToken);

        var response = await _httpClient.SendAsync(requestMessage);
        await _tokenManager.UpdateAccessTokenIfPresent(response);

        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
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

    public async Task<List<UserInfo>> GetMultipleUserBasicInfo(string category, Int64 minValue, Int64 maxValue)
    {
        var request = new GetMultipleUserBasicInfoRequest
        {
            Category = category,
            MinValue = minValue,
            MaxValue = maxValue
        };

        var requestMessage = new HttpRequestMessage(HttpMethod.Post, "api/UserData/GetMultipleUserBasicInfo");
        SerializeReqBody(ref requestMessage, request);

        var (accessToken, refreshToken) = await _tokenManager.GetTokensFromSessionStorage();
        AttachTokensToRequestHeader(ref requestMessage, accessToken, refreshToken);

        var response = await _httpClient.SendAsync(requestMessage);
        await _tokenManager.UpdateAccessTokenIfPresent(response);

        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            return new List<UserInfo>();
        }

        var responseDTO = await response.Content.ReadFromJsonAsync<GetUserBasicInfoListResponse>();

        if (responseDTO == null)
        {
            // errorlog
        }

        return responseDTO.UserInfo;
    }

    public async Task<UpdateUserBasicInformationResponse> UpdateUserBasicInfo(UserInfo userInfo)
    {
        var request = new UpdateUserBasicInformationRequest
        {
            UserInfo = userInfo
        };

        var requestMessage = new HttpRequestMessage(HttpMethod.Post, "api/UserData/UpdateUserBasicInfo");
        SerializeReqBody(ref requestMessage, request);

        var (accessToken, refreshToken) = await _tokenManager.GetTokensFromSessionStorage();
        AttachTokensToRequestHeader(ref requestMessage, accessToken, refreshToken);

        var response = await _httpClient.SendAsync(requestMessage);
        await _tokenManager.UpdateAccessTokenIfPresent(response);

        var responseDTO = await response.Content.ReadFromJsonAsync<UpdateUserBasicInformationResponse>();

        if (responseDTO == null)
        {
            // errorlog
        }

        return responseDTO;
    }
}

