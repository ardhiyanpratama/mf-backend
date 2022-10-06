using Autofac.Extensions.DependencyInjection;
using Autofac;
using instaapp_backend.Core.IConfiguration;
using mf_backend.Data;
using mf_backend.Helper;
using mf_backend.Models;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using mf_backend.Policies;
using mf_backend.Infrastructure.AutofacModules;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        containerBuilder.RegisterModule(new ServiceModule());
    });

builder.Services.AddControllers();
builder.Services.AddDbContext<MfContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers().AddJsonOptions(opts =>
{
    opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddHttpClient("polly")
    .AddPolicyHandler(request => request.Method == HttpMethod.Get ? new ClientPolicy().ImmediateHttpRetry : new ClientPolicy().ImmediateHttpRetry);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection(nameof(AppSettings)));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddCors(builder =>
{
    builder.AddPolicy("Cors", policy =>
    {
        policy.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Cors");

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
