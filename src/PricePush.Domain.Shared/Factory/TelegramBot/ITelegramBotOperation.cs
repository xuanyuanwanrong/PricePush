using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace PricePush.Factory.TelegramBot
{
    public interface ITelegramBotOperation
    {
        Update Update { get; }

        long BotId { get; }
        Task Execute();
    }
}
