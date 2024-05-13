using PricePush.Database;
using PricePush.Entities;
using PricePush.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace PricePush.Services
{
    public class TgBotService : ApplicationService, ITgBotService
    {
        public async Task<TgBot> GetTgBot(long botId)
        {
            var bot = await Sqlsugar.Database.Queryable<TgBot>().FirstAsync(t => t.BotID == botId);
            return bot;
        }

        public async Task<List<TgBot>> GetTgBotList()
        {
            var bots = await Sqlsugar.Database.Queryable<TgBot>().ToListAsync();
            return bots;
        }
    }
}
