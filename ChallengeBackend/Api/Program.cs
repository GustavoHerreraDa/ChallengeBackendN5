using Challenge.Application;
using Challenge.Application.Common.Interfaces;
using Challenge.Infrastructure;
using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using UnitTest.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IPermissionsRepository,PermissionsRepository>();
builder.Services.AddTransient<DbContext, ApplicationDbContext>();
builder.Services.AddDbContext<ApplicationDbContext>((provider, options) => {
    IConfiguration config = provider.GetRequiredService<IConfiguration>();
    string connectionString = config.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString);
});

var kafkaConfig = builder.Configuration.GetSection("Kafka");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

builder.Services.AddSingleton<ProducerConfig>(new ProducerConfig
{
    BootstrapServers = kafkaConfig["BootstrapServers"],
    ClientId = kafkaConfig["ClientId"].ToString()
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
