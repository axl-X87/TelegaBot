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
        public static ITelegramBotClient bot = new TelegramBotClient(Token.tokenBot);
        public static async Task HandleUpdateAsync(ITelegramBotClient telegram, Update update, CancellationToken token)
        {
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                Message message = update.Message;
                if (message.Text[0] == '/')
                {
                    Console.WriteLine("Bot cath command : " + message.Text + " from " + message.From);
                }
                else
                {
                    if (message.MessageId >= InlineCommands.idAmountMessange && InlineCommands.idAmountMessange != null)
                    {
                        TextCommands.GetAmountMessage(telegram, message, update, message.Text);
                        Console.WriteLine(TextCommands.kal);
                    }
                    else
                    {
                        Console.WriteLine("Bot cath message : " + message.Text + " from " + message.From);
                    }
                }
                TextCommands.GetMessangeCommand(telegram, message, message.Text);
            }
            else if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
            {
                Message message = update.CallbackQuery.Message;
                var userChat = update.CallbackQuery.From;
                Console.WriteLine("Bot cath inline command : " + message.Text + " " + update.CallbackQuery.Data + " from " + userChat);
                InlineCommands.CallBack(telegram, update, message);
            }
            else if (update.Type == Telegram.Bot.Types.Enums.UpdateType.EditedMessage)
            {
                Message message = update.EditedMessage; 
                Console.WriteLine("Bot cath edited message : " + message.Text + " from " + message.From);
                await telegram.SendTextMessageAsync(message.Chat, "It is not right edit you messages");
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