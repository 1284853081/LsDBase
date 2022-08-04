# LsReader类
## 定义
命名空间:LsDBase.Core    
数据库Reader对象，用于查看获取到的数据    
该类不能直接创建对象，需要由DataBase类的[Select](/Docs/Functions/DataBase_Select.md)方法获得   
继承:Object -> LsReader   
```C#
public class LsReader
```
## 方法
|方法定义|描述|
|:----|:----|
|[GetBoolean(int ind)](/Docs/Functions/LsReader_GetBoolean.md)|将数据转成bool|
|[GetByte(int ind)](/Docs/Functions/LsReader_GetByte.md)|将数据转成byte|
|[GetChar(int ind)](/Docs/Functions/LsReader_GetChar.md)|将数据转成char|
|[GetDateTime(int ind)](/Docs/Functions/LsReader_GetDateTime.md)|将数据转成DateTime|
|[GetDecimal(int ind)](/Docs/Functions/LsReader_GetDecimal.md)|将数据转成decimal|
|[GetDouble(int ind)](/Docs/Functions/LsReader_GetDouble.md)|将数据转成double|
|[GetFloat(int ind)](/Docs/Functions/LsReader_GetFloat.md)|将数据转成float|
|[GetInt16(int ind)](/Docs/Functions/LsReader_GetInt16.md)|将数据转成short|
|[GetInt32(int ind)](/Docs/Functions/LsReader_GetInt32.md)|将数据转成int|
|[GetInt64(int ind)](/Docs/Functions/LsReader_GetInt64.md)|将数据转成long|
|[GetSByte(int ind)](/Docs/Functions/LsReader_GetSByte.md)|将数据转成sbyte|
|[GetString(int ind)](/Docs/Functions/LsReader_GetString.md)|将数据转成string|
|[GetUInt16(int ind)](/Docs/Functions/LsReader_GetUInt16.md)|将数据转成ushort|
|[GetUInt32(int ind)](/Docs/Functions/LsReader_GetUInt32.md)|将数据转成uint|
|[GetUInt64(int ind)](/Docs/Functions/LsReader_GetUInt64.md)|将数据转成ulong|
|[Read(int ind)](/Docs/Functions/LsReader_Read.md)|判断下一行是否有数据|