
using Abstraction.Command.Customer.Register;
using Api.Middleware;
using Application.Customer;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options => { options.JsonSerializerOptions.PropertyNameCaseInsensitive = true; });

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterCommandValidator>();



// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(new[]{
         typeof(Program).Assembly,
         typeof(RegisterCommandHandler).Assembly
        })
 );

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(connectionString: connectionString,
                          npgsqlOptionsAction: npgsqlOptions => npgsqlOptions.MigrationsAssembly("Api")));

builder.Services.AddScoped<ITransactionManager, TransactionManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlerMiddleware>();
if (app.Environment.EnvironmentName != "Test")
{
    app.UseMiddleware<TransactionMiddleware>();
}
app.MapControllers();

app.Run();

public partial class Program { }
