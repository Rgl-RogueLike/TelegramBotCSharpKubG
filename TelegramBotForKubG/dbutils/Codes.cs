using System.ComponentModel.DataAnnotations;

namespace TelegramBotForKubG.dbutils
{
    public partial class Codes
    {
        [Key]
        public int Id { get; set; }
        public string? Code { get; set; }
        public int Faculty_Id { get; set; }
    }
}
