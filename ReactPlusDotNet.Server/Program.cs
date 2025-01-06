using Microsoft.Extensions.DependencyInjection;
using ReactPlusDotNet.Server.Extensions;
using ReactPlusDotNet.Server.Interfaces;
using ReactPlusDotNet.Server.Storage;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServiceCollection(builder.Configuration);
var app = builder.Build();
app.Services.AddCustomService(builder.Configuration);
app.UseDefaultFiles();
app.UseStaticFiles();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthorization();

app.MapControllers();


app.MapFallbackToFile("/index.html");

app.Run();
