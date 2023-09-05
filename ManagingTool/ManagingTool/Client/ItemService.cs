using System.Net.Http.Headers;
using System.Net.Http.Json;
using ManagingTool.Shared.DTO;
using Microsoft.JSInterop;

namespace ManagingTool.Client;

public class ItemService : BaseService
{
    public static HttpClient _httpClient { get; set; }
    private readonly TokenManager _tokenManager;

    public ItemService(TokenManager tokenManager)
    {
        _tokenManager = tokenManager;
    }

    public async Task<GetItemTableResponse> GetItemTable()
    {
        var request = new GetItemTableRequest();
        var requestMessage = new HttpRequestMessage(HttpMethod.Post, "api/ItemData/GetItemTable");
        SerializeReqBody(ref requestMessage, request);

        var (accessToken, refreshToken) = await _tokenManager.GetTokensFromSessionStorage();
        AttachTokensToRequestHeader(ref requestMessage, accessToken, refreshToken);


        var response = await _httpClient.SendAsync(requestMessage);
        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            return new GetItemTableResponse { errorCode = ErrorCode.Unauthorized };
        }
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
        var requestMessage = new HttpRequestMessage(HttpMethod.Post, "api/ItemData/GetUserItemList");
        SerializeReqBody(ref requestMessage, request);

        var (accessToken, refreshToken) = await _tokenManager.GetTokensFromSessionStorage();
        AttachTokensToRequestHeader(ref requestMessage, accessToken, refreshToken);

        var response = await _httpClient.SendAsync(requestMessage);
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

        var requestMessage = new HttpRequestMessage(HttpMethod.Post, "api/ItemData/RetrieveUserItem");
        SerializeReqBody(ref requestMessage, request);

        var (accessToken, refreshToken) = await _tokenManager.GetTokensFromSessionStorage();
        AttachTokensToRequestHeader(ref requestMessage, accessToken, refreshToken);

        var response = await _httpClient.SendAsync(requestMessage);
        var responseDTO = await response.Content.ReadFromJsonAsync<RetrieveUserItemResponse>();

        if (responseDTO.errorCode != ErrorCode.None)
        {
            //errorlog
        }

        return responseDTO;
    }
}

