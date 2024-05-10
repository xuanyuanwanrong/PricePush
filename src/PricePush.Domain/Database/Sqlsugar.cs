using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PricePush.Database
{
    public class Sqlsugar
    {
        public static SqlSugarClient Database;
        public Sqlsugar(string connectionString)
        {
            Database = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = connectionString,
                DbType = global::SqlSugar.DbType.SqlServer,
                InitKeyType = InitKeyType.Attribute,//从特性读取主键和自增列信息
                IsAutoCloseConnection = true,//开启自动释放模式
            });
        }
    }
}
