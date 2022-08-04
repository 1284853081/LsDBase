# LsDBase完整使用示例
示例使用VS2022完成，本文档默认读者熟悉VS操作
## 建库建表
打开VS并建立一个C#控制台应用，添加[LsDBase.dll](https://github.com/1284853081/LsDBase/releases)的引用   
将program.cs中的代码删除，添加如下代码
```C#
using LsDBase;
using LsDBase.Core;

DataBase dbase = LSDB.CreateDB("test","123456");
dbase.CreateTable("player",new string[]{"id","name","time"},DataSize.Long,DataSize.String16,DataSize.String32);
```
运行上述代码即可创建一个名为test密码为123456的数据库并创建了一个名为player的数据表    
数据表包含id,name,time三个字段   
**完成后将上述代码删除**
## 登陆数据库
**将[建库建表](#建库建表)中所写示例代码删除，输入如下代码完成数据库登陆**
```C#
using LsDBase;
using LsDBase.Core;

DataBase dbase = LSDB.Login("test","123456");
```
运行上述代码，如没有报错则成功   
**可以尝试使用不是123456的密码登陆数据库**
## 获取test数据库中所有的表名
**接着[登陆数据库](#登陆数据库)示例代码**，输入如下代码
```C#
List<string> tables = dbase.GetTables();
foreach(var name in tables)
  Console.WriteLine(name);
```
运行代码可以看到如下输出    
> player
## 获取player表中的所有字段
**删除[获取test数据库中所有的表名](#获取test数据库中所有的表名)示例代码**，输入如下代码
```C#
List<string> fields = dbase.GetFields();
for(int i = 0;i < fields.Count;i++)
  Console.WriteLine($"字段{i}的名字为{fields[i]}");
```
运行代码可以看到如下输出    
> 字段1的名字为id
> 字段2的名字为name
> 字段3的名字为time
