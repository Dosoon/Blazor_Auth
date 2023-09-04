using System.Net.Http.Headers;
using System.Net.Http.Json;
using ManagingTool.Shared.DTO;
using Microsoft.JSInterop;

namespace ManagingTool.Client;

public class MailService
{
    public static HttpClient _httpClient { get; set; }
    private readonly IJSRuntime _jsRuntime;

    public MailService(IJSRuntime jsRuntime)
	{
		_jsRuntime = jsRuntime;
	}

	public async Task<SendMailResponse> SendMail(MailForm mailForm, Int64 userId)
    {
        var request = new SendMailRequest
        {
            MailForm = mailForm,
            UserID = userId
        };

		var sessionToken = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "accesstoken");
		_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessionToken);

		var response = await _httpClient.PostAsJsonAsync("api/MailData/SendMail", request);
        var responseDTO = await response.Content.ReadFromJsonAsync<SendMailResponse>();

        return responseDTO;
    }

    public async Task<GetUserMailListResponse> GetUserMailList(Int64 userId)
    {
        var request = new GetUserMailListRequest
        {
            UserID = userId
        };

		var sessionToken = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "accesstoken");
		_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessionToken);

		var response = await _httpClient.PostAsJsonAsync("api/MailData/GetUserMailList", request);
        var responseDTO = await response.Content.ReadFromJsonAsync<GetUserMailListResponse>();

        return responseDTO;
    }
}

