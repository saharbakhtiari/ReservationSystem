using Domain.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace WebAppBlazor.Server.Services
{
    public class GetClientInfo : IGetClientInfo
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public GetClientInfo(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string ClientIp
        {
            get { return GetClientIpAddress(); }
        }
        private string GetClientIpAddress()
        {
            var request = _contextAccessor.HttpContext.Request;
            string varResult = string.Empty;
            try
            {
                if (request.Headers.ContainsKey("X-Forwarded-For"))
                {
                    varResult = request.Headers["X-Forwarded-For"].ToString().Split(',').LastOrDefault()?.Trim();
                }
                varResult = string.IsNullOrEmpty(varResult) ? request.HttpContext.Connection.RemoteIpAddress.ToString() : varResult;
                varResult = varResult == "::1" || varResult == "localhost" ? "127.0.0.1" : varResult;
            }
            catch (Exception ex)
            {
                varResult = ex.Message;
            }

            return varResult;
        }

        public string GetHeader(string headerName)
        {
            var request = _contextAccessor.HttpContext.Request;
            return request.Headers[headerName];
        }
    }
}
