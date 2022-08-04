# LsDBase完整使用示例
示例使用VS2022完成，本文档默认读者熟悉VS操作
## 建库建表
打开VS并建立一个C#控制台应用，添加[LsDBase.dll](https://github.com/1284853081/LsDBase/releases)的引用   
将program.cs中的代码删除，添加如下代码
```C#
using LsDBase;
using LsDBase.Core;
using LsDBase.Subsidiary;

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
```C#
using LsDBase;
using LsDBase.Core;

DataBase dbase = LSDB.Login("test","123456");
List<string> tables = dbase.GetTables();
foreach(var name in tables)
  Console.WriteLine(name);
```
运行代码可以看到如下输出    
> player
## 获取player表中的所有字段
```C#
using LsDBase;
using LsDBase.Core;

DataBase dbase = LSDB.Login("test","123456");
List<string> fields = dbase.GetFields();
for(int i = 0;i < fields.Count;i++)
  Console.WriteLine($"字段{i}的名字为{fields[i]}");
```
运行代码可以看到如下输出    
> 字段0的名字为id   
> 字段1的名字为name   
> 字段2的名字为time
## 插入数据
```C#
using LsDBase;
using LsDBase.Core;
using LsDBase.Subsidiary;

DataBase dbase = LSDB.Login("test","123456");
dbase.Insert("player",new KeyValues("666666","lsdb1","2022/8/4 15:54:00"));
dbase.Insert("player",new KeyValues("777777","lsdb2","2020/12/30 8:30:26"));
dbase.Insert("player",new KeyValues("888888","lsdb3","2021/3/15 12:11:49"));
```
运行上述代码即可完成向player表中插入三条数据
## 查看数据
接下来我们来查看上述插入的三条数据
```C#
using LsDBase;
using LsDBase.Core;
using LsDBase.Subsidiary;

DataBase dbase = LSDB.Login("test","123456");
LsReader ls = dbase.Select("player",Fields.All,KeyValues.Null);
int i = 1;
while(ls.Read())
{
  Console.WriteLine($"第{i}条数据的id为{ls.GetLong(0)}");
  Console.WriteLine($"第{i}条数据的name为{ls.GetString(1)}");
  Console.WriteLine($"第{i}条数据的time为{ls.GetDateTime(2)}");
  i++;
}
```
运行上述代码将看到如下输出
>第1条数据的id为666666    
>第1条数据的name为lsdb1   
>第1条数据的time为2022/8/4 15:54:00   
>第2条数据的id为777777    
>第2条数据的name为lsdb2   
>第2条数据的time为2020/12/30 8:30:26    
>第3条数据的id为888888    
>第3条数据的name为lsdb3   
>第3条数据的time为2021/3/15 12:11:49
## 删除数据
```C#
using LsDBase;
using LsDBase.Core;
using LsDBase.Subsidiary;

DataBase dbase = LSDB.Login("test","123456");
dbase.Delete("player",new KeyValues("name=lsdb2"));
```
运行即可删除name为lsdb2的那条数据
## 更新数据
```C#
using LsDBase;
using LsDBase.Core;
using LsDBase.Subsidiary;

DataBase dbase = LSDB.Login("test","123456");
dbase.Update("player",new KeyValues("id=111111"),new KeyValues("name=lsdb3"));
```
运行即可将name为lsdb3的那条数据中的id更新为111111
## 删除表
```C#
using LsDBase;
using LsDBase.Core;

DataBase dbase = LSDB.Login("test","123456");
dbase.DeleteTable("player");
```
运行即可删除player表   
`尝试用错误的密码登陆数据库再查看数据库信息`
## 数据库跨网络传输
LsDBase默认在当前工作目录的完全限定路径下创建数据库，生成LSDB文件夹，LsDBase的默认操作路径均为工作目录完全限定路径，因此如果需要跨网络传输只需在工作目录下找到LSDB文件夹，将其复制打包传输即可，传输过程无需考虑数据泄露，文件中的数据均已加密，接受者在接受到发送方的LSDB文件夹只需将其放置在工作目录下即可通过上述教程操作数据库.
