using IdentityServer4.Models;
using PricePush.BackgroundJobs.Telegram.Command.Base;
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
            StartReceiving().GetAwaiter().GetResult();
        }
        async Task StartReceiving()
        {
            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };
            using CancellationTokenSource cts = new();
            var botClient = new TelegramBotClient("7144607718:AAHz-2YrjZxTnlQjMnFe8q9YGslREFquxzo");

            var me = await botClient.GetMeAsync();

            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );

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

            IOperationHandler handler = OperationFactory.GetOperationHandler(update.Message.Text);
            await handler.HandleOperation(botClient, update);

        }

        //string GetCommand(Message msg)
        //{
        //    if (string.IsNullOrWhiteSpace(msg.Text))
        //    {
        //        return string.Empty;
        //    }

        //}
    }

    public class TelegramBotMonitorJobArgs
    {
        public long BotId { get; set; }
    }
}
