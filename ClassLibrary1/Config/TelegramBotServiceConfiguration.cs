using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using TelegramBot.Entities.Configs;

namespace TelegramBot.Service
{
    public static class TelegramBotServiceConfiguration
    {
        private static readonly object _lock = new object();
        private static TelegramBotConfig? _config;
        internal static TelegramBotConfig Config { get { return _config!; } }
        private static TelegramBotClient? _botClient;
        internal static TelegramBotClient BotClient { get { return _botClient!; } }


        public static void AddTelegram(this IServiceCollection serviceProvider, TelegramBotConfig config)
        {
            _config = config;
            _botClient = new TelegramBotClient(_config.Token);
        }




    }
}
