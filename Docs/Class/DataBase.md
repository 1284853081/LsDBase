# DataBase类
## 定义
命名空间:LsDBase.Core    
数据库对象，包含表格建立，数据查询等功能    
该类不能直接创建对象，需要由LSDB类方法获得   
继承:Object -> DataBase   
```C#
public class DataBase
```
## 方法
|方法定义|描述|
|:----|:----|
|[public void CreateTable(string table, string[] fields, params DataSize[] sizes)](/Docs/Functions/LSDB_CreateDB.md)|用指定名称和密码创建数据库|
|[public void Delete(string table,KeyValues condition)](/Docs/Functions/LSDB_DeleteDB.md)|删除指定名称的数据库|
|[public void DeleteTable(string table)](/Docs/Functions/LSDB_GetAllBase.md)|查询所有数据库名称|
|[public List\<string> GetFields(string table)](/Docs/Functions/LSDB_Login.md)|用指定名称和任意密码连接数据库|
