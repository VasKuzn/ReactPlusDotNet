using ReactPlusDotNet.Server.Storage;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ContactStorage>();
builder.Services.AddCors(
    opt => opt.AddPolicy("CorsPolicy", policy => { policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("https://localhost:5173");}) // если на другой комп и в другой среде, то пишем dotnet run https://localhost:5173 а в withorigins(args[0])
    );
var app = builder.Build();

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
