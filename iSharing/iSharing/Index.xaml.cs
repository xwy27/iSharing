using System;
using System.Diagnostics;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Newtonsoft.Json.Linq;

namespace iSharing {
  /// <summary>
  /// 登陆注册页面。
  /// </summary>
  public sealed partial class IndexPage : Page {
    public IndexPage () {
      this.InitializeComponent ();
    }

    /**
     * 转到登陆页面
     */
    private void ToLogin (object sender, RoutedEventArgs e) {
      Rest ();
      SignupPage.Visibility = Visibility.Collapsed;
      LoginPage.Visibility = Visibility.Visible;
    }

    /**
     * 转到注册页面
     */
    private void ToSignUp (object sender, RoutedEventArgs e) {
      Rest ();
      SignupPage.Visibility = Visibility.Visible;
      LoginPage.Visibility = Visibility.Collapsed;
    }

    /**
     * 注册校验
     * 校验失败，弹出错误详细信息
     * 校验成功，发送服务器再校验
     * 服务器校验成功，跳转 APP 主页面
     * 服务器校验失败，弹出错误信息
     */
    private async void SignUp(object sender, RoutedEventArgs e) {
      string error = "";

      string username = SUsername.Text;
      string password = SPassword.Password;
      string confirmpwd = SConfrimPwd.Password;
      string email = SMail.Text;
      string tel = SPhone.Text;

      if (username == "") {
        error += "请输入用户名\n";
      }
      if (password == "") {
        error += "请输入密码\n";
      }
      if (confirmpwd == "") {
        error += "请输入确认密码\n";
      }
      if (tel == "") {
        error += "请输入电话\n";
      }
      if (email == "") {
        error += "请输入邮箱\n";
      }
      if (confirmpwd != "" && confirmpwd != password) {
        error += "密码不一致\n";
      }
      if (tel != "" && tel.Length != 11) {
        error += "手机号码位数应为11\n";
      }
      if (tel != "" && tel[0] != '1') {
        error += "手机号码格式错误\n";
      }
      if (email != "" && !email.Contains ("@")) {
        error += "邮箱格式错误\n";
      }

      if (error != "") {
        var dialog = new MessageDialog (error);
        await dialog.ShowAsync ();
      } else {
        string jsonString = "{\"username\":\"" + username + "\"," + "\"password\":\"" + password + "\","
          + "\"email\":\"" + email + "\"," + "\"tel\":\"" + tel + "\"}";
        JObject signupJson = JObject.Parse(jsonString);
        // post
        Frame.Navigate (typeof (MainPage));
      }
    }

    /**
     * 登陆校验
     * 成功跳转至 APP 主页面
     * 失败弹出错误信息
     */
    private void LogIn (object sender, RoutedEventArgs e) {
      string jsonString = "{\"username\":\"" + LUsername.Text + "\"," + "\"password\":\"" + LPassword.Password + "\"}";
      JObject loginJson = JObject.Parse(jsonString);
      // post
      try {
        Frame.Navigate (typeof (MainPage));
      } catch (Exception ex) {
        Debug.WriteLine (ex.Message + ex.StackTrace);
      }
    }

    /**
     * 清空注册页面和登陆页面表单
     */
    private void Rest () {
      LUsername.Text = "";
      LPassword.Password = "";

      SUsername.Text = "";
      SPassword.Password = "";
      SConfrimPwd.Password = "";
      SMail.Text = "";
      SPhone.Text = "";
    }
  }
}