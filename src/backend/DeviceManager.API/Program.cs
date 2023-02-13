using DeviceManager.Repository;
using DeviceManager.Service;

var builder = WebApplication.CreateBuilder(args);

// Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
	builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

// Read config and get DB Type
var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
var databaseType = config.GetValue<string>("DatabaseType");

// Custom DI
if (databaseType == "json")
{
	builder.Services.AddScoped<IDeviceRepository, JsonDeviceRepository>(sp =>
	{
		return new JsonDeviceRepository("data.json");
	});
}
else
	throw new NotSupportedException("The given Database Type is not supported");

builder.Services.AddScoped<IDeviceService, DeviceService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors("corsapp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();