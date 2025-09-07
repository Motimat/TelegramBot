using TelegramBot.Context;
using TelegramBot.Context.EfServices;
using TelegramBot.Context.Interfaces;
using TelegramBot.Entities.Configs;
using TelegramBot.Server.Components;
using TelegramBot.Service;
using TelegramBot.Service.Hub;
using TelegramBot.Service.Interfaces;
using TelegramBot.Service.Router;
using TelegramBot.Service.Services;
var builder = WebApplication.CreateBuilder(args);

TelegramBotConfig config = new TelegramBotConfig() { Token = "8448121997:AAE26xV3QzA6DlFi-Fr2IMS6Z22SOM8LM-Y", ChannelUserName = "@jcc_sample" };




// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.AddTelegram(config);
builder.Services.AddSingleton<TelegramBotConfig>(config);
builder.Services.AddSingleton<ICheckService, TelegramChannelService>();
builder.Services.AddSingleton<IRouteConfiguration, RouterConfiguration>();
builder.Services.AddSingleton<TelegramHubService>();
builder.Services.AddHostedService(sp => sp.GetRequiredService<TelegramHubService>());
builder.Services.AddScoped<IDbService, DbService>();

builder.Services.AddDbContext<TelegramContext>();



var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();


