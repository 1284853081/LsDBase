# LSDB.Login方法
## 定义
命名空间:LsDBase    
登陆数据库对象   
```C#
public static DataBase Login(string dbname,string password)
```
## 参数
`dbname`  string    
数据库名称   
`password` string   
任意数据库密码，密码长度不超过16个字符，ASCII中256个字符均支持,密码错误也可连接至数据库，但无法正确的读取数据
## 返回
`DataBase`    
数据库对象
