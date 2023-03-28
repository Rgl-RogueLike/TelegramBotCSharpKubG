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
/*
        static string botToken = "6208909723:AAHNvSYCQgSovB3iSbpJmjHKQmv-upNwXso";
        static ITelegramBotClient botClient = new TelegramBotClient(botToken);

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            if (update.Type == UpdateType.Message)
            {
                var message = update.Message;
                string stageReg = "";

                if(message.Text.ToLower() == "/start")
                {
                    stageReg = "Введите Ваш логин:";
                    await botClient.SendTextMessageAsync(
                        message.Chat.Id,
                        "*Приветсвие"
                        );
                    await botClient.SendTextMessageAsync(
                        message.Chat.Id,
                        stageReg
                        );
                    return;
                }

                var loginStudent = message.Text.ToString();

*//*                if(await DataBaseMethods.GetLogin(loginStudent) is null)
                {
                    await botClient.SendTextMessageAsync(
                        message.Chat.Id,
                        "Студент с таким именем не найден.\n" +
                        "Попробуйте ещё раз:"
                        );
                    return;
                }
                else
                {
                    stageReg = "Логин введен корректно!";
                    await botClient.SendTextMessageAsync(
                        message.Chat.Id,
                        stageReg
                        );
                    
                }*//*
                if (stageReg == "Логин введен корректно!")
                {
                    stageReg = "InputPass";
                    await botClient.SendTextMessageAsync(
                        message.Chat.Id,
                        "Введите Ваш пароль:"
                        );
                }
                if(stageReg == "InputPass")
                {
                    var password = message.Text.ToString();
*//*                    if(DataBaseMethods.GetPassword(password) is null)
                    {
                        await botClient.SendTextMessageAsync(
                                message.Chat.Id,
                                "Введен неправильный пароль"
                                );
                    }
                    else
                    {
                        stageReg = "Пароль введен корректно";
                        await botClient.SendTextMessageAsync(
                            message.Chat.Id,
                            stageReg);
                    }*//*
                }
            }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }
*/
        static async Task Main(string[] args)
        {
            /*            Console.WriteLine("Запущен бот: " + botClient.GetMeAsync().Result.FirstName);
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
                        );*/
            string facultyName = "Зоотехния";
            await Excel.GetCodeFromExcel(facultyName);
            Console.ReadLine();
        }
    }
}