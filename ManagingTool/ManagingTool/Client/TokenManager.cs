using System.Net.Http.Headers;
using System.Net.Http;
using Microsoft.JSInterop;

namespace ManagingTool.Client
{
    public class TokenManager
    {
        private readonly IJSRuntime _jsRuntime;

        public TokenManager(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<(string, string)> GetTokensFromSessionStorage()
        {
            var accessToken = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "accesstoken");
            var refreshToken = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "refreshtoken");

            return (accessToken, refreshToken);
        }

        public async Task UpdateAccessTokenIfPresent(HttpResponseMessage res)
        {
            if (res.Headers.TryGetValues("X-NEW-ACCESS-TOKEN", out var newAccessTokenEnum))
            {
                var newAccessToken = newAccessTokenEnum.FirstOrDefault();
                if (newAccessToken != null || newAccessToken != string.Empty)
                {
                    await SetNewAccessTokenToSessionStorage(newAccessToken!);
                }
            }
        }

        public async Task SetNewAccessTokenToSessionStorage(string token)
        {
            await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "accesstoken", token);
        }
    }
}
