using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace PricePush.Entities
{
    public class TgBot : Entity<int>, IHasCreationTime
    {

        [SugarColumn(IsPrimaryKey = true)]
        public override int Id { get; protected set; }

        public long BotID { get; set; }
        /// <summary>
        /// name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// token
        /// </summary>
        public string AuthToken { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }

        public DateTime CreationTime { get; set; } =DateTime.Now;
    }
}
