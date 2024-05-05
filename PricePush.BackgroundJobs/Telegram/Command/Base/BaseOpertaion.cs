using PricePush.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace PricePush.BackgroundJobs.Telegram.Command.Base
{
    public class BaseOpertaion : IOpertaionr
    {
        TelegramBotClient? m_BotClient;
        public string Name { get; set; } = "";

        public string Command { get; set; } = "";

        public string AuthToken { get; set; } = "";
        public string Describe { get; set; } = "";

        public TelegramBotClient BotClient
        {
            get
            {
                if (m_BotClient == null)
                {
                    m_BotClient = new TelegramBotClient(AuthToken);
                }
                return m_BotClient;
            }
        }

        public async Task HandleCommand()
        {
           await Task.Run(() => Execute());
        }

        protected virtual void Execute() { }
    }
}
