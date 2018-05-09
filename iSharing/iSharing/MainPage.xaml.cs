using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace iSharing {
  /// <summary>
  /// 可用于自身或导航至 Frame 内部的空白页。
  /// </summary>
  public sealed partial class MainPage : Page {
    public MainPage () {
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
    private async void SignUp (object sender, RoutedEventArgs e) {
      string error = "";
      if (SConfrimPwd.Password != SPassword.Password) {
        error += "密码不一致\n";
      }
      if (SPhone.Text.Length != 13) {
        error += "手机号码位数应为13\n";
      }
      if (SPhone.Text[0] != '1') {
        error += "手机号码格式错误\n";
      }
      if (!SMail.Text.Contains ("@")) {
        error += "邮箱格式错误\n";
      }

      if (error != "") {
        var dialog = new MessageDialog (error);
        await dialog.ShowAsync ();
      } else {
        // post
        Frame.Navigate (typeof (MyInfo));
      }
    }

    /**
     * 登陆校验
     * 成功跳转至 APP 主页面
     * 失败弹出错误信息
     */
    private void LogIn (object sender, RoutedEventArgs e) {
      // post
      Frame.Navigate (typeof (MyInfo));
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