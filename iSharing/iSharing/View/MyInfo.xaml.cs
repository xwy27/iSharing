using System;
using System.Diagnostics;
using iSharing.Models;
using iSharing.ViewModel;
using Newtonsoft.Json.Linq;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace iSharing {
  /// <summary>
  /// 个人信息页面
  /// </summary>
  public sealed partial class MyInfo : Page {
    // 用户视图模型
    private UserViewModel viewModel = UserViewModel.GetInstance ();

    public MyInfo () {
      try {
        this.InitializeComponent ();
        GetInfo();
      } catch (Exception ex) {
        Debug.WriteLine (ex.Message + ex.StackTrace);
      }
    }

    protected override void OnNavigatedFrom(NavigationEventArgs e) {
      //如果选择了头像但未上传，删除该记录
      if (ApplicationData.Current.LocalSettings.Values.ContainsKey("MyToken")) {
        ApplicationData.Current.LocalSettings.Values.Remove("MyToken");
      }
    }

    /**
     * 从服务器请求个人信息
     * 显示个人信息页面信息
     */
    private async void GetInfo() {
      string jsonString = "{ \"user\" : {\"username\":\"" + viewModel.CurrentUser.username + "\"} }";

      string result = await Models.Post.PostHttp("/user_get", jsonString);

      JObject data = JObject.Parse(result);
      viewModel.CurrentUser.username = data["user"]["username"].ToString();
      viewModel.CurrentUser.Password = Post.DecodePsd(data["user"]["password"].ToString());
      viewModel.CurrentUser.Mail = data["user"]["email"].ToString();
      viewModel.CurrentUser.Phone = data["user"]["tel"].ToString();
      viewModel.CurrentUser.QQ = data["user"]["qq"].ToString();
      viewModel.CurrentUser.Wechat = data["user"]["wechat"].ToString();
      viewModel.CurrentUser.PhotoUrl = data["user"]["icon"].ToString();

      if (viewModel.CurrentUser.PhotoUrl != "") {
        Windows.Web.Http.HttpClient http = new Windows.Web.Http.HttpClient();
        IBuffer buffer = await http.GetBufferAsync(new Uri("http://" + viewModel.CurrentUser.PhotoUrl));
        BitmapImage img = new BitmapImage();
        using (IRandomAccessStream stream = new InMemoryRandomAccessStream()) {
          await stream.WriteAsync(buffer);
          stream.Seek(0);
          await img.SetSourceAsync(stream);
          photo.ImageSource = img;
        }
      }
    }

    /**
     * 更新个人信息
     * 成功，弹出成功信息
     * 失败，弹出错误信息
     */
    private async void UpdateInfo(object sender, RoutedEventArgs e) {
      bool show = false;
      string error = "";

      string username = viewModel.CurrentUser.username;
      string password = viewModel.CurrentUser.Password;
      string email = viewModel.CurrentUser.Mail;
      string tel = viewModel.CurrentUser.Phone;
      string qq = viewModel.CurrentUser.QQ;
      string wechat = viewModel.CurrentUser.Wechat;

      if (viewModel.CurrentUser.Password.Length < 6) {
        error += "密码长度最小为6\n";
      }
      if (viewModel.CurrentUser.Password.Length > 20) {
        error += "密码长度最大为20\n";
      }
      if (viewModel.CurrentUser.Phone.Length != 11) {
        error += "手机号码位数应为11\n";
      }
      if (viewModel.CurrentUser.Phone[0] != '1') {
        error += "手机号码格式错误\n";
      }
      if (!viewModel.CurrentUser.Mail.Contains("@")) {
        error += "邮箱格式错误\n";
      }
      if (viewModel.CurrentUser.Mail.Length > 50) {
        error += "邮箱长度最大为50\n";
      }
      if (viewModel.CurrentUser.QQ.Length > 20) {
        error += "QQ 号码最长为20\n";
      }
      if (viewModel.CurrentUser.Wechat.Length > 30) {
        error += "微信号最长为30\n";
      }

      if (error != "") {
        var dialog = new MessageDialog(error);
        await dialog.ShowAsync();
      } else {
        // post photo
        StorageFile theFile;
        if (ApplicationData.Current.LocalSettings.Values.ContainsKey("MyToken")) {
          if ((string)ApplicationData.Current.LocalSettings.Values["MyToken"] != "") {
            theFile = await StorageApplicationPermissions.FutureAccessList.GetFileAsync(
                (string)ApplicationData.Current.LocalSettings.Values["MyToken"]);
            ApplicationData.Current.LocalSettings.Values.Remove("MyToken");
          } else {
            theFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/logo.jpg"));
          }
        } else {
          theFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/logo.jpg"));
        }
        if (theFile != null) {
          var photoResult = await Post.PostPhoto(theFile);
          // Pharse the json data
          JObject photoData = JObject.Parse(photoResult);
          var msg = (photoData["status"].ToString() == "success") ? "上传成功\n" : "上传失败\n";
          viewModel.CurrentUser.PhotoUrl = photoData["url"].ToString();
        }

        password = Post.EncodePsd(password);
        // post userInfo
        string jsonString = "{ \"user\" : {" +
            "\"username\":\"" + username + "\"," +
            "\"password\":\"" + password + "\"," +
            "\"email\":\"" + email + "\"," + "\"tel\":\"" + tel + "\"," +
            "\"qq\":\"" + qq + "\"," +
            "\"wechat\":\"" + wechat + "\"," +
            "\"icon\":\"" + viewModel.CurrentUser.PhotoUrl + "\"}" +
          "}";
        // post
        string result = await Post.PostHttp("/user_update", jsonString);
        // Pharse the json data
        JObject data = JObject.Parse(result);
        show = (data["status"].ToString() == "success") ? false : true;
        error += show ? data["errorMsg"].ToString() : "修改成功\n";
        var dialog = new MessageDialog(error);
        await dialog.ShowAsync();
      }
    }

    /**
     * 本地选择头像图片
     */
    private async void UpdatePhoto (object sender, RoutedEventArgs e) {
      FileOpenPicker picker = new FileOpenPicker ();
      // Initialize the picture file type to take
      picker.FileTypeFilter.Add (".jpg");
      picker.FileTypeFilter.Add (".jpeg");
      picker.FileTypeFilter.Add (".png");
      picker.FileTypeFilter.Add (".bmp");
      picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;

      StorageFile file = await picker.PickSingleFileAsync ();

      if (file != null) {
        ApplicationData.Current.LocalSettings.Values["MyToken"] =
            StorageApplicationPermissions.FutureAccessList.Add(file);
        // Load the selected picture
        IRandomAccessStream ir = await file.OpenAsync (FileAccessMode.Read);
        BitmapImage bi = new BitmapImage ();
        await bi.SetSourceAsync (ir);
        photo.ImageSource = bi;
      }
    }
  }
}