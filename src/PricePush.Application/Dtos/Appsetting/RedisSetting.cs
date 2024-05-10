using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricePush.Dtos.Appsetting
{
    public class RedisSetting
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public int Database { get; set; }
        public string Password { get; set; }
    }
}
