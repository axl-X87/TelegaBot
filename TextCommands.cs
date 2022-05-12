using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Exceptions;
using System.Linq;
using System.IO;

namespace TelegaBot
{
    public static class TextCommands
    {
        public static string kal = "";
        public static async void GetMessangeCommand(ITelegramBotClient telegram, Message message, string command)
        {
            if (command[0] == '/')
            {
                if (command == "/Menu")
                {
                    await telegram.SendTextMessageAsync(message.Chat, "Commands for Bot", null, null, false, false, 0, false, InlineCommands.commandListKeyboard);
                }
                else if (command == "/editeble")
                {
                    await telegram.SendTextMessageAsync(message.Chat, "Menu", replyMarkup: InlineCommands.GetKeyboard("int"));
                }
                else if (command == "/key")
                {
                    await telegram.SendTextMessageAsync(message.Chat, "Text", null, null, false, false, 0, false, InlineCommands.keyboardKalKAL);
                }
                else if (command == "/img")
                {
                    FileStream fileSP = new FileStream(InlineCommands.pathPhoto, FileMode.Open, FileAccess.Read);
                    await telegram.SendPhotoAsync(chatId: message.Chat, new Telegram.Bot.Types.InputFiles.InputOnlineFile(fileSP), replyMarkup: InlineCommands.GetDBInline(0));
                }
                else if (command == "/null")
                {
                    await telegram.SendTextMessageAsync(message.Chat, "Choose goods", replyMarkup: InlineCommands.GetDBInline(1));
                }
                else
                {
                    await telegram.SendTextMessageAsync(message.Chat, "Incorect command");
                }
            }
        }
        public static void GetAmountMessage(ITelegramBotClient telegram, Message message, Update update, string content)
        {
            try
            {
                int amount = Convert.ToInt32(content);
                decimal price = ConnectClass.entities.TovarListTelega.Where(i => i.id == InlineCommands.idGood).Select(i => i.PriceTovar).FirstOrDefault();
                telegram.SendTextMessageAsync(message.Chat, $"You choosed {content} of {ConnectClass.entities.TovarListTelega.Where(i => i.id == InlineCommands.idGood).Select(i => i.NameTovar).FirstOrDefault()} \nPrice = {price * amount}");
                kal = "Bot cath inline command : " + $" {ConnectClass.entities.TovarListTelega.Where(i => i.id == InlineCommands.idGood).Select(i => i.NameTovar).FirstOrDefault()} " + amount.ToString() + " from " + message.From.ToString();
            }
            catch (Exception)
            {
                telegram.SendTextMessageAsync(message.Chat, "ТЫ ДОЛБОЕБ ТРЕХЪЯДЕРНЫЙ, ИЛИ ГДЕ?!\n ЦИФРУ, СУКА, ВВЕДИ МНЕ КОРРЕКТНУЮ, ПО ХОРОШЕМУ");
            }
        }
    }
}