using QuickJob.Users.Api.DI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSettings();
builder.Services.AddExternalServices();
builder.Services.AddServiceCors();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddServiceAuthentication();
builder.Services.AddServiceSwaggerDocument();
builder.Services.AddSystemServices();

var app = builder.Build();

//app.UseDeveloperExceptionPage()
app.UseUnhandledExceptionMiddleware();
app.UseSwaggerUi3().UseOpenApi();
app.UseHttpsRedirection();
app.UseRouting();
app.UseServiceCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
