# KeyValues类
## 定义
命名空间:LsDBase.Subsidiary    
定义条件集合   
继承:Object -> KeyValues   
```C#
public class KeyValues
```
## 属性
|属性定义|描述|
|:----|:----|
|Count|获得当前对象所具有的的字段数量|
|IsNull|表示当前对象是否是Null对象|
|Null|静态属性，获得Null对象，指示没有任何条件|

## 构造函数
|函数定义|描述|
|:----|:----|
|Fields(string kvp,param string/[] data)|用任意数量的条件创建对象|

**条件字符串需满足格式才能识别，格式("field=value")**
## 方法
|方法定义|描述|
|:----|:----|
|ContainsKey(string key)|是否包含指定字段|
