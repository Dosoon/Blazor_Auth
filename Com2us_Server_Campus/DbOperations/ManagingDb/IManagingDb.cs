using System;
using WebAPIServer.DataClass;
using WebAPIServer.ReqRes;

namespace WebAPIServer.DbOperations;

public interface IManagingDb
{
    public Task<ErrorCode> Init();
    public Task<Tuple<ErrorCode, ManagingAccount>> GetLoginUserData(string email, string pwd);
    public Task<ErrorCode> UpdateRefreshToken(Int64 accountId, string refreshToken);
}

