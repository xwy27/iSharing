/**************************************************
 *  Author      ：xwy (xuwy27@mail2.sysu.edu.cn)
 *  Time        ：2018/5/10 14:46:18
 *  Filename    ：ViewModel
 *  Version     : V1.0.1  
 *  Description : 视图模型
 **************************************************/

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using iSharing.Models;
using Windows.UI.Xaml.Media.Imaging;

namespace iSharing.ViewModel {

  class UserViewModel {
    // 初始头像
    private BitmapImage originPhoto = new BitmapImage ();

    // 单例用户视图模型
    private static UserViewModel instance;

    // 可更新用户数据模型集合，可用来进行数据绑定
    private ObservableCollection<User> user = new ObservableCollection<User> ();
    public ObservableCollection<User> User { get { return this.user; } }

    // 当前用户
    private User currentUser = default (User);

    // 当前用户数据模型接口
    public User CurrentUser {
      get { return this.currentUser; }
      set { this.currentUser = value; }
    }

    /**
     * 构造函数
     * 测试：使用静态数据构造
     * 上线：使用服务器返回数据构造
     */
    private UserViewModel () {
      try {
        this.User.Add (new User ("Test", "Test", "123@qq.com", "12345678901", "ms-appx///Assets/photo.jpg"));
        currentUser = User[0];
      } catch (Exception ex) {
        Debug.WriteLine (ex.Message + ex.StackTrace);
      }
    }

    /**
     * 单例模式
     * @return {UserViewModel} 返回单例用户视图模型
     */
    public static UserViewModel GetInstance () {
      return instance ?? (instance = new UserViewModel ());
    }

    /**
     * 更新用户信息
     * @param {string} password 新密码
     * @param {string} mail 新邮箱
     * @param {string} phone 新电话
     * @param {string} photoUrl 新头像
     * @param {string} wechat 新微信号
     * @param {string} qq 新qq号
     */
    public void UpdateUserInfo (string password, string mail, string phone,
      string photoUrl, string wechat, string qq) {
      if (this.currentUser != null) {
        this.currentUser.Password = password;
        this.currentUser.Mail = mail;
        this.currentUser.Phone = phone;
        this.currentUser.PhotoUrl = photoUrl;
        this.currentUser.Wechat = wechat;
        this.currentUser.QQ = qq;
      }
    }
  }
}