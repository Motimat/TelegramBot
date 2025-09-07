namespace TelegramBot.Service
{
    [AttributeUsage(AttributeTargets.Method)]
    internal class Route : Attribute
    {
        public string Value { get; set; }
        public Route(string value)
        {
            Value = value;
        }
    }
}
