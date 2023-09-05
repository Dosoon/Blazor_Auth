using WebAPIServer.DbOperations;
using WebAPIServer.Middleware;
using WebAPIServer.Util;
using ZLogger;
using IdGen.DependencyInjection; //https://github.com/RobThree/IdGen
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

var defaultSetting = new DefaultSetting();
configuration.Bind("DefaultSetting", defaultSetting);
builder.Services.AddSingleton(defaultSetting);

builder.Services.AddTransient<IAccountDb, AccountDb>();
builder.Services.AddTransient<IGameDb, GameDb>();
builder.Services.AddSingleton<IRedisDb, RedisDb>();
builder.Services.AddSingleton<IMasterDb, MasterDb>();
builder.Services.AddSingleton<IManagingDb, ManagingDb>();
builder.Services.AddIdGen((int)defaultSetting.GeneratorId);

builder.Services.AddSingleton<TokenManager>();
TokenManager.Init(configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .AllowAnyMethod()
            .AllowCredentials()
            .SetIsOriginAllowed((host) => true)
            .AllowAnyHeader());
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,    // 토큰 유효성 검증 여부
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SigningKey"]!)) // 비밀 서명 키
            };

            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = async context =>
                {
                    var handler = context.HttpContext.RequestServices.GetRequiredService<TokenManager>();
                    await handler.OnAuthenticationFailedHandler(context, options);
                }
            };
        });

builder.Services.AddControllers();

LogManager.SetLogging(builder);

var app = builder.Build();


var redisDb = app.Services.GetRequiredService<IRedisDb>();
await redisDb.Init();

var masterDb = app.Services.GetRequiredService<IMasterDb>();
await masterDb.Init();

// 로그인 이후 유저 인증
//app.UseMiddleware<WebAPIServer.Middleware.CheckUserAuth>();

app.UseRouting();
app.UseCors("CorsPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run(configuration["ServerAddress"]);


public class DefaultSetting
{
	public Int64 MailsPerPage { get; set; }
	public Int64 GeneratorId { get; set; }
}