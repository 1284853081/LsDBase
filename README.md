# LsDBase
[新手教程](/Docs/Course.md)   
[API文档](/Docs/ClassList.md)
## 下载
[release](https://github.com/1284853081/LsDBase/releases)
## 简介
LsDBase为轻量本地结构化数据库插件，包含一系列数据操作，数据按照库-表-字段的方式存放，登陆数据库后即可完成对数据的增删查改。LsDBase为轻量数据库，仅支持单用户使用。本项目以库的形式制作，将LsDBase的前置安装与使用聚合在一起，在使用时建议先通过一个前置程序完成LsDBase的安装在将dll引用至项目本地进行登陆，完成数据的增删查改。本项目基于.Net 6框架。
## 协议
`LsDBase`采用`Apache v2.0`协议
## LsDBase安装
将LsDBase源代码下载到本地，进行编译生成dll  
通过VS(Visual Studio)建立一个控制台应用程序  
建立好之后选择依赖项，添加对dll的项目引用
在控制台主程序入口输入下面的代码完成数据库的创建  
```C#
using LsDBase;
using LsDBase.Core;

//dbname为你创建的数据库名，password为数据库密码
DataBase dbase = LSDB.CreateDB("dbname", "password");
//tablename为需要建表的名字,string[]数组存放表中的字段名，DataSize类指示字段的类型大小且与字段一一对应   
DataSize.String16表示fieldname1为最多存放16字节的字符串
dbase.CreateTable("tablename", new string[] { "fieldname1", "fieldname2" }, DataSize.String16, DataSize.Short);
```   
运行上面的代码即可完成安装与建库建表操作。 
运行后将会在控制台程序的主目录下生成一个LSDB文件夹，该文件夹存放所有的数据信息   
通过将LSDB文件夹完整的移植到其他项目的主目录下即可完成数据的转移
## LsDBase使用
在需要使用到LsDBase的项目中添加对dll的引用    
```C#
using LsDBase;
using LsDBase.Core;

//dbname为你创建的数据库名，password为数据库密码
DataBase dbase = LSDB.Login("dbname","password");
//查指定表中所有值,返回LsReader对象
LsReader reader = dbase.Select("tablename",Fields.All,KeyValues.Null);
//其余操作查看文档
```   
