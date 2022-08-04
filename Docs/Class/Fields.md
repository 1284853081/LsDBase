# Fields结构
## 定义
命名空间:LsDBase.Subsidiary    
定义字段集合   
继承:Object -> Fields   
```C#
public struct Fields
```
## 属性
|属性定义|描述|
|:----|:----|
|All|静态属性，返回Fields，指示获得所有字段下的数据|
|Count|获得当前对象所具有的的字段数量|
|IsAll|表示当前对象是否是All对象|
|this\[int]|自定义索引，获得字段名称|

##构造函数
|函数定义|描述|
|:----|:----|
|Fields(param string/[] args)|用任意数量的string生成对象|