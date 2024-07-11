using API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IConverter, Converter>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Read the ALLOWED_IPS environment variable
var allowedClientIPs = Environment.GetEnvironmentVariable("ALLOWED_CLIENT_IPS")?.Split(',');

if (allowedClientIPs != null && allowedClientIPs.Length > 0)
{
    app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins(allowedClientIPs));
}

// app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod()
//     .WithOrigins("http://localhost:3000","https://localhost:3000" ));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
