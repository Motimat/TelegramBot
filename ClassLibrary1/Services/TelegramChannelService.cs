using Telegram.Bot;
using TelegramBot.Service.Interfaces;

namespace TelegramBot.Service.Services
{
    public class TelegramChannelService : ICheckService
    {


        public async Task<bool> IsTheBotAdminInChannel(long channelId)
        {
            var me = await TelegramBotServiceConfiguration.BotClient!.GetMe();
            var chatMember = await TelegramBotServiceConfiguration.BotClient!.GetChatMember(channelId, me.Id);
            if (chatMember.Status == Telegram.Bot.Types.Enums.ChatMemberStatus.Administrator)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> IsUserInTheChannel(long channelId, long userId)
        {
            var isAdmin = await IsTheBotAdminInChannel(channelId);
            if (!isAdmin)
                return false;

            var isMember = await TelegramBotServiceConfiguration.BotClient.GetChatMember(channelId, userId);
            if (isMember.IsInChat)
            {
                return true;
            }
            return false;
        }
    }
}
