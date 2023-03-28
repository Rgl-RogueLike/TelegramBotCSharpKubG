using System.ComponentModel.DataAnnotations;

namespace TelegramBotForKubG.dbutils
{
    internal class Faculties
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
