using System;
using Microsoft.EntityFrameworkCore;
using Data;
using Business.Interfaces;
using Business.Services;
using Data.Interfaces;
using Data.Repositories;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddDbContext<Data.Contexts.DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICompanyRoleRepository, CompanyRoleRepository>();
builder.Services.AddScoped<ICompanyServiceRepository, CompanyServiceRepository>();
builder.Services.AddScoped<IContactPersonRepository, ContactPersonRepository>();
builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
builder.Services.AddScoped<ICustomerReferenceRepository, CustomerReferenceRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectStatusRepository, ProjectStatusRepository>();
builder.Services.AddScoped<ITimeframeRepository, TimeframeRepository>();
builder.Services.AddScoped<IUnitRepository, UnitRepository>();

//builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

builder.Services.AddScoped<IContactPersonService, ContactPersonService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ITimeframeService, TimeframeService>();
builder.Services.AddScoped<ICompanyServiceService, CompanyServiceService>();
builder.Services.AddScoped<ICompanyRoleService, CompanyRoleService>();
builder.Services.AddScoped<ICurrencyService, CurrencyService>();
builder.Services.AddScoped<IUnitService, UnitService>();
builder.Services.AddScoped<IProjectStatusService, ProjectStatusService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
