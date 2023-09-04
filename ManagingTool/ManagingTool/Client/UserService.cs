using System.Net.Http.Json;
using ManagingTool.Shared.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Headers;

namespace ManagingTool.Client;

public class UserService
{
    public static HttpClient _httpClient { get; set; }
    private readonly IJSRuntime _jsRuntime;

    public UserService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<GetUserBasicInfoListResponse> GetUserBasicInfo(Int64 userId)
    {
        var request = new GetUserBasicInfoRequest
        {
            UserID = userId
        };

        var sessionToken = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "accesstoken");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessionToken);

        var response = await _httpClient.PostAsJsonAsync("api/UserData/GetUserBasicInfo", request);
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

        var sessionToken = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "accesstoken");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessionToken);

        var response = await _httpClient.PostAsJsonAsync("api/UserData/GetMultipleUserBasicInfo", request);
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

        var sessionToken = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "accesstoken");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessionToken);

        var response = await _httpClient.PostAsJsonAsync("api/UserData/UpdateUserBasicInfo", request);
        var responseDTO = await response.Content.ReadFromJsonAsync<UpdateUserBasicInformationResponse>();

        if (responseDTO == null)
        {
            // errorlog
        }

        return responseDTO;
    }
}

