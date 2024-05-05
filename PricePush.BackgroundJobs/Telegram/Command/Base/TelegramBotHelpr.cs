using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace PricePush.BackgroundJobs.Telegram.Command.Base
{
    public static class TelegramBotHelpr
    {
        public static Message? GetUpdateMessage(Update update)
        {
            Message? msg = null;
            switch (update.Type)
            {
                case global::Telegram.Bot.Types.Enums.UpdateType.Message:
                    msg = update.Message;
                    break;
                case global::Telegram.Bot.Types.Enums.UpdateType.InlineQuery:
                    break;
                case global::Telegram.Bot.Types.Enums.UpdateType.ChosenInlineResult:
                    break;
                case global::Telegram.Bot.Types.Enums.UpdateType.CallbackQuery:
                    msg = update.CallbackQuery!.Message;
                    break;
                case global::Telegram.Bot.Types.Enums.UpdateType.EditedMessage:
                    break;
                case global::Telegram.Bot.Types.Enums.UpdateType.ChannelPost:
                    msg = update.ChannelPost;
                    break;
                case global::Telegram.Bot.Types.Enums.UpdateType.EditedChannelPost:
                    break;
                case global::Telegram.Bot.Types.Enums.UpdateType.ShippingQuery:
                    break;
                case global::Telegram.Bot.Types.Enums.UpdateType.PreCheckoutQuery:
                    break;
                case global::Telegram.Bot.Types.Enums.UpdateType.Poll:
                    break;
                case global::Telegram.Bot.Types.Enums.UpdateType.PollAnswer:
                    break;
                case global::Telegram.Bot.Types.Enums.UpdateType.MyChatMember:
                    break;
                case global::Telegram.Bot.Types.Enums.UpdateType.ChatMember:
                    break;
                case global::Telegram.Bot.Types.Enums.UpdateType.ChatJoinRequest:
                    break;
            case global::Telegram.Bot.Types.Enums.UpdateType.Unknown:
            default:
                    break;
            }
            return msg;
        }
    }
}
