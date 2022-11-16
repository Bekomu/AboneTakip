using AboneTakip.API.Autofac;
using AboneTakip.API.Extensions;
using AboneTakip.Business.AutoMapper.Profiles;
using AboneTakip.Business.EntityValidation.Concrete.Customers;
using AboneTakip.DataAccess.EntitiyFramework.Context;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AboneTakipDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConStr")));


builder.Services.Configure<ApiBehaviorOptions>(o =>
{
    o.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddAutoMapper(typeof(IProfile).Assembly);

//builder.Services.AddRepositoryServices();
//builder.Services.AddBusinessServices();

// AUTOFAC
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerbuilder => containerbuilder.RegisterModule(new ServiceModuleExtension()));
// AUTOFAC

builder.Services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateCustomerValidator>());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
