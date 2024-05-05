using PricePush.BackgroundJobs.Telegram.Command.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;

namespace PricePush.BackgroundJobs.Telegram.Command
{
    [CommandName("/start")]
    public class StartOperation : IOperationHandler
    {
        public async Task HandleOperation(ITelegramBotClient botClient, Update update)
        {
            await botClient.SendTextMessageAsync(update.Message.Chat.Id, "执行了/start操作");
        }
    }
}
