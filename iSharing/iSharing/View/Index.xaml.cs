﻿using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using iSharing.ViewModel;
using Newtonsoft.Json.Linq;
using iSharing.Models;
using System.Text.RegularExpressions;

namespace iSharing {
  /// <summary>
  /// 登陆注册页面。
  /// </summary>
  public sealed partial class IndexPage : Page {
    // 用户视图模型
    private UserViewModel viewModel = UserViewModel.GetInstance();
    // 错误信息
    private string error = "";

    public IndexPage() {
      this.InitializeComponent();
      error = "";
    }

    /**
     * 转到登陆页面
     */
    private void ToLogin(object sender, RoutedEventArgs e) {
      Rest();
      SignupPage.Visibility = Visibility.Collapsed;
      LoginPage.Visibility = Visibility.Visible;
    }

    /**
     * 转到注册页面
     */
    private void ToSignUp(object sender, RoutedEventArgs e) {
      Rest();
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
      bool wrong = false;
      error = "";

      string username = SUsername.Text;
      string password = SPassword.Password;
      string confirmpwd = SConfrimPwd.Password;
      string email = SMail.Text;
      string tel = SPhone.Text;

      if (username == "") {
        error += "请输入用户名\n";
      }
      if (username.Length > 30) {
        error += "用户名长度最大为30\n";
      }
      if (password == "") {
        error += "请输入密码\n";
      }
      if (confirmpwd == "") {
        error += "请输入确认密码\n";
      }
      if (password.Length < 6) {
        error += "密码长度不低于6\n";
      }
      if (password.Length > 20) {
        error += "密码长度最大为20\n";
      }
      if (confirmpwd != "" && confirmpwd != password) {
        error += "密码不一致\n";
      }
      if (tel == "") {
        error += "请输入电话\n";
      }
      if (tel != "" && tel.Length != 11) {
        error += "手机号码位数应为11\n";
      }
      if (tel != "" && tel[0] != '1') {
        error += "手机号码格式错误\n";
      }
      if (email == "") {
        error += "请输入邮箱\n";
      }
      if (email != "" && !email.Contains("@")) {
        error += "邮箱格式错误\n";
      }
      if (email.Length > 50) {
        error += "邮箱长度最大为50\ns";
      }

      if (error != "") {
        var dialog = new MessageDialog(error);
        await dialog.ShowAsync();
      } else {
        password = Post.EncodePsd(password);
        string jsonString = "{ \"user\": { " +
            "\"username\":\"" + username + "\"," +
            "\"password\":\"" + password + "\"," +
            "\"email\":\"" + email + "\"," +
            "\"tel\":\"" + tel + "\"}" +
          "}";
        // post
        string result = await Post.PostHttp("/user_add", jsonString);
        // Pharse json data
        JObject data = JObject.Parse(result);
        wrong = (data["status"].ToString() == "success") ? false : true;
        if (wrong) {
          error = data["errorMsg"].ToString();
          var dialog = new MessageDialog(error);
          await dialog.ShowAsync();
        } else {
          viewModel.CurrentUser.username = username;
          Frame.Navigate(typeof(MainPage));
        }
      }
    }

    /**
     * 登陆校验
     * 成功跳转至 APP 主页面
     * 失败弹出错误信息
     */
    private async void LogIn(object sender, RoutedEventArgs e) {
      bool wrong = false;
      error = "";

      string password = Post.EncodePsd(LPassword.Password);
      string jsonString = "{ \"user\" : {" +
          "\"username\":\"" + LUsername.Text + "\"," +
          "\"password\":\"" + password + "\"}" +
        "}";
      // post
      string result = await Post.PostHttp("/user_login", jsonString);
      // Pharse json data
      JObject data = JObject.Parse(result);
      wrong = (data["status"].ToString() == "success") ? false : true;
      error = wrong ? data["errorMsg"].ToString() : "";

      if (wrong) {
        var dialog = new MessageDialog(error);
        await dialog.ShowAsync();
      } else {
        viewModel.CurrentUser.username = LUsername.Text;
        Frame.Navigate(typeof(MainPage));
      }
    }

    /**
     * 清空注册页面和登陆页面表单
     */
    private void Rest() {
      error = "";
      LUsername.Text = "";
      LPassword.Password = "";

      SUsername.Text = "";
      SPassword.Password = "";
      SConfrimPwd.Password = "";
      SMail.Text = "";
      SPhone.Text = "";
    }

    private void SPhone_TextChanged(object sender, TextChangedEventArgs e) {
      int phone;
      if(!int.TryParse(SPhone.Text.ToString(), out phone)) {
        Regex number = new Regex("[^0-9]");
        string after = SPhone.Text.ToString();
        after = number.Replace(after, "");
        SPhone.Text = after;
      }
    }
  }
}