using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace iSharing
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void ToLogin(object sender, RoutedEventArgs e) {
            SignupPage.Visibility = Visibility.Collapsed;
            LoginPage.Visibility = Visibility.Visible;
        }

        private void ToSignUp(object sender, RoutedEventArgs e) {
            SignupPage.Visibility = Visibility.Visible;
            LoginPage.Visibility = Visibility.Collapsed;
        }

        private async void SignUp(object sender, RoutedEventArgs e) {
            string error = "";
            if (SConfrimPwd.Text != SPassword.Text) {
                error += "密码不一致\n";
            }
            if (SPhone.Text.Length != 13) {
                error += "手机号码位数应为13\n";
            }
            if (SPhone.Text[0] != '1') {
                error += "手机号码格式错误\n";
            }
            if (!SMail.Text.Contains("@")) {
                error += "邮箱格式错误\n";
            }

            if (error != "") {
                var dialog = new MessageDialog(error);
                await dialog.ShowAsync();
            } else {
                // post
            }
        }

        private void LogIn(object sender, RoutedEventArgs e) {
            // post
        }
    }
}
