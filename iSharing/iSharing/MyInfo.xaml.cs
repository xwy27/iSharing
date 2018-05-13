using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using iSharing.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

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
        photo.Source = viewModel.CurrentUser.Photo;
      } catch (Exception ex) {
        Debug.WriteLine (ex.Message + ex.StackTrace);
      }
    }

    /**
     * 更新个人信息
     * 成功，弹出成功信息
     * 失败，弹出错误信息
     */
    private async void UpdateInfo (object sender, RoutedEventArgs e) {
      try {
        string error = "";

        string username = viewModel.CurrentUser.username;
        string password = viewModel.CurrentUser.Password;
        string email = viewModel.CurrentUser.Mail;
        string tel = viewModel.CurrentUser.Phone;
        string qq = viewModel.CurrentUser.QQ;
        string wechat = viewModel.CurrentUser.Wechat;

        if (viewModel.CurrentUser.Phone.Length != 11) {
          error += "手机号码位数应为11\n";
        }
        if (viewModel.CurrentUser.Phone[0] != '1') {
          error += "手机号码格式错误\n";
        }
        if (!viewModel.CurrentUser.Mail.Contains ("@")) {
          error += "邮箱格式错误\n";
        }

        if (error != "") {
          var dialog = new MessageDialog (error);
          await dialog.ShowAsync ();
        } else {
          // post photo
          if (ApplicationData.Current.LocalSettings.Values.ContainsKey("MyToken")) {
            if ((string)ApplicationData.Current.LocalSettings.Values["MyToken"] != "") {
              StorageFile theFile = await StorageApplicationPermissions.FutureAccessList.GetFileAsync(
                  (string)ApplicationData.Current.LocalSettings.Values["MyToken"]);
              if (theFile != null) {
                //error += UploadPhoto(theFile, "");
              }
            }
          }
          
          // post userInfo
          string jsonString = "{\"username\":\"" + username + "\"," + "\"password\":\"" + password + "\","
            + "\"email\":\"" + email + "\"," + "\"tel\":\"" + tel + "\","
            + "\"qq\":\"" + qq + "\"," + "\"wechat\":\"" + wechat + "\","
            + "\"icon\":\"" + viewModel.CurrentUser.PhotoUrl + "\"}";
          JObject signupJson = JObject.Parse(jsonString);
          // post

          error += viewModel.CurrentUser.Mail;
          var dialog = new MessageDialog (error);
          await dialog.ShowAsync ();
        }
      } catch(Exception ex) {
        Debug.WriteLine(ex.Message + ex.StackTrace);
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
        photo.Source = bi;
      }
    }

    /**
     * 头像上传服务器
     * @param {StorageFile} file 头像文件
     * @param {string} uploadUrl 上传网址
     * @return {string} 成功返回成功信息，失败返回错误信息
     */
    private async Task<string> UploadPhoto(StorageFile file, string uploadUrl) {
      string msg = "";
      try {
        HttpClient client = new System.Net.Http.HttpClient();
        var content = new MultipartFormDataContent();
        if (file != null) {
          var streamData = await file.OpenReadAsync();
          var bytes = new byte[streamData.Size];
          using (var dataReader = new DataReader(streamData)) {
            await dataReader.LoadAsync((uint)streamData.Size);
            dataReader.ReadBytes(bytes);
          }
          var streamContent = new ByteArrayContent(bytes);
          content.Add(streamContent, "file");
        }
        var response = await client.PostAsync(new Uri(uploadUrl, UriKind.Absolute), content);
        if (response.IsSuccessStatusCode) {
          // Set encoding to 'UTF-8'
          Byte[] getByte1 = await response.Content.ReadAsByteArrayAsync();
          Encoding code1 = Encoding.GetEncoding("UTF-8");
          string result1 = code1.GetString(getByte1, 0, getByte1.Length);
          // Pharse the json data
          JsonReader reader = new JsonTextReader(new StringReader(result1));
          while (reader.Read()) {
            if ((String)reader.Value == "statue") {
              reader.Read();
              if ((String)reader.Value == "success") {
                msg += "上传成功\n";
              } else {
                // error
                msg += "上传失败\n";
                break;
              }
            } else if ((String)reader.Value == "url") {
              reader.Read();
              viewModel.CurrentUser.PhotoUrl = (string)reader.Value;
            }
          }
          if (msg != "") {
            return msg;
          }
          msg += "上传失败\n";
          return msg;
        }
      } catch (Exception ex) {
        msg += "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message + "\n";
        return msg;
      }
      msg += "上传失败\n";
      return msg;
    }
  }
}