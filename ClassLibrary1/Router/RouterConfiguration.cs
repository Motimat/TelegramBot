using System.Reflection;
using Telegram.Bot;
using TelegramBot.Service.Interfaces;

namespace TelegramBot.Service.Router
{
    public sealed class RouterConfiguration : IRouteConfiguration
    {
        private readonly ICheckService _checkService;

        public RouterConfiguration(ICheckService checkService)
        {
            _checkService = checkService;
        }
        public void CallController(string comment)
        {
            Type type = typeof(RouterController);
            MethodInfo[] methods = type.GetMethods()
               .Where(m => m.GetCustomAttributes(typeof(Route), false)
               .Any(attr => comment == ((Route)attr).Value))
               .ToArray();
            foreach (var method in methods)
            {
                method.Invoke(null, null);
            }
        }

        public async Task<bool> ProccessContact(long userId)
        {
            var method = _checkService.IsUserInTheChannel(TelegramBotServiceConfiguration.Config!.ChannelId, userId);
            method.Wait();
            return method.Result;
        }

        public async Task<bool> ProccessForwarder(long userId)
        {

            var channel = await TelegramBotServiceConfiguration.BotClient!.GetChat(TelegramBotServiceConfiguration.Config.ChannelUserName);
            var method = await _checkService.IsUserInTheChannel(channel.Id, userId);
            return method;

        }
    }




}
