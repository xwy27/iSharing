# iSharing

<!-- TOC -->

- [iSharing](#isharing)
    - [代码规范](#)
    - [Design for Version 1.0](#design-for-version-10)
    - [DDL](#ddl)
    - [API](#api)
    - [iSharing FrontEnd](#isharing-frontend)
        - [Login & Signup](#login-signup)

<!-- /TOC -->

MOSAD midterm project: 租赁服务平台

## 代码规范

- 命名规范：
    - 函数名：
        - 先小后大驼峰式命名
        - 动词开头，再接名词
        - 例如：clickTab
    - 变量名：
        - 先小后大驼峰式命名
        - 尽量使用名词
        - 常量全字母大写
        - 允许包含阿拉伯数字
        - 例如：fileList
    - 参数名：
        - 同变量名
- 缩进：
    VSCode 下 google style 格式化文件
- 注释：
    - 变量注释：
        - 变量头部使用行注释
        - 例如：
            ```cs
            //文件名称
            string fileName;
            ```
    - 函数注释：
        - 函数头部使用区块注释
            - 提供函数功能描述
            - 提供参数类型及描述
            - 提供返回值类型及描述，如果返回值不定，只提供文字描述
        - 例如:
            ```cs
            /**
             * 计算两个整数和
             * @param  {int} lval 左操作数
             * @param  {int} rval 右操作数
             * @return {int} 两个整数和
            */
            int func(lval, rval) {
                return lval + rval;
            }
            ```

## Design for Version 1.0

1. 页面(Navigation view)
    - 登陆/注册页面(Xwy)
        - Username
        - Password(encode)
        - E-mail
        - Tel
        - 扣扣/微信
        - 头像
    - 浏览页面(Xya)
        - 物品图片
        - 物品名称
        - 租赁价格
    - 物品详情(Xya)
        - 物品名称 & 价格
        - 物品图片
        - 物品描述
        - 房东信息<!--
        已租次数
        评论列表
        评论框-->
    - 发布页面(Xya)
        - 物品名称
        - 物品价格
        - 物品图片
        - 物品描述
    - 个人信息(Xwy)
        - 头像(V2.0)
        - Username
        - Password(encode)
        - E-mail
        - Tel
        - 扣扣/微信<!--
        租赁记录-->
    - 物品管理(Xya)
        - 物品图片
        - 物品名称
        - 租赁价格
1. Database(Xyq,Xwy)
    - 用户表单
        - userId(int primary key)
        - 头像(blob)
        - Username(string unique)
        - Password(string)
        - E-mail(string)
        - Tel(string)
        - 扣扣/微信(string)
    - 物品表单
        - userId(int, foreignkey)
        - ItemId(int, primarykey)
        - 物品名称(string)
        - 物品价格(float)
        - 物品描述(string)
        - 物品图片(blob)
        - 已租次数(int)<!--
        租赁关系
        userId1(int, foreignkey)
        userId2(int, foreignkey)
        ItemId(int, foreignkey)
        rentId(int, primarykey)
    评论表单
        commentId(int, primarykey)
        userId(int, foreignkey)
        ItemId(int, foreignkey)
        time(string)-->
1. Server(Xyq，Xwy)

## DDL

1. UI 5.13
1. Server 5.13(hope)

## API

1. 图片上传

    url: localhost:8000/image_load
    
    response：
    
    ```js
    {
        statue: 'success',
        url: 'localhost:8000/public/2147813828.png'
    }
    ```

1. 用户注册

    url: localhost:8000

    request:

    ```js
    {
        user: {
            username: '123',
            password: '123',
            email: '1@1.1',
            tel: '12345678901'
        }
    }
    ```

    response:

    ```js
    {
        status: 'success',
        errorMsg: null
    }

    {
        status: 'error',
        errorMsg: '用户名重复'
    }
    ```

1. 获取用户的信息

    url: locaohost:8000/user_get
    
    request:

    ```js
    {
        username: '123'
    }
    ```

    response:
    
    ```js
    {
        user: {
            username: '123',
            password: '123',
            email: '1@1.1',
            tel: '12345678901',
            qq: '123456',
            wechat: '?',
            icon: 'localhost:8000/public/2147813828.png'
        }
    }
    ```

1. 更新用户信息

    url: localhost:8000/user_update

    request:

    ```js
    {
        user: {
            username: '123',
            password: '123',
            email: '1@1.1',
            tel: '12345678901',
            qq: '123456',
            wechat: '?',
            icon: 'localhost:8000/public/2147813828.png'
        }
    }
    ```

    response:

    ```js
    {
        status: 'success',
        errorMsg: null
    }

    {
        status: 'error',
        errorMsg: 'Opps'
    }
    ```

1. 物品信息

## iSharing FrontEnd

### Login & Signup

1. **2018.5.9 Xwy**
    1. 登陆注册同页面
    1. 注册基础校验
        - 手机号码长13位
        - 手机号码必须以1开头
        - 邮箱包含'@'字符
    1. 个人信息页面
        - 上传图片修改头像
        - 修改个人信息数据
