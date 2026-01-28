var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Root page for WebFromCli.");
app.MapGet("/who", () => "Denis Andriiuk");
app.MapGet("/time", () => DateTime.Now.ToString("HH:mm:ss"));

app.Run();
