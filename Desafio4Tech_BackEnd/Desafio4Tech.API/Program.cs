using Desafio4Tech.Aplicacao.AutoMapper;
using Desafio4Tech.Aplicacao.Facade;
using Desafio4Tech.Aplicacao.Interface.Facade;
using Desafio4Tech.Dominio.Interface.Repository;
using Desafio4Tech.Dominio.Interface.Servico;
using Desafio4Tech.Infra.Data;
using Desafio4Tech.Infra.UnitOfWork;
using Desafio4Tech.Servico.Servico;
using DotNetEnv;
using Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

Env.Load();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DataBase"));
});

builder.Services.AddControllers().AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())); ;

builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddAutoMapper(typeof(PlanoProfile).Assembly);
builder.Services.AddAutoMapper(typeof(BeneficiarioProfile).Assembly);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IBeneficiarioRepository, BeneficiarioRepository>();
builder.Services.AddScoped<IBeneficiarioFacade, BeneficiarioFacade>();
builder.Services.AddScoped<IBeneficiarioServico, BeneficiarioServico>();

builder.Services.AddScoped<IPlanoRepository, PlanoRepository>();
builder.Services.AddScoped<IPlanoFacade, PlanoFacade>();
builder.Services.AddScoped<IPlanoServico, PlanoServico>();

var app = builder.Build();

app.UseCors("AllowAngularDev");

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
