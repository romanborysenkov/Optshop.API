using OptShopAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OptShopAPI.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Security.Cryptography.X509Certificates;
using System.Net;

var builder = WebApplication.CreateBuilder(args);


builder.WebHost.ConfigureKestrel((context, serverOptions)=>
{
    HostConfig.CertPath = context.Configuration["CertPath"];
    HostConfig.CertPassword = context.Configuration["CertPassword"];
serverOptions.ListenAnyIP(5000);
    serverOptions.ListenAnyIP(5001, listOpt =>
    {
        listOpt.UseHttps(HostConfig.CertPath, "HelloMary");
    });

     
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DataContext"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
*/

builder.Services.AddDbContext<DataContext>(options =>
{
   // options.UseSqlServer(builder.Configuration.GetConnectionString("OptshopConnectionConnectionString"));
    options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnectionString"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddCors();

/*
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null; // Use camelCase or other naming policy
    options.JsonSerializerOptions.WriteIndented = true;
});
*/

var app = builder.Build();

app.UseCors(options => options.WithOrigins("http://localhost:5700", "http://127.0.0.1:5700", "http://192.168.1.14:5700", "https://optshop-e7eb5.web.app", "https://xpantek.net").AllowAnyMethod().AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsProduction())
{

    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI();


app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "Images")),
    RequestPath = "/Images"
});

BotService botService = new BotService();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthorization();
app.UseAuthentication();


app.MapControllers();
app.Run();




public static class HostConfig{
    public static string CertPath{get;set;}
     public static string CertPassword { get; set; }
}