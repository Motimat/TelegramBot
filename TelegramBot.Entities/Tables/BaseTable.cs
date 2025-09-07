using System.ComponentModel.DataAnnotations;
using TelegramBot.Entities.Sub;

namespace TelegramBot.Entities.Tables
{
    public class BaseTable<T> : IEntity
    {
        [Key]
        public T Id { get; set; }
    }

    public class BaseTable : BaseTable<long>, IEntity
    {

    }
}
