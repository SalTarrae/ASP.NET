var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

int requestCount = 0;
const string ServerApiKey = "c";


app.Use(async (context, next) => {
	Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {context.Request.Method} {context.Request.Path}");
	await next();
});

app.Use(async (context, next) => {
	if (!context.Request.Headers.TryGetValue("X-API-KEY", out var apiKey) || apiKey != ServerApiKey) {
		context.Response.StatusCode = StatusCodes.Status403Forbidden;
		context.Response.ContentType = "text/plain; charset=utf-8";
		await context.Response.WriteAsync("403 Forbidden: invalid or missing X-API-KEY.");
		return;
	}

	await next();
});

app.Use(async (context, next) => {
	if (context.Request.Query.ContainsKey("custom")) {

		context.Response.StatusCode = StatusCodes.Status200OK;
		context.Response.ContentType = "text/plain; charset=utf-8";
		await context.Response.WriteAsync("You’ve hit a custom middleware!\n");
		return;
	}

	await next();
});

app.Use(async (context, next) => {
	if (context.Request.Path == "/favicon.ico") {
		context.Response.StatusCode = StatusCodes.Status204NoContent;
		return;
	}

	var current = Interlocked.Increment(ref requestCount);

	context.Response.ContentType = "text/plain; charset=utf-8";
	await next();
	await context.Response.WriteAsync($"The amount of processed requests is {current}.");
});

app.MapGet("/", () => "MiddlewareSandbox root.");

app.Run();
