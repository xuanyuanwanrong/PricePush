using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PricePush.BackgroundJobs.Telegram.Command.Base
{
    // 自定义属性，用于标记命令名称
    [AttributeUsage(AttributeTargets.Class)]
    public class CommandNameAttribute : Attribute
    {
        public string CommandName { get; }

        public CommandNameAttribute(string commandName)
        {
            CommandName = commandName;
        }
    }
}
