using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PricePush.BackgroundJobs.Telegram.Command.Base
{
    public interface IOperationHandler
    {
        Task HandleOperation(ITelegramBotClient botClient, Update update);
    }
}
