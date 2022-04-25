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
                        telegram.SendTextMessageAsync(message.Chat, "KAL", replyMarkup: InlineCommands.GetDBInline());
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
                telegram.SendTextMessageAsync(message.Chat, "good to say!");
            }
        }
    }
}
