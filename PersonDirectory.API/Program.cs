using FluentValidation.AspNetCore;
using PersonDirectory.API.MappingProfile;
using PersonDirectory.Domain.Interfaces;
using PersonDirectory.Infrastructure;
using PersonDirectory.Infrastructure.Repositories;
using PersonDirectory.Infrastructure.UnitOfWorks;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<PersonMappingProfile>();
});
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.Load("PersonDirectory.Application"));
});


builder.Services.AddScoped<IPersonWriteRepository, PersonWriteRepository>();
builder.Services.AddScoped<IPersonReadRepository, PersonReadRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PersonDirectory API V1");
        c.RoutePrefix = "swagger";
    });

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
