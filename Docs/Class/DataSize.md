# DataSize类
## 定义
命名空间:LsDBase.Subsidiary    
数据存放大小    
该类不能直接创建对象，通过属性获得   
继承:Object -> DataSize   
```C#
public class DataSize
```
## 属性
|属性定义|描述|
|:----|:----|
|Short|指示int类型数据，占用10字节|
|UShort|指示int类型数据，占用9字节|
|Int|指示int类型数据，占用15字节|
|UInt|指示int类型数据，占用15字节|
|Long|指示int类型数据，占用24字节|
|ULong|指示int类型数据，占用24字节|
|String16|指示int类型数据，占用20字节|
|String32|指示int类型数据，占用36字节|
|String64|指示int类型数据，占用68字节|
|String128|指示int类型数据，占用132字节|
|String256|指示int类型数据，占用260字节|
|String512|指示int类型数据，占用516字节|
|String1024|指示int类型数据，占用1028字节|
|String2048|指示int类型数据，占用2052字节|
|String4096|指示int类型数据，占用4100字节|
|String8192|指示int类型数据，占用8192字节|
    
**在LsDBase中所有的数据都将以字符串的形式存储，为避免数据占用过大，应合理选择字段大小**
