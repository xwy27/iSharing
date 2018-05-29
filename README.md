# iSharing FrontEnd

## Login & Signup

1. **2018.5.9-13**
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

1. **2018.5.18**
    1. 增加信息合法性校验(√)
        - 用户名长度不超过30
        - 密码长度不超过20
        - 邮箱长度不超过50
        - 微信长度不超过30
        - qq 号码长度不超过20
    1. 标题栏透明化(√)
    1. 返回登陆页面(x)
        - 直接进入主页面，未登录，ban 其余界面，登录后解除限制。
        - homepage 可以登出
    1. 密码加密(√)
    1. 实现登陆注册(√)
    1. 个人信息更新和获取(√)
1. **2018.5.19-5.28**
    1. 前端发送数据到服务器的代码提取(√)
    1. UI 设计(xya)(√)
    1. 物品页面逻辑(待5.30测试)
1. **2018.5.29**
    1. Review code(等修改完毕)
        - 去除合作时的重复代码(√)
        - 审查代码注释和易读性
    1. 设计 logo(ing)
1. **关于 post 到服务器说明**
    1. 调用 Models 下的 Post 类的 PostHttp 方法，传入服务端 api 提供的 url 和需要上传的 json 数据；
    1. 函数返回服务器响应的字符串；
    1. 解析字符串获得数据；
    例如注册账号:
    
    - url: /user_add
    - 请求 json 示例
    
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
    
    - 返回 json 示例：

    ```js
    {
        status: 'error',
        errorMsg: 'Opps...'
    }

    //or

    {
        status: 'success'
    }
    ```

    - 后台请求，处理返回数据示例:

    ```cs
    string jsonString = "{ \"user\": { " +
        "\"username\":\"" + username + "\"," +
        "\"password\":\"" + password + "\"," +
        "\"email\":\"" + email + "\"," +
        "\"tel\":\"" + tel + "\"}" +
    "}";
    // post
    string result = await Models.Post.PostHttp("/user_add", jsonString);
    // Pharse json data
    JObject data = JObject.Parse(result);
    wrong = (data["status"].ToString() == "success") ? false : true;
    error = data["errorMsg"].ToString();
    ```
