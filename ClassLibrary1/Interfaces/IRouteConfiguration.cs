namespace TelegramBot.Service.Interfaces
{
    public interface IRouteConfiguration
    {
        public void CallController(string comment);
        public Task<bool> ProccessForwarder(long userId);
        public Task<bool> ProccessContact(long userId);

    }
}
