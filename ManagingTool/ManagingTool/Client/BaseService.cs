using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using System.Text;

namespace ManagingTool.Client
{
    public class BaseService
    {
        protected void SerializeReqBody(ref HttpRequestMessage reqMsg, Object reqBody)
        {
            string requestBody = JsonSerializer.Serialize(reqBody);
            reqMsg.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
        }

        protected void AttachTokensToRequestHeader(ref HttpRequestMessage req, string accessToken, string refreshToken)
        {
            req.Headers.Authorization = AuthenticationHeaderValue.Parse(accessToken);
            req.Headers.Remove("refresh_token");
            req.Headers.Add("refresh_token", refreshToken);
        }
    }
}
