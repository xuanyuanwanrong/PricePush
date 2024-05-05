using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using System.Security.Cryptography;

namespace PricePush.BackgroundJobs.Telegram.Command.Base
{
    public interface IOpertaionr
    {
        string Name { get; }
        string Command { get; }
        string Describe { get; }
        TelegramBotClient BotClient { get; }

        Task HandleCommand();
    }
}
