var builder = WebApplication.CreateBuilder(args);
builder.Services.AddProblemDetails();
var app = builder.Build();

// API skeleton only. See requirement/03-http-api.md for endpoints and authentication.
// Do not add unauthenticated printing endpoints. Future routes: /api/v1/jobs and /api/v1/printers.
app.MapGet("/health/live", () => Results.Ok(new { status = "alive", printing = "not-implemented" }));
app.Run();

public partial class Program { }
