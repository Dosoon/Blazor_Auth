using System.Collections.Generic;
using System.Data;
using Google.Protobuf.Collections;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Runtime.Intrinsics.X86;
using System.Xml;
using MySqlConnector;
using SqlKata.Execution;
using WebAPIServer.DataClass;
using ZLogger;
using WebAPIServer.Util;

namespace WebAPIServer.DbOperations;

public class ManagingDb : IManagingDb
{
    // 데이터베이스에서 마스터데이터 가져오기
    // 이후에는 마스터데이터와 관련된 것들은 여기서 사용

    readonly ILogger<MasterDb> _logger;
    IDbConnection _dbConn;
    QueryFactory _queryFactory;

    public ManagingDb(ILogger<MasterDb> logger, IConfiguration configuration)
    {
        _logger = logger;

        var DbConnectString = configuration.GetSection("DBConnection")["ManagingDb"];
        _dbConn = new MySqlConnection(DbConnectString);

        var compiler = new SqlKata.Compilers.MySqlCompiler();
        _queryFactory = new SqlKata.Execution.QueryFactory(_dbConn, compiler);
    }

    public Task<ErrorCode> Init()
    {
        throw new NotImplementedException();
    }

    public async Task<Tuple<ErrorCode, ManagingAccount>> GetLoginUserData(string email, string pwd)
    {
        try
        {
            // Email과 PW가 일치하는 회원 정보를 가져옴
            var managingUserData = await _queryFactory.Query("managing_account")
                                                      .Where("Email", email)
                                                      .Where("Password", pwd)
                                                      .FirstOrDefaultAsync<ManagingAccount>();

            return new Tuple<ErrorCode, ManagingAccount>( ErrorCode.None, managingUserData );
        }
        catch (Exception ex)
        {
            var errorCode = ErrorCode.ManagingGetLoginUserDataException;

            _logger.ZLogError(LogManager.MakeEventId(errorCode), ex, "GetLoginUserData Exception");

            return new Tuple<ErrorCode, ManagingAccount>( errorCode, new ManagingAccount() );
        }
    }

    public async Task<ErrorCode> UpdateRefreshToken(long accountId, string refreshToken)
    {
        try
        {
            // AccountId에 해당하는 유저의 리프레시 토큰을 갱신
            var affectedRow = await _queryFactory.Query("managing_account")
                                                 .Where("AccountId", accountId)
                                                 .UpdateAsync(new { RefreshToken = refreshToken });

            if (affectedRow == 0)
            {
                return ErrorCode.UpdateRefreshTokenFail;
            }

            return ErrorCode.None;
        }
        catch (Exception ex)
        {
            var errorCode = ErrorCode.ManagingUpdateRefreshTokenException;

            _logger.ZLogError(LogManager.MakeEventId(errorCode), ex, "UpdateRefreshToken Exception");

            return errorCode;   
        }
    }

    public async Task<string> GetRefreshTokenByAccountId(long accountId)
    {
        try
        {
            // AccountId에 해당하는 유저의 리프레시 토큰을 가져옴
            var token = (await _queryFactory.Query("managing_account")
                                            .Where("AccountId", accountId)
                                            .Select("RefreshToken")
                                            .GetAsync<string>()).FirstOrDefault();

            if (token == null || token == string.Empty)
            {
                return "";
            }

            return token;
        }
        catch (Exception ex)
        {
            var errorCode = ErrorCode.GetRefreshTokenByAccountIdException;

            _logger.ZLogError(LogManager.MakeEventId(errorCode), ex, "GetRefreshTokenByAccountId Exception");

            return "";
        }
    }
}
