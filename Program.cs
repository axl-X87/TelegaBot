using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Exceptions;


namespace TelegaBot
{
    /// <summary>
    /// Логика для бота еблота АСУСУЕ
    /// </summary>

    public class Program
    {
        public static ITelegramBotClient bot = new TelegramBotClient(Token.tokenBot); //присваивание токена
        public static async Task HandleUpdateAsync(ITelegramBotClient telegram, Update update, CancellationToken token)
        {        
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message) // условие при -обновление- = -сообщение-
            {
                Message message = update.Message; //непосредственно обновление
                if (message.Text[0] == '/')
                {
                    Console.WriteLine("Bot cath command : " + message.Text); 
                    TextCommands.GetMessangeCommand(telegram, message, message.Text); //вызов обработчика команды
                }
                else
                {
                    if (message.ReplyToMessage != null) //если сообщение является ОТВЕТОМ
                    {
                        Console.WriteLine("Bot cath command : " + message.Text);
                        TextCommands.GetReplyMessage(telegram, message, update, message.Text, message.ReplyToMessage.Text); 
                    }
                    else
                    {
                        Console.WriteLine("Bot cath message : " + message.Text); // вывод принятого сообщения
                        TextCommands.GetMessangeCommand(telegram, message, message.Text); //вызов обработчика команды
                    }                    
                }                                
            }
            else if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery) // если -тип о-
            {                
                Message message = update.CallbackQuery.Message;
                Console.WriteLine("Bot cath inline command : " + message.Text + " " + update.CallbackQuery.Data);
                InlineCommands.CallBack(telegram, update, message);
            }
        }
        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine("Ошибка Бота");
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Bot started " + bot.GetMeAsync().Result.FirstName);
            var cts = new CancellationTokenSource();
            var ctsToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { },
            };
            bot.StartReceiving(HandleUpdateAsync, HandleErrorAsync, receiverOptions, ctsToken);
            Console.ReadLine();
        }
    }
}