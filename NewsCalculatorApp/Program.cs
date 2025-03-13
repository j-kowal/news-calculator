using Microsoft.AspNetCore.HttpLogging;
using NewsCalculatorApp.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
// available on openapi/v1.json
builder.Services.AddOpenApi();
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields =
        HttpLoggingFields.RequestPath |
        HttpLoggingFields.ResponseStatusCode |
        HttpLoggingFields.RequestMethod |
        HttpLoggingFields.RequestBody |
        HttpLoggingFields.ResponseBody;
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
    logging.CombineLogs = true;
});
builder.Services.AddControllers().AddJsonOptions(options => {
    options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
});
builder.Services.AddScoped<NewsService>();

WebApplication app = builder.Build();
app.UseStatusCodePages();
app.UseHttpLogging();
app.UseCors(policy =>
{
    policy.WithOrigins("http://localhost:3000")
        .WithMethods(HttpMethods.Post, HttpMethods.Options)
        .AllowAnyHeader();
});
app.MapControllers();
app.MapOpenApi();
app.Run();