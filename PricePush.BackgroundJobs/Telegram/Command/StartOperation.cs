using Microsoft.VisualBasic;
using PricePush.CustomAttribute;
using PricePush.Factory.TelegramBot;
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
    [CommandAttribute("/start")]
    public class StartOperation : ITelegramBotOperation
    {
        public Update Update => throw new NotImplementedException();

        public long BotId => throw new NotImplementedException();

        public async Task Execute()
        {
            //await botClient.SendTextMessageAsync(update.Message.Chat.Id, "执行了/start操作");
        }
    }
}
