using System.Diagnostics;

public class StopwatchMiddleware
{
    private readonly RequestDelegate requestDelegate;
    private readonly ILogger<StopwatchMiddleware> logger;

    public StopwatchMiddleware(RequestDelegate requestDelegate, 
        ILogger<StopwatchMiddleware> logger)
    {
        this.requestDelegate = requestDelegate;
        this.logger = logger;
    }
    public async Task Invoke(HttpContext context)
    {
        var sw = new Stopwatch();
        sw.Start();
        await requestDelegate.Invoke(context);
        sw.Stop();
        logger.LogInformation($"Tiempo: {sw.ElapsedMilliseconds}");
    }
}

public static class StopwatchMiddlewareHelpers
{
    public static void UseStopwatch(this WebApplication app)
    {
        app.UseMiddleware<StopwatchMiddleware>();
    }
}