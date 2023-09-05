using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using System.Text;

namespace ManagingTool.Client
{
    public class BaseService
    {
        // Request Body를 JSON 직렬화하여 Body에 저장
        protected void SerializeReqBody(ref HttpRequestMessage reqMsg, Object reqBody)
        {
            string requestBody = JsonSerializer.Serialize(reqBody);
            reqMsg.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
        }

        // AccessToken과 RefreshToken을 RequestMessage 헤더에 추가
        protected void AttachTokensToRequestHeader(ref HttpRequestMessage req, string accessToken, string refreshToken)
        {
            req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            req.Headers.Remove("refresh_token");
            req.Headers.Add("refresh_token", refreshToken);
        }
    }
}
