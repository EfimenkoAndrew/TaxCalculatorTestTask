using HttpClients;
using Library.Infrastructure.Common;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<SystemHttpClientsSettings>(builder.Configuration.GetSection(nameof(SystemHttpClientsSettings)));
var systemHttpClientsSettings = builder.Configuration.GetSection(nameof(SystemHttpClientsSettings)).Get<SystemHttpClientsSettings>()
    ?? throw new AggregateException($"Settings: '{nameof(SystemHttpClientsSettings)}' is not found in configurations.");

builder.Services.RegisterCalculationsHttpClient(systemHttpClientsSettings.TestTast);

// for bearer token in future
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler("/Home/Error");
// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
