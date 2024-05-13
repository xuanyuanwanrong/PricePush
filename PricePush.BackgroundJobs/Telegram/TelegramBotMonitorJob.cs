using IdentityServer4.Models;
using PricePush.Database;
using PricePush.Entities;
using PricePush.Factory.TelegramBot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Volo.Abp.BackgroundJobs;

namespace PricePush.BackgroundJobs.Telegram
{
    public class TelegramBotMonitorJob : IBackgroundJob<TelegramBotMonitorJobArgs>
    {
        public void Execute(TelegramBotMonitorJobArgs args)
        {
            var token = LoadAuthToken(args.BotId);
            StartReceiving(token).GetAwaiter().GetResult();
        }
        async Task StartReceiving(string token)
        {
            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };
            using CancellationTokenSource cts = new();

            var botClient = new TelegramBotClient(token);

            var me = await botClient.GetMeAsync();

            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );

        }

        string LoadAuthToken(long botId)
        {
            var bot = Sqlsugar.Database.Queryable<TgBot>().First(t => t.BotID == botId && t.IsEnable);
            if (bot == null)
            {
                throw new Exception("机器人不存在");
            }
            return bot.AuthToken;
        }
        Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var msg = GetMessage(update);
            if (msg is null)
            {
                return;
            }
            var command = GetCommand(msg);

            var factory = TelegramBotOperationFactory.FindOperation(command);

            await factory.Execute();
        }

        string GetCommand(Message msg)
        {

            if (msg.Text is not { } messageText)
            { return ""; }
            if (msg.Entities == null) { return ""; }
            var action = msg.Entities.FirstOrDefault(p => p.Type == MessageEntityType.BotCommand);
            if (action == null) { return ""; }
            return msg.Text.Substring(action.Offset, action.Length);
        }
        Message? GetMessage(Update update)
        {
            Message? message = null;
            switch (update.Type)
            {
                case UpdateType.Message:
                    message = update.Message;
                    break;
                case UpdateType.InlineQuery:
                    break;
                case UpdateType.ChosenInlineResult:
                    break;
                case UpdateType.CallbackQuery:
                    break;
                case UpdateType.EditedMessage:
                    break;
                case UpdateType.ChannelPost:
                    break;
                case UpdateType.EditedChannelPost:
                    break;
                case UpdateType.ShippingQuery:
                    break;
                case UpdateType.PreCheckoutQuery:
                    break;
                case UpdateType.Poll:
                    break;
                case UpdateType.PollAnswer:
                    break;
                case UpdateType.MyChatMember:
                    break;
                case UpdateType.ChatMember:
                    break;
                case UpdateType.ChatJoinRequest:
                    break;
                case UpdateType.Unknown:
                default:
                    break;
            }
            return message;
        }
    }

    public class TelegramBotMonitorJobArgs
    {
        public long BotId { get; set; }
    }
}
