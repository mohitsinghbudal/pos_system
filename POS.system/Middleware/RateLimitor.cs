using Microsoft.Extensions.Caching.Memory;
using System.Net;
using System.Text.Json;
using System.Threading.RateLimiting;

namespace POS.system.Middleware
{
    //public class RateLimitor
    //{
    //    private readonly RequestDelegate _next;
    //    private readonly IMemoryCache _cache;
    //    private readonly ILogger<RateLimitor> _logger;

    //    public RateLimitor (RequestDelegate next, IMemoryCache cache, ILogger<RateLimiter> logger)
    //    {
    //        _next = next;
    //        _cache = cache;
    //        _logger = logger;
    //    }
    //    public async Task InvokeAsync(HttpContext context)
    //    {
    //        var clientIp = GetClientIpAddress(context);
    //        var cacheKey = $"rate_limit_{clientIp}";

    //        var requestLog = _cache.GetOrCreate(cacheKey, entry =>
    //        {
    //            entry.AbsoluteExpirationRelativeToNow = WindowTime;
    //            return new List<DateTime>();
    //        }) ?? new List<DateTime>();

    //        var now = DateTime.UtcNow;

    //        lock (requestLog)
    //        {
    //            requestLog.RemoveAll(timestamp => now - timestamp > WindowTime);

    //            if (requestLog.Count >= MaxRequests)
    //            {
    //                _logger.LogWarning("Rate limit exceeded for IP: {ClientIp}", clientIp);

    //                context.Response.ContentType = "application/json";
    //                context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
    //                context.Response.Headers["Retry-After"] = WindowTime.TotalSeconds.ToString();

    //                var response = new
    //                {
    //                    StatusCode = context.Response.StatusCode,
    //                    Message = "Too many requests. Please try again later.",
    //                    RetryAfterSeconds = (int)WindowTime.TotalSeconds
    //                };

    //                var jsonResponse = JsonSerializer.Serialize(response);
    //                return context.Response.WriteAsync(jsonResponse);
    //            }

    //            requestLog.Add(now);
    //        }

    //        context.Response.Headers["X-RateLimit-Limit"] = MaxRequests.ToString();
    //        context.Response.Headers["X-RateLimit-Remaining"] = (MaxRequests - requestLog.Count).ToString();

    //        await _next(context);
    //    }

    //    private static string GetClientIpAddress(HttpContext context)
    //    {
    //        if (context.Request.Headers.TryGetValue("X-Forwarded-For", out var forwardedFor))
    //        {
    //            var ip = forwardedFor.ToString().Split(',').FirstOrDefault()?.Trim();
    //            if (!string.IsNullOrEmpty(ip)) return ip;
    //        }

    //        return context.Connection.RemoteIpAddress?.ToString() ?? "unknown_ip";
    //    }
    //}

}

