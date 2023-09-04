using System.Net.Http.Headers;
using System.Net.Http.Json;
using ManagingTool.Shared.DTO;
using Microsoft.JSInterop;

namespace ManagingTool.Client;

public class ItemService
{
    public static HttpClient _httpClient { get; set; }
    private readonly IJSRuntime _jsRuntime;

    public ItemService(IJSRuntime jsRuntime)
	{
		_jsRuntime = jsRuntime;
	}

	public async Task<GetItemTableResponse> GetItemTable()
    {
        var request = new GetItemTableRequest();

		var sessionToken = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "accesstoken");
		_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessionToken);

		var response = await _httpClient.PostAsJsonAsync("api/ItemData/GetItemTable", request);
        var responseDTO = await response.Content.ReadFromJsonAsync<GetItemTableResponse>();

        return responseDTO;
    }

    public async Task<GetUserItemListResponse> GetUserItemList(string searchType, Int64 searchValue)
    {
        var request = new GetUserItemListRequest
        {
            SearchType = searchType,
            SearchValue = searchValue
        };

		var sessionToken = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "accesstoken");
		_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessionToken);

		var response = await _httpClient.PostAsJsonAsync("api/ItemData/GetUserItemList", request);
        var responseDTO = await response.Content.ReadFromJsonAsync<GetUserItemListResponse>();

        if (responseDTO.errorCode != ErrorCode.None)
        {
            //errorlog
        }

        return responseDTO;
    }

    
    public async Task<RetrieveUserItemResponse> RetrieveUserItem(IEnumerable<UserItem> selectedRows, MailForm? mailForm)
    {
        List<Tuple<Int64, Int64>> selectedItemList = new List<Tuple<Int64, Int64>>();

        foreach (var item in selectedRows)
        {
            selectedItemList.Add(new Tuple<Int64, Int64>(item.ItemId, item.UserId));
        }

        var request = new RetrieveUserItemRequest
        {
            SelectedItemList = selectedItemList,
            MailForm = mailForm
        };

		var sessionToken = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "accesstoken");
		_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessionToken);

		var response = await _httpClient.PostAsJsonAsync("api/ItemData/RetrieveUserItem", request);
        var responseDTO = await response.Content.ReadFromJsonAsync<RetrieveUserItemResponse>();

        if (responseDTO.errorCode != ErrorCode.None)
        {
            //errorlog
        }

        return responseDTO;
    }
    
}

