using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Exceptions;
using System.Linq;

namespace TelegaBot
{
    public static class TextCommands
    {
        static bool active = false;
        public static void GetMessangeCommand(ITelegramBotClient telegram, Message message, string command)
        {
            if (command[0] == '/')
            {
                if (command == "/menuBot")
                {
                    telegram.SendTextMessageAsync(message.Chat, "/StartAction\n/StopAction\n/FuckYou\n/OhShitImSorry");
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
                else if (command == "/FuckYou")
                {
                    if (active == true)
                    {
                        telegram.SendTextMessageAsync(message.Chat, "No, Fuck You Leather Man", replyMarkup: InlineCommands.GetKeyboard("fuck"));
                    }
                    else
                    {
                        telegram.SendTextMessageAsync(message.Chat, "Bot is sleep");
                    }
                }
                else if (command == "/null")
                {
                    if (active == true)
                    {
                        telegram.SendTextMessageAsync(message.Chat, "Choose goods", replyMarkup: InlineCommands.GetDBInline());
                    }
                    else
                    {
                        telegram.SendTextMessageAsync(message.Chat, "Bot is sleep");
                    }
                }
                else if (command == "/Kal")
                {
                    if (active == true)
                    {
                        telegram.SendTextMessageAsync(message.Chat, "Kal?", replyMarkup: InlineCommands.GetKeyboard("kal"));
                    }
                    else
                    {
                        telegram.SendTextMessageAsync(message.Chat, "Bot is sleep");
                    }
                }
                else if (command == "/OhShitImSorry")
                {
                    if (active == true)
                    {
                        telegram.SendTextMessageAsync(message.Chat, "Sorry for what?\nOur dad learn us not shame our dicks, exacly when he a such a good size.");
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
            else
            {
               
            }
        }
        public static void GetReplyMessage(ITelegramBotClient telegram, Message message, Update update, string content, string replyTo)
        {
            if (replyTo == "How many you want? For answer - reply this message \n (^._.^)")
            {
                try
                {
                    int amount = Convert.ToInt32(content);
                    decimal price = ConnectClass.entities.TovarListTelega.Where(i => i.NameTovar == InlineCommands.LastCallBack).Select(i => i.PriceTovar).FirstOrDefault();
                    telegram.SendTextMessageAsync(message.Chat, $"{content} {InlineCommands.LastCallBack}\nPrice = {price * amount}");
                }
                catch (Exception)
                {
                    telegram.SendTextMessageAsync(message.Chat, "ТЫ ДОЛБОЕБ ТРЕХЪЯДЕРНЫЙ, ИЛИ ГДЕ?!\n ЦИФРУ, СУКА, ВВЕДИ МНЕ КОРРЕКТНУЮ, ПО ХОРОШЕМУ");
                }
            }
        }
    }
}
