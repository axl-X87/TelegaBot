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

    public class Program
    {
        public static ITelegramBotClient bot = new TelegramBotClient(Token.tokenBot);
        public static async Task HandleUpdateAsync(ITelegramBotClient telegram, Update update, CancellationToken token)
        {            
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                Message message = update.Message;
                if (message.Text[0] == '/')
                {
                    Console.WriteLine("Bot cath command : " + message.Text);
                }
                else
                {
                    Console.WriteLine("Bot cath message : " + message.Text);
                }
                TextCommands.GetMessangeCommand(telegram, message, message.Text);
            }
            else if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
            {                
                Message message = update.CallbackQuery.Message;
                Console.WriteLine("Bot cath inline command : " + message.Text);
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