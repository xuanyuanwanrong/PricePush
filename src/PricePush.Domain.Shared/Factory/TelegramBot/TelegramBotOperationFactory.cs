using PricePush.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PricePush.Factory.TelegramBot
{
    public class TelegramBotOperationFactory
    {
        public Dictionary<string, Func<ITelegramBotOperation>> _operations = new Dictionary<string, Func<ITelegramBotOperation>>();

        public TelegramBotOperationFactory()
        {
            RegisterOperations();
        }

        private void RegisterOperations()
        {
            var operationTypes = Assembly.GetExecutingAssembly().GetTypes()
                                          .Where(t => typeof(ITelegramBotOperation).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            foreach (var type in operationTypes)
            {
                var commandAttribute = type.GetCustomAttribute<CommandAttribute>();
                if (commandAttribute != null)
                {
                    var command = commandAttribute.Command;
                    _operations[command] = () => (ITelegramBotOperation)Activator.CreateInstance(type);
                }
            }

        }
        public void RegisterOperation(string command, Func<ITelegramBotOperation> operation)
        {
            _operations[command] = operation;
        }
        public ITelegramBotOperation FindOperation(string command)
        {
            if (_operations.ContainsKey(command))
            {
                return _operations[command]();
            }
            else
            {
                throw new ArgumentException("Unknown command.");
            }
        }
    }
}
