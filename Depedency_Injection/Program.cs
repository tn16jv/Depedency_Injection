using Depedency_Injection.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register services in the DI container
// Singleton: registers once and is shared across all requests
// Scoped: created once per client request and is shared across all components of it
// Transient: a [service lifetime] that gets created each time that it's requested
builder.Services.AddSingleton<IMessageService, EmailService>();
builder.Services.AddSingleton<NotificationService>();

var app = builder.Build();

// Get an instance of NotificationService from the DI container
var notificationService = app.Services.GetRequiredService<NotificationService>();
string test = notificationService.Notify("Program / Main test");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
