using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Exceptions;
using TelegaBot.DataBase;

namespace TelegaBot
{
    /// <summary>
    /// Логика для бота еблота АСУСУЕ
    /// </summary>

    public class Program
    {
        public static string pathFolder { get; set; }
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
                    if (message.MessageId >= InlineCommands.idAmountMessange && InlineCommands.idAmountMessange != null)
                    {
                        TextCommands.GetAmountMessage(telegram, message, update, message.Text);
                    }
                    else if (message.MessageId >= TextCommands.idPhotoMessange && TextCommands.idPhotoMessange != null)
                    {
                        TextCommands.GetPhotoMessage(telegram, message, update, message.Text);
                    }
                    else
                    {
                        Console.WriteLine("Bot cath message : " + message.Text);
                    }
                }
                TextCommands.GetMessangeCommand(telegram, message, message.Text);
            }
            else if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
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
        [STAThread]
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
            string cmd = Console.ReadLine();
            bool next = true;
            if (cmd == "inPH")
            {
                ListTG tovar = new ListTG();
                tovar.Name = "KAL";
                tovar.Price = 666.66m;
                System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
                dialog.ShowDialog();
                foreach (var photo in dialog.FileNames)
                {
                    tovar.Photo = System.IO.File.ReadAllBytes(photo);
                    ConnectClass.entities.ListTG.Add(tovar);
                    ConnectClass.entities.SaveChanges();
                    cmd = "dd";
                    next = true;
                }
            }
            else if (cmd == "fold")
            {
                System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Console.WriteLine(dialog.SelectedPath);
                    pathFolder = dialog.SelectedPath;
                    cmd = "dd";
                }
                next = true;
            }
            Console.ReadLine();
        }
    }
}