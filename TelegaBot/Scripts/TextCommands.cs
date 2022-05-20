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
using Telegram.Bot.Types.InputFiles;
using System.Drawing;

namespace TelegaBot
{
    public static class TextCommands
    {
        public static Nullable<int> idPhotoMessange = null;
        static bool active = false;
        public static void GetMessangeCommand(ITelegramBotClient telegram, Message message, string command)
        {
            if (command[0] == '/')
            {
                if (command == "/Menu")
                {
                    telegram.SendTextMessageAsync(message.Chat, "Commands for Bot", null, null, false, false, 0, false, InlineCommands.commandListKeyboard);
                }
                else if (command == "/editeble")
                {
                    telegram.SendTextMessageAsync(message.Chat, "Menu", replyMarkup: InlineCommands.GetKeyboard("int"));
                }
                else if (command == "/key")
                {
                    telegram.SendTextMessageAsync(message.Chat, "Text", null, null, false, false, 0, false, InlineCommands.keyboardKalKAL);
                }
                else if (command == "/StartAction")
                {
                    active = true;
                    telegram.SendTextMessageAsync(message.Chat, "Begin work bot!");
                }
                else if (command == "/StopAction")
                {
                    active = false;
                    telegram.SendTextMessageAsync(message.Chat, "Stop work bot!");
                }
                else if (command == "/img")
                {
                    FileStream fileSP = new FileStream(InlineCommands.pathPhoto, FileMode.Open, FileAccess.Read);
                    telegram.SendPhotoAsync(chatId: message.Chat, new InputOnlineFile(fileSP), replyMarkup: InlineCommands.GetDBInline(0));
                }
                else if (command == "/imgDB")
                {
                    telegram.SendTextMessageAsync(message.Chat, $"You choosed");
                    idPhotoMessange = message.MessageId;
                    //try
                    //{
                    //    var photo = InlineCommands._dataBaseMassPH.First();
                    //    using (FileStream file = new FileStream(Program.pathFolder + "/img.jpg", FileMode.Create, FileAccess.Write))
                    //    {
                    //        file.Write(photo, 0, photo.Length);
                    //        string newPath = Program.pathFolder + "/img.jpg";
                    //        file.Close();
                    //        FileStream stream = new FileStream(newPath, FileMode.Open, FileAccess.ReadWrite);
                    //        telegram.SendPhotoAsync(chatId: message.Chat, photo: new InputOnlineFile(stream));
                    //    }
                    //}
                    //catch (Exception)
                    //{
                    //    telegram.SendTextMessageAsync(message.Chat, "Stop work bot!");
                    //}
                }
                else if (command == "/null")
                {
                    if (active == true)
                    {
                        telegram.SendTextMessageAsync(message.Chat, "Choose goods", replyMarkup: InlineCommands.GetDBInline(1));
                    }
                    else
                    {
                        telegram.SendTextMessageAsync(message.Chat, "Bot is sleep");
                    }
                }
                else
                {
                    telegram.SendTextMessageAsync(message.Chat, "Incorect command");
                }
            }
        }
        public static void GetAmountMessage(ITelegramBotClient telegram, Message message, Update update, string content)
        {
            try
            {
                int amount = Convert.ToInt32(content);
                Nullable<decimal> price = ConnectClass.entities.ListTG.Where(i => i.id == InlineCommands.idGood).Select(i => i.Price).FirstOrDefault();
                telegram.SendTextMessageAsync(message.Chat, $"You choosed {content} of {ConnectClass.entities.ListTG.Where(i => i.id == InlineCommands.idGood).Select(i => i.Name).FirstOrDefault()} \nPrice = {price * amount}");
            }
            catch (Exception)
            {
                telegram.SendTextMessageAsync(message.Chat, "ТЫ ДОЛБОЕБ ТРЕХЪЯДЕРНЫЙ, ИЛИ ГДЕ?!\n ЦИФРУ, СУКА, ВВЕДИ МНЕ КОРРЕКТНУЮ, ПО ХОРОШЕМУ");
            }
        }
        public static void GetPhotoMessage(ITelegramBotClient telegram, Message message, Update update, string content)
        {
            try
            {
                int code = Convert.ToInt32(content);
                var photo = ConnectClass.entities.ListTG.Where(i => i.id == code).Select(i => i.Photo).FirstOrDefault();
                using (FileStream file = new FileStream(Program.pathFolder + "/img.jpg", FileMode.Create, FileAccess.Write))
                {
                    file.Write(photo, 0, photo.Length);
                    string newPath = Program.pathFolder + "/img.jpg";
                    file.Close();
                    FileStream stream = new FileStream(newPath, FileMode.Open, FileAccess.ReadWrite);
                    telegram.SendPhotoAsync(chatId: message.Chat, photo: new InputOnlineFile(stream));
                }
            }
            catch (Exception e)
            {
                telegram.SendTextMessageAsync(message.Chat, e.ToString());
            }
        }
    }
}