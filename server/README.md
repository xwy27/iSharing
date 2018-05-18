# iSharing Server 使用指南

注意：本部分通过了下述样例测试与委员版的前端交互测试！

注意：所有方法均为 POST！但如果您要通过 URL 获取图片，则应使用 GET。在请求时，请务必指明类型为 **application/json** 或 **multipart/form-data**，本文中的C#示例代码理论上已经处理了这个问题（未测试）。

## Get Started

首先用 npm 安装依赖：

```bash
cd server
npm install
```

修改用户名密码为本地环境下的 mysql 服务器用户名与密码：

```js
// @file: model/db.js
// @line: 6-7
user: 'root',
password: '',
```

启动 mysql 服务器并进入 mysql 命令行，创建数据库：
```bash
create database MOSAD_MIDTERM_DB;
```

创建文件夹 public/Images 来保存图片

```bash
mkdir public/Images
```

完成上述步骤后即可启动服务器：

```bash
node server.js
```

## Data Structure

### User

字段名|数据类型|描述
:-:|:-:|:-:
username|string, length <= 30|用户名
password|string, length <= 60|用户密码
email|string, length <= 50|用户邮箱
tel|string, length <= 11|用户电话
wechat|string, length <= 30|微信号
qq|string, length <= 20|qq号
icon|string, length <= 60000+|用户头像的URL

### Item

字段名|数据类型|描述
:-:|:-:|:-:
username|string, length <= 30|物品所属的用户名，不可为空
itemid|int|物品的识别编号，由数据库自行生成，**尽量不要在C#中设置**
itemname|string, length <= 100|物品名称
price|float|租赁价格
description|string, length <= 60000+|物品描述
leasetimes|int|租赁次数
icon|string, length <= 60000+|物品图片的URL

## API Document

**root:** http://localhost:8000

### 用户信息

#### /user_add

描述：添加用户，保存新用户信息（注册用）

请求参数示例：

```js
{
    user: {
        username: '123',
        password: '123',
        email: '123@1.1',
        tel: '12345678901'
    }
}
```

返回：

```js
{
    status: 'error',
    errorMsg: 'Opps...'
}

or

{
    status: 'success'
}
```

#### /user_login

描述：用于检查用户名与密码是否对应

请求参数示例：

```js
{
    user: {
        username: '123',
        password: '123'
    }
}
```

返回：

```js
{
    status: 'error',
    errorMsg: '用户名或密码错误'
}

or

{
    status: 'success' 
}
```

#### /user_get

描述：通过 username 获取用户所有信息

请求参数示例：

```js
{
    user: {
        username: '123'
    }
}
```

返回：

```js
{
    user: {
        username: '123',
        password: '123',
        email: '123@1.1',
        tel: '12345678901',
        qq: '123',
        wechat: '123',
        icon: 'http://localhost:8000/public/Images/1.png'
    }
}
```

#### /user_update

描述：更新 username 对应的用户的信息

请求参数示例：

```js
{
    user: {
        username: '123',
        password: '123',
        email: '123@1.1',
        tel: '12345678901',
        qq: '123',
        wechat: '123',
        icon: 'http://localhost:8000/public/Images/1.png'
    }
}
```

返回：

```js
{
    status: 'error',
    errorMsg: 'Opps...'
}

or

{
    status: 'success'
}
```

### 物品信息

#### /item_add

描述：添加物品，保存新物品信息

请求参数示例：

```js
{
    item: {
        username: '123',
        itemname: '123',
        price: 123.1,
        description: 'A Item',
        leasetimes: 123,
        icon: 'http://localhost:8000/public/Images/1.png'
    }
}
```

返回：

```js
{
    status: 'error',
    errorMsg: 'Opps...'
}
```

#### /item_update

描述：更新 itemid 对应的物品的信息

请求参数示例：

```js
{
    item: {
        username: '123',
        itemname: '123',
        itemid: 123,
        price: 123.1,
        description: 'A Item',
        leasetimes: 123,
        icon: 'http://localhost:8000/public/Images/1.png'
    }
}
```

返回：

```js
{
    status: 'error',
    errorMsg: 'Opps...'
}

or

{
    status: 'success'
}
```

#### /item_getone

描述：通过 itemid 获取物品所有信息

请求参数示例：

```js
{
    item: {
        itemid: 123
    }
}
```

返回：

```js
{
    item: [{
        username: '123',
        itemname: '123',
        itemid: 123,
        price: 123.1,
        description: 'A Item',
        leasetimes: 123,
        icon: 'http://localhost:8000/public/Images/1.png'
    }]
}
```

#### /item_getpage

描述：通过 pageNumber 获取物品信息列表

请求参数示例：

```js
{
    pageNumber: 1
}
```

返回：

```js
{
    items: [
        {
            username: '123',
            itemname: '123',
            itemid: 123,
            price: 123.1,
            description: 'A Item',
            leasetimes: 123,
            icon: 'http://localhost:8000/public/Images/1.png'
        }, ...
    ]
}
```

#### /item_getonesitems

描述：通过 username 和 pageNumber 获取物品信息列表

请求参数示例：

```js
{
    pageNumber: 1,
    user: {
        username: '123'
    }
}
```

返回：

```js
{
    items: [
        {
            username: '123',
            itemname: '123',
            itemid: 123,
            price: 123.1,
            description: 'A Item',
            leasetimes: 123,
            icon: 'http://localhost:8000/public/Images/1.png'
        }, ...
    ]
}
```

#### /item_find

描述：通过 itemname 和 pageNumber 模糊查找并获取物品信息列表

请求参数示例：

```js
{
    pageNumber: 1,
    item: {
        itemname: '123'
    }
}
```

返回：

```js
{
    items: [
        {
            username: '123',
            itemname: '123',
            itemid: 123,
            price: 123.1,
            description: 'A Item',
            leasetimes: 123,
            icon: 'http://localhost:8000/public/Images/1.png'
        }, ...
    ]
}
```

### 图片

#### /image_upload

描述：上传一个图片并且获取其在服务器上的 url

请求方式示例：

```cs
public static async Task<bool> UploadFile(StorageFile file, string upload_url)
{
    try
    {
        System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
        var content = new MultipartFormDataContent();
        if (file != null)
        {
            var streamData = await file.OpenReadAsync();
            var bytes = new byte[streamData.Size];
            using (var dataReader = new DataReader(streamData))
            {
                await dataReader.LoadAsync((uint)streamData.Size);
                dataReader.ReadBytes(bytes);
            }
            var streamContent = new ByteArrayContent(bytes);
            content.Add(streamContent, "file", "icon.png"); // filename matters nothing
        }
        //client.DefaultRequestHeaders.Add("Access-Token", AccessToken);
        var response = await client.PostAsync(new Uri(upload_url, UriKind.Absolute), content);
        if (response.IsSuccessStatusCode)
            return true;
    }
    catch { return false; }

    return false;
}
```

返回：

```js
{
    status: 'success',
    url: 'http://localhost:8000/public/Images/1.png'
}
```