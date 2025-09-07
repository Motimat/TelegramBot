using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Context;
using TelegramBot.Service.Interfaces;

namespace TelegramBot.Service.Hub
{
    public class TelegramHubService : BackgroundService
    {

        private readonly IRouteConfiguration _routerConfiguration;
        private Thread thread;
        private CancellationTokenSource _cts = new();
        private CancellationToken Token => _cts.Token;
        private static readonly object lockObj = new object();

        private readonly IServiceScopeFactory _scopeFactory;
        private readonly TelegramContext _context;

        private bool _isOn = false;
        public bool IsOn { get { return _isOn; } }

        public TelegramHubService(IRouteConfiguration routerConfiguration, IServiceScopeFactory scopeFactory)
        {

            _scopeFactory = scopeFactory;
            _routerConfiguration = routerConfiguration;
            var scope = _scopeFactory.CreateScope();
            _context = scope.ServiceProvider.GetRequiredService<TelegramContext>();
            _context.Database.Migrate();
        }
        public void Start()
        {
            lock (lockObj)
            {
                _isOn = true;
                if (thread != null && thread.IsAlive)
                    return;

                _cts = new CancellationTokenSource();

                thread = new Thread(() => Hub(Token));
                thread.IsBackground = true;
                thread.Start();
            }
        }
        public void Close()
        {
            _isOn = false;
            lock (lockObj)
            {
                _cts.Cancel();

                TelegramBotServiceConfiguration.BotClient.Close();
                if (thread != null && thread.IsAlive)
                    thread.Join();
            }
        }

        public void Hub(CancellationToken cancellationToken)
        {
            TelegramBotServiceConfiguration.BotClient.StartReceiving(updateHandle, errorHandle, cancellationToken: cancellationToken);

        }
        public async Task updateHandle(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message is not null)
            {
                long userId = update.Message.From!.Id;
                var user = _context.Users.Any(u => u.UserId == userId);
                if (user == false)
                {
                    _context.Users.Add(new Entities.Tables.TelegramUser { UserId = userId });
                    _context.SaveChanges();
                }

                if (await _routerConfiguration.ProccessForwarder(userId))
                {
                    await TelegramBotServiceConfiguration.BotClient.SendMessage(update.Message.Chat.Id, "شما در کانال عضو هستید");
                }
                else
                {
                    await TelegramBotServiceConfiguration.BotClient.SendMessage(update.Message.Chat.Id, "شما در کانال عضو نیستید");
                }
            }

            return;
        }

        public Task errorHandle(ITelegramBotClient botClient, Exception ex, CancellationToken cancellation)
        {

            return Task.CompletedTask;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
    }
}
