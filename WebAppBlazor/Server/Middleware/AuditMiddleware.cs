using CustomLoggers.AuditLog;
using Domain.Common;
using Domain.Common.Interfaces;
using Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebAppBlazor.Server.AppSettings;

namespace WebAppBlazor.Server.Middleware;

public class AuditMiddleware
{
    private readonly RequestDelegate _next;

    public AuditMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, IGetClientInfo getClientInfo, IAuditInfo auditInfo, IAuditlogStorage logger, IOptions<AuditSetting> auditSettingOption)
    {
        AuditSetting auditSetting = auditSettingOption.Value;
        var stopwatch = Stopwatch.StartNew();
        try
        {
            auditInfo.ExecutionTime = DateTime.Now;
            if (httpContext is not null)
            {
                foreach (var key in httpContext.Request.Headers.Keys)
                {
                    if (auditSetting.ExceptionHeader.Any(x => String.Compare(key, x, true) == 0).Not())
                    {
                        auditInfo.ExtraProperties.Add(key, httpContext.Request.Headers[key].ToString());
                    }
                }
            }

            auditInfo.ClientIpAddress = getClientInfo.ClientIp;
            auditInfo.Url = httpContext?.Request?.Path.Value;
            auditInfo.HttpMethod = httpContext?.Request?.Method;

            await _next(httpContext);

            auditInfo.HttpStatusCode = httpContext?.Response?.StatusCode;
        }
        catch (Exception ex)
        {
            auditInfo.HttpStatusCode = httpContext?.Response?.StatusCode;

            auditInfo.Exception = ex;

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            httpContext.Response.ContentType = "application/json";

            var details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An error occurred while processing your request.",
                Detail = "An error occurred while processing your request. Please Contact With Administrator",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            };

            string jsonString = JsonSerializer.Serialize(details);

            await httpContext.Response.WriteAsync(jsonString, Encoding.UTF8);
        }
        finally
        {

            stopwatch.Stop();
            auditInfo.ExecutionDuration = Convert.ToInt32(stopwatch.Elapsed.TotalMilliseconds);

            if ((auditSetting.ExceptionIP?.Contains(getClientInfo.ClientIp) ?? false).Not() &&
                (auditSetting.ExceptionUrl?.Contains(httpContext.Request.Path) ?? false).Not())
            {
                _ = logger.SaveAsync(auditInfo);
            }
        }

    }
}
