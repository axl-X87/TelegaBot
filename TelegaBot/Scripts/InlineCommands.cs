using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Exceptions;
using System.Linq;
using TelegaBot.DataBase;
using System.IO;

namespace TelegaBot
{
    public static class InlineCommands
    {
        public static Nullable<int> idAmountMessange = null;
        public static Nullable<int> idGood = null;
        public static string pathPhoto = "C:\\Users\\Привет!\\Desktop\\ЕБАТЬ ВАЖНО НАХУЙ.jpg";
        public static InlineKeyboardButton[] buttonMass =
        {
            InlineKeyboardButton.WithCallbackData("Tovar List", "list"),
            InlineKeyboardButton.WithCallbackData("Image", "img"),
            InlineKeyboardButton.WithCallbackData("Back", "back"),
        };
        public static ReplyKeyboardMarkup keyboardKalKAL = new ReplyKeyboardMarkup(new[] { new[] { new KeyboardButton("Бебебе с Бабаба"), new KeyboardButton("Ломай меня полностью") }, new[] { new KeyboardButton("Ты можешь сломать меня"), new KeyboardButton("Я хочу чтоб ты ломал меня") } });
        public static ReplyKeyboardMarkup commandListKeyboard = new ReplyKeyboardMarkup(new[] { new[] { new KeyboardButton("/menuBot"), new KeyboardButton("/StartAction") }, new[] { new KeyboardButton("/null"), new KeyboardButton("/Kal") }, new[] { new KeyboardButton("/key") } });
        private static string[] _dataBaseMass = ConnectClass.entities.ListTG.Select(i => i.Name).ToArray();
        private static int[] _dataBaseMassid = ConnectClass.entities.ListTG.Select(i => i.id).ToArray();
        public static byte[][] _dataBaseMassPH = ConnectClass.entities.ListTG.Select(i => i.Photo).ToArray();
        private static Nullable<decimal>[] _dataBaseMassP = ConnectClass.entities.ListTG.Select(i => i.Price).ToArray();
        public static InlineKeyboardMarkup GetDBInline(int variant)
        {
            InlineKeyboardButton[][] keyboardB = new InlineKeyboardButton[_dataBaseMass.Length + variant][];
            for (int id = 0; id < _dataBaseMass.Length; id++)
            {
                keyboardB[id] = new[] { InlineKeyboardButton.WithCallbackData(ConnectClass.entities.ListTG.Where(i => i.id == id + 1).Select(i => i.id).FirstOrDefault().ToString(), ConnectClass.entities.ListTG.Where(i => i.id == id + 1).Select(i => i.id).FirstOrDefault().ToString()) };
            }
            if (variant == 1)
            {
                keyboardB[_dataBaseMass.Length] = new[] { buttonMass[2] };
            }            
            InlineKeyboardMarkup markup = new InlineKeyboardMarkup(keyboardB);
            return markup;
        }
        public static InlineKeyboardMarkup GetKeyboard(string catchCommand)
        {
            if (catchCommand == "null")
            {
                return GetDBInline(1);
            }
            else if (catchCommand == "int")
            {
                return new[] { new[] { buttonMass[0] }, new[] { buttonMass[1] } };
            }
            else
            {
                return null;
            }
        }
        public static void CallBack(ITelegramBotClient telegram, Update update, Message message)
        {
            if (update.CallbackQuery.Data == "list")
            {
                telegram.EditMessageTextAsync(message.Chat, message.MessageId, "Choose goods", replyMarkup: GetDBInline(1));
            }
            else if (update.CallbackQuery.Data == "back")
            {
                telegram.EditMessageTextAsync(message.Chat, message.MessageId, "Choose goods", replyMarkup: GetKeyboard("int"));
            }
            else if (update.CallbackQuery.Data == "img")
            {
                telegram.DeleteMessageAsync(message.Chat, message.MessageId);
                FileStream fileSP = new FileStream(InlineCommands.pathPhoto, FileMode.Open, FileAccess.Read);
                telegram.SendPhotoAsync(chatId: message.Chat, photo: new Telegram.Bot.Types.InputFiles.InputOnlineFile(fileSP), replyMarkup: InlineCommands.GetDBInline(0));
            }
            else if (_dataBaseMassid.Any(i => i.ToString() == update.CallbackQuery.Data))
            {
                idGood = Convert.ToInt32(update.CallbackQuery.Data);
                telegram.SendTextMessageAsync(message.Chat, $"You choosed" +
                    $"\n{ConnectClass.entities.ListTG.Where(i => i.id == idGood).Select(i => i.Name).FirstOrDefault() }" +
                    $"\nPrice : {ConnectClass.entities.ListTG.Where(i => i.id == idGood).Select(i => i.Price).FirstOrDefault().ToString()} ₽");
                idAmountMessange = message.MessageId;
            }
        }
    }
}
