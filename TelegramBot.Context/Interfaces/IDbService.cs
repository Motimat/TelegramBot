using TelegramBot.Entities.Tables;

namespace TelegramBot.Context.Interfaces
{
    public interface IDbService
    {
        public bool AddUser(TelegramUser telegramUser);

        public bool RemoveUser(TelegramUser telegramUser);
        public bool RemoveUser(long telegramUserId);

        public TelegramUser GetUser(long telegramUserId);

        public List<TelegramUser> GetAllUsers();
    }
}
