using System.Drawing;
using System.Runtime.CompilerServices;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBotForKubG.dbutils;
using TelegramBotForKubG.res;


namespace TelegramBotForKubG
{
    internal class Program
    {

        static string botToken = "6208909723:AAHNvSYCQgSovB3iSbpJmjHKQmv-upNwXso";
        static ITelegramBotClient botClient = new TelegramBotClient(botToken);

        public static async Task CommandAddFaculty(ITelegramBotClient botClient, Update update, Message message)
        {
            List<string> specialties = new List<string>()
                    {
                        "Агрономии и экологии",
                        "Агрохимии и защиты растений",
                        "Архитектурно-строительный",
                        "Ветеринарной медицины",
                        "Гидромелиорации",
                        "Землеустроительный",
                        "Зоотехнии",
                        "Механизации",
                        "Пищевых производств и биотехнологий",
                        "Плодоовощеводства и виноградарства",
                        "Прикладной информатики",
                        "Управления",
                        "Учетно-финансовый",
                        "Финансы и кредит",
                        "Экономический",
                        "Энергетики",
                        "Юридический"
                    };
            foreach (var item in specialties)
            {
                await DataBaseMethods.AddFaculties(item);
            }
            await botClient.SendTextMessageAsync(
                message.Chat.Id,
                "Факультеты успешно добавлены"
            );
        }

        public static async Task CommandAddCodes(ITelegramBotClient botClient, Update update, Message message)
        {
            List<string> specialties = new List<string>()
                    {
                        "Агрономии и экологии",
                        "Агрохимии и защиты растений",
                        "Архитектурно-строительный",
                        "Ветеринарной медицины",
                        "Гидромелиорации",
                        "Землеустроительный",
                        "Зоотехнии",
                        "Механизации",
                        "Пищевых производств и биотехнологий",
                        "Плодоовощеводства и виноградарства",
                        "Прикладной информатики",
                        "Управления",
                        "Учетно-финансовый",
                        "Финансы и кредит",
                        "Экономический",
                        "Энергетики",
                        "Юридический"
                    };
            foreach (var item in specialties)
            {
                await Excel.GetCodeFromExcel(item);
            }
            await botClient.SendTextMessageAsync(
                    message.Chat.Id,
                    "Коды успешно записаны!"
                    );
        }

        public static async Task CommandAdmin(ITelegramBotClient botClient, Update update, Message message)
        {
            await botClient.SendTextMessageAsync(
                message.Chat.Id,
                "__AdminPanel__\n" +
                "/addFaculty - добавить факультеты\n" +
                "/addCode - добавить коды\n"
                );
        }

        public static async Task CommandStart(ITelegramBotClient botClient, Update update, Message message)
        {
            await botClient.SendTextMessageAsync(
                    message.Chat.Id,
                    "*Приветствие\n" +
                    "Введите Ваш логин:");
            await DataBaseMethods.AddStudent(message.Chat.Id);
        }

        public static async Task DialogStudent(ITelegramBotClient botClient, Update update, Message message)
        {
            int dialogStatus = await DataBaseMethods.GetStatus(message.Chat.Id);
            string token = await DataBaseMethods.GetCode(message.Chat.Id);
            bool trueLogin = true;
            bool truePassword = true;
            string nameFaculty = "Прикладной информатики";
            if (message.Text is not null)
            {
                if (dialogStatus == 0)
                {
                    if (trueLogin)
                    {
                        await DataBaseMethods.AddStudentLogin(message.Chat.Id, message.Text.ToString());
                        await botClient.SendTextMessageAsync(
                                message.Chat.Id,
                                "Логин введен корректно"
                              );
                    }
                }
                if (dialogStatus == 1)
                {
                    if (truePassword)
                    {
                        await DataBaseMethods.AddStudentCode(message.Chat.Id, nameFaculty);
                        await botClient.SendTextMessageAsync(
                                message.Chat.Id,
                                "Пароль введен корректно"
                              );
                        await botClient.SendTextMessageAsync(
                                message.Chat.Id,
                                @"Ваш токен для голосования:\n" + 
                                token + "\n" + 
                                "Ссылка для голосования:\n" + 
                                "https://www.kubsau.ru/"
                            ); 
                    }
                }
                if (dialogStatus == 2)
                {
                    await botClient.SendTextMessageAsync(
                            message.Chat.Id,
                            @"Ваш токен для голосования:\n" +
                            token + "\n" +
                            "Ссылка для голосования:\n" +
                            "https://www.kubsau.ru/"
                          ); 
                }
            }
        }

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            if (update.Type == UpdateType.Message)
            {
                var message = update.Message;
                if (message.Text is not null)
                {
                    if (message.Text.ToString() == "/addFaculty")
                    {
                        await CommandAddFaculty(botClient, update, message);
                    }
                    if (message.Text.ToString() == "/addCode")
                    {
                        await CommandAddCodes(botClient, update, message);
                    }
                    if (message.Text.ToString() == "/admin")
                    {
                        await CommandAdmin(botClient, update, message);
                    }
                    if (message.Text.ToString() == "/start")
                    {
                        await CommandStart(botClient, update, message);
                    }
                    if (!message.Text.StartsWith("/"))
                    {
                        await DialogStudent(botClient, update, message);
                    }
                }
            }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Запущен бот: " + botClient.GetMeAsync().Result.FirstName);
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { },
            };

            botClient.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
            Console.ReadLine();
        }
    }
}