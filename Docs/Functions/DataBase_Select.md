# DataBase.Select方法
## 定义
命名空间:LsDBase.Core    
查询数据   
```C#
public LsReader Select(string table, Fields fields,KeyValues condition)
```
## 参数
***table***  string    
数据表名称   
***fields*** Fields   
查询的字段   
***kvps*** KeyValues   
条件   
## 返回
***LsReader***
数据库Reader对象