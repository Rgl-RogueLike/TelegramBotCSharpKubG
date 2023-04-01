using System.ComponentModel.DataAnnotations;

namespace TelegramBotForKubG.dbutils
{
    public partial class Students
    {
        [Key] 
        public long Chat_Id { get; set; }
        public string? Login { get; set; }
        public int Code_Id { get; set; }
        public int StageDialog { get; set; }
    }
}
 