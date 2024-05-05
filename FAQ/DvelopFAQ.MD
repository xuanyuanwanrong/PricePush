# #主要描述在开发该项目中遇到问题

###### 1.如何下载指定版本的abp框架，为什么每次下载默认为最新

答：

###### 2.SQL server如何开启sa或其他账号登陆，并使用外网登陆这些账号

答：

###### 3.c# web项目启动时为什么没有控制台打开，   Console.WriteLine();的输出也没地方展示

答：

###### 4.chrome出现错误：您的连接不是私密连接 攻击者可能会试图从 localhost 窃取您的信息（例如：密码、通讯内容或信用卡信息）。了解详情 NET::ERR_CERT_INVALID

```
这是由于访问的目标网站没有证书导致，可以进入chrome的隐藏页面开启`chrome://flags/#allow-insecure-localhost`  改为启动
```

###### 5.abp项目的生成迁移文件、更新到数据库、回滚数据库、删除迁移文件的命令分别是什么

使用cmd、powershell、vs程序控制台，打开 xxx.EntityFrameworkCore模块进行迁移

```
生成迁移文件 				Add-Migration 迁移名称
更新到数据库   				update-database

回滚数据库				update-database -migration 目标版本
删除迁移文件 				Remove-Migration
```

###### 6.为什么 hangfire 提示没有活动的服务器

答：注入userhangfireservice

###### 7.为什么 hangfire 任务很久没有执行

1.检查hangfire 持久化的配置、sqlserver 、mysql、redis是否正确
2.检查corn表达式
3.查看是否有活动服务器
4.查看失败任务


