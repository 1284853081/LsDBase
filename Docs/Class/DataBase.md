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
|[CreateTable(string table, string[] fields, params DataSize[] sizes)](/Docs/Functions/DataBase_CreateTable.md)|创建数据表|
|[Delete(string table,KeyValues condition)](/Docs/Functions/DataBase_Delete.md)|删除数据|
|[DeleteTable(string table)](/Docs/Functions/DataBase_DeleteTable.md)|删除数据表|
|[GetFields(string table)](/Docs/Functions/DataBase_GetFields.md)|获得指定表所有字段|
|[GetTables()](/Docs/Functions/DataBase_GetTables.md)|获得所有表|
|[Insert(string table, KeyValues kvps)](/Docs/Functions/DataBase_Insert.md)|插入数据|
|[Select(string table, Fields fields, KeyValues condition)](/Docs/Functions/DataBase_Select.md)|查询数据|
|[Update(string table, KeyValues kvps, KeyValues condition)](/Docs/Functions/DataBase_Update.md)|更新数据|
