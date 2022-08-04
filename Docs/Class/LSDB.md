# LSDB类
## 定义
命名空间:LsDBase    
包含用于处理数据库创建删除登陆等方法    
继承:Object -> LSDB   
```C#
public static class LSDB
```
## 方法
|方法定义|描述|
|:----:|:----:|
|[CreateDB(string dbname,string password)](/Docs/Functions/LSDB_CreateDB.md)|用指定名称和密码创建数据库|
|[DeleteDB(string dbname)](/Docs/Functions/LSDB_DeleteDB.md)|删除指定名称的数据库|
|[GetAllBase()](/Docs/Functions/LSDB_GetAllBase.md)|查询所有数据库名称|
|[Login(string dbname,string password)](/Docs/Functions/LSDB_Login.md)|用指定名称和任意密码连接数据库|
