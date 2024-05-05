using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
namespace PricePush.BackgroundJobs.Telegram.Command.Base
{
    static class OperationFactory
    {
        private static readonly Dictionary<string, Func<IOperationHandler>> operationHandlers = new Dictionary<string, Func<IOperationHandler>>();


        static OperationFactory()
        {
            var handlerTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => typeof(IOperationHandler).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            foreach (var handlerType in handlerTypes)
            {
                // 获取命令名称
                var attribute = handlerType.GetCustomAttribute<CommandNameAttribute>();
                if (attribute != null)
                {
                    var commandName = attribute.CommandName;

                    // 创建处理器实例
                    var handler = (IOperationHandler)Activator.CreateInstance(handlerType);

                    // 添加到字典中
                    operationHandlers.Add(commandName, () => handler);
                }
            }
        }

        public static IOperationHandler GetOperationHandler(string command)
        {
            if (operationHandlers.TryGetValue(command, out Func<IOperationHandler> handlerFactory))
            {
                return handlerFactory.Invoke();
            }
            else
            {
                return null;
            }
        }
    }
}
