namespace WebAPIServer.Controllers.ManagingController;

using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using ManagingTool.Shared.DTO;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class Auth : ControllerBase
{
    readonly ILogger<ItemData> _logger;
    readonly HttpClient _httpClient;

    public Auth(ILogger<ItemData> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    [Authorize]
    [HttpGet]
    public ErrorCode Post()
    {
        return ErrorCode.None;
    }

    [HttpPost("Login")]
    public async Task<ManagingLoginResponse> Login(ManagingLoginRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("ManagingLogin", request);
        var responseDTO = await response.Content.ReadFromJsonAsync<ManagingLoginResponse>();

        if (responseDTO == null)
        {
            // errorlog
        }

        return responseDTO;
    }
}