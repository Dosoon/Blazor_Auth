namespace WebAPIServer.Controllers.ManagingController;

using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using ManagingTool.Shared.DTO;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.Text;
using System.Text.Json;

public class BaseController : ControllerBase
{
    protected void SerializeReqBody(ref HttpRequestMessage reqMsg, Object reqBody)
    {
        string requestBody = JsonSerializer.Serialize(reqBody);
        reqMsg.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
    }

    protected void AttachTokensToRequestHeader(ref HttpRequestMessage req)
    {
        var accessToken = HttpContext.Request.Headers["Authorization"];
        var refreshToken = HttpContext.Request.Headers["refresh_token"].FirstOrDefault();

        req.Headers.Authorization = AuthenticationHeaderValue.Parse(accessToken);
        req.Headers.Remove("refresh_token");
        req.Headers.Add("refresh_token", refreshToken);
    }

    protected void AttachNewTokenToResponseIfPresent(ref HttpResponseMessage res)
    {
        if (res.Headers.TryGetValues("X-NEW-ACCESS-TOKEN", out var newAccessTokenEnum))
        {
            var newAccessToken = newAccessTokenEnum.FirstOrDefault();
            if (newAccessToken != null || newAccessToken != string.Empty)
            {
                HttpContext.Response.Headers.Remove("X-NEW-ACCESS-TOKEN");
                HttpContext.Response.Headers.Add("X-NEW-ACCESS-TOKEN", newAccessToken);
            }
        }
    }
}