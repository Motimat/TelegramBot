namespace TelegramBot.Service.Interfaces
{
    public interface ICheckService
    {
        public Task<bool> IsTheBotAdminInChannel(long channelId);
        public Task<bool> IsUserInTheChannel(long channelId, long userId);
    }
}
