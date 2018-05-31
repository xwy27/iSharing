# iSharing

<!-- TOC -->

- [iSharing](#isharing)
  - [Code Format](#code-format)
  - [Design for Version 1.0](#design-for-version-10)
  - [iSharing FrontEnd Schedule](#isharing-frontend-schedule)
    - [2018.5.9-13](#201859-13)
    - [2018.5.18](#2018518)
    - [2018.5.19-5.28](#2018519-528)
    - [2018.5.29](#2018529)
    - [2018.5.31](#2018531)

<!-- /TOC -->

MOSAD midterm project: 租赁服务平台

## Code Format

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
      - 回调函数注释:
        ```js
        /**
          * @class
          */
          function MyClass() {}

          /**
          * Do asynchronous stuff.
          * @param {MyClass~onSuccess} onSuccess - Called if doing asynchronous stuff succeeds.
          */
          MyClass.prototype.doAsyncStuff = function(onSuccess) {
            // ...
          };

          /**
          * @function MyClass~onSuccess
          * @param {String} result - The result of the asynchronous process.
          * @param {Number} int - A result code.
          * @return undefined
          */
        ```

## Design for Version 1.0

1. 页面(Navigation view)
    - 登陆/注册页面(Xwy)
    - 浏览页面(Xya)
    - 物品详情(Xya)
    - 发布页面(Xya)
    - 个人信息(Xwy)
    - 物品管理(Xya)
1. Database(Xyq,Xwy)
    - 用户表单
        - 头像(url) maxlength: 60000+
        - Username(string unique) maxlength: 30
        - Password(string) maxlength: 20
        - E-mail(string) maxlength: 50
        - Tel(string) maxlength: 11
        - qq(string) maxlength: 20
        - wechat(string) maxlength: 30 
    - 物品表单
        - userId(int, foreignkey)
        - ItemId(int, primarykey)
        - 物品名称(string)
        - 物品价格(float)
        - 物品描述(string)
        - 物品图片(blob)
1. Server(Xyq，Xwy)

## iSharing FrontEnd Schedule

### 2018.5.9-13

1. 登陆注册同页面(√)
1. 注册基础校验(√)
    - 信息不为空
    - 手机号码长11位
    - 手机号码必须以1开头
    - 邮箱包含'@'字符
1. 个人信息页面(√)
    - 上传图片修改头像
    - 修改个人信息数据
1. 头像 post 和 response 解析代码(√)
1. 表单 json 生成(√)

### 2018.5.18

1. 增加信息合法性校验(√)
    - 用户名
    - 密码
    - 邮箱
    - 微信
    - qq 号码
1. 标题栏透明化(√)
1. 返回登陆页面(√)
    - mainpage 可以登出(√)
1. 密码加密(√)
1. 实现登陆注册(√)
1. 个人信息更新和获取(√)

### 2018.5.19-5.28

1. 前端发送数据到服务器的代码提取(√)
1. UI 设计(√)
1. 物品页面逻辑(√)

### 2018.5.29

1. Review code(√)
    - 去除合作时的重复代码(√)
    - 审查代码注释和易读性(√)
1. 设计 logo(√)

### 2018.5.31

1. ItemDetail页面增加Provider的联系方式们(√)
2. 改善ItemDetail页面的排版(√)
3. 改善ItemList页面的排版(√)
4. EditItem页面的Price文本框的placeholder(√)
5. EditItem页面提交时验证物品名称与价格非空(√)
6. EditItem页面提交时未选择图片则提交默认图片(离开页面与本次提交完时删除选择记录)(√)
7. ItemDetail页面增加分享按钮与其功能，能分享至邮件(√)
8. 增加我的物品页面，此页面仅显示本人发布的物品，点击能修改(√)