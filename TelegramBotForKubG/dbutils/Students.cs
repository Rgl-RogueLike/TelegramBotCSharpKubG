using System.ComponentModel.DataAnnotations;

namespace TelegramBotForKubG.dbutils
{
    public partial class Students
    {
        [Key] 
        public int Id { get; set; }
        public string? Login { get; set; }
        public int Code_Id { get; set; }
    }
}
 