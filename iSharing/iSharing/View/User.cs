/**************************************************
 *  Author      ：xwy (xuwy27@mail2.sysu.edu.cn)
 *  Time        ：2018/5/10 14:24:16
 *  Filename    ：UserModel
 *  Version     : V1.0.1  
 *  Description : 用户数据模型
 **************************************************/

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml.Media.Imaging;

namespace iSharing.Models {
  public class User : INotifyPropertyChanged {
    // 用户头像
    private BitmapImage photo;

    // 用户名
    public string username;

    // 用户密码
    private string password;

    // 用户邮箱
    private string mail;

    // 用户电话
    private string phone;

    // 用户微信
    private string wechat;

    // 用户QQ
    private string qq;

    // 属性更改处理器
    public event PropertyChangedEventHandler PropertyChanged;

    /**
     * 属性更新函数
     * @param {string} propertyName 更新的属性名称
     */
    private void NotifyPropertyChanged ([CallerMemberName] String propertyName = "") {
      if (PropertyChanged != null) {
        PropertyChanged (this, new PropertyChangedEventArgs (propertyName));
      }
    }

    // 密码接口
    public string Password {
      get { return this.password; }
      set {
        this.password = value;
        this.NotifyPropertyChanged ("Password");
      }
    }

    // 邮箱接口
    public string Mail {
      get { return this.mail; }
      set {
        this.mail = value;
        this.NotifyPropertyChanged ("Mail");
      }
    }

    // 电话接口
    public string Phone {
      get { return this.phone; }
      set {
        this.phone = value;
        this.NotifyPropertyChanged ("Phone");
      }
    }

    // 微信接口
    public string Wechat {
      get { return this.wechat; }
      set {
        this.wechat = value;
        this.NotifyPropertyChanged ("Wechat");
      }
    }

    // QQ接口
    public string QQ {
      get { return this.qq; }
      set {
        this.qq = value;
        this.NotifyPropertyChanged ("QQ");
      }
    }

    // 头像接口
    public BitmapImage Photo {
      get { return this.photo; }
      set {
        this.photo = value;
        this.NotifyPropertyChanged ("Photo");
      }
    }

    /**
     * 构造函数，根据给定数据构造用户
     * @param {string} password 密码
     * @param {string} mail 邮箱
     * @param {string} phone 电话
     * @param {BitmapImage} photo 头像
     * @param {string} wechat 微信号，可为空
     * @param {string} qq qq号，可为空
     */
    public User (string username, string password, string mail, string phone,
      BitmapImage photo, string wechat = "", string qq = "") {
      try {
        this.username = username;
        this.password = password;
        this.mail = mail;
        this.phone = phone;
        this.wechat = wechat;
        this.qq = qq;
        this.photo = photo;
      } catch (Exception ex) {
        Debug.WriteLine (ex.Message + ex.StackTrace);
      }
    }
  }
}