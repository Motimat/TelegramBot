using TelegramBot.Context.Interfaces;
using TelegramBot.Entities.Tables;

namespace TelegramBot.Context.EfServices
{
    public class DbService : IDbService
    {
        private readonly TelegramContext _context;
        public DbService(TelegramContext context)
        {
            _context = context;
        }

        public bool AddUser(TelegramUser telegramUser)
        {
            try
            {
                _context.Users.Add(telegramUser);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public List<TelegramUser> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public TelegramUser GetUser(long telegramUserId)
        {
            throw new NotImplementedException();
        }

        public bool RemoveUser(TelegramUser telegramUser)
        {
            try
            {
                _context.Users.Remove(telegramUser);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool RemoveUser(long telegramUserId)
        {
            try
            {
                var user = GetUser(telegramUserId);
                _context.Users.Remove(user);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
