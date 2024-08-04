using Microsoft.AspNetCore.Server.Kestrel.Core;
using MainClient.Hubs;
using AzureUploader.Services;
using MainClient.Interfaces;
using MainClient.Services;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CrossCuttingLayer;
using GreenPipes;
using System;
using MainClient.Extensions;
using MainClient.Hubs;
var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureIISServerOptions();
builder.Services.ConfigureKestrelServerOptions();
builder.Services.ConfigureFormOptions();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigurFileStorageService(builder.Configuration);
builder.Services.ConfigurBlockUploaderService(builder.Configuration);
builder.Services.ConfigurBlobAccessService(builder.Configuration);
builder.Services.ConfigureMassTransit();
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddControllersWithViews();
builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapHub<ChatHub>("/chatHub");
 
app.MapRazorPages();

app.Run();
