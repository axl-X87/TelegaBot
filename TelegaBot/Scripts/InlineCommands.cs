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

namespace TelegaBot
{
    public static class InlineCommands
    {
        public static InlineKeyboardMarkup keyboardFuckYou = new InlineKeyboardMarkup(new[] { new[] { InlineKeyboardButton.WithCallbackData("Fuck you", "fuck"), InlineKeyboardButton.WithCallbackData("No \nfuck you", "nFuck") } });
        public static InlineKeyboardMarkup keyboardKal = new InlineKeyboardMarkup(new[] { new[] { InlineKeyboardButton.WithCallbackData("Kal\nKal\nKal\nKal\nKal\nKal\nKal\nKal\nKal\nKal\n", "manyKal"), InlineKeyboardButton.WithCallbackData("Kal\nKal\n", "someKal") } });
        private static string[] _dataBaseMass = ConnectClass.entities.TovarList.Select(i => i.NameTovar).ToArray();
        private static decimal[] _dataBaseMassP = ConnectClass.entities.TovarList.Select(i => i.PriceTovar).ToArray();
        public static InlineKeyboardMarkup GetDBInline()
        {
            InlineKeyboardButton[][] keyboardB = new InlineKeyboardButton[_dataBaseMass.Length][];
            InlineKeyboardButton[] inlines = new InlineKeyboardButton[2];
            for (int i = 0; i < keyboardB.Length; i++)
            {
                keyboardB[i] = new[] { InlineKeyboardButton.WithCallbackData(_dataBaseMass[i] + " - " + _dataBaseMassP[i].ToString() + "₽", _dataBaseMass[i])};
            }
            InlineKeyboardMarkup markup = new InlineKeyboardMarkup(keyboardB);
            return markup;
        }
        public static InlineKeyboardMarkup GetKeyboard(string catchCommand)
        {
            if (catchCommand == "fuck")
            {
                return keyboardFuckYou;
            }
            else if (catchCommand == "kal")
            {
                return keyboardKal;
            }
            else if (catchCommand == "null")
            {
                return GetDBInline();
            }
            else
            {
                return null;
            }
        }
        public static void CallBack(ITelegramBotClient telegram, Update update, Message message)
        {
            if (update.CallbackQuery.Data == "fuck")
            {
                telegram.SendTextMessageAsync(message.Chat, "No, Fuck You Leather Man");
            }
            else if (update.CallbackQuery.Data == "nFuck")
            {
                TextCommands.GetMessangeCommand(telegram, message, "/Kal");
            }
            else if (update.CallbackQuery.Data == "manyKal")
            {
                telegram.SendTextMessageAsync(message.Chat, "KAL\nKAL\nKAL\nKAL\nKAL\nKAL\nKAL\nKAL\nKAL\nKAL\nKAL\nKAL\nKAL\nKAL\nKAL\nKAL\nKAL\nKAL\nKAL\nKAL\nKAL\nKAL\nKAL\nKAL\nKAL\nKAL\nKAL\nKAL\nKAL\nKAL\nKAL\nKAL\nKAL\nKAL\nKAL\nKAL\nKAL\n");
            }
            else if (update.CallbackQuery.Data == "someKal")
            {
                telegram.SendTextMessageAsync(message.Chat, "KAL\nKAL!");
            }
            else if (update.CallbackQuery.Data == "next")
            {
                telegram.SendTextMessageAsync(message.Chat, "KAL", replyMarkup: GetDBInline());
            }
            else if (_dataBaseMass.Any(i => i == update.CallbackQuery.Data))
            {
                telegram.SendTextMessageAsync(message.Chat, $"You choosed\n{update.CallbackQuery.Data}\nPrice : {ConnectClass.entities.TovarList.Where(i => i.NameTovar == update.CallbackQuery.Data).Select(i => i.PriceTovar).FirstOrDefault().ToString()}₽");
            }
        }
    }
}

