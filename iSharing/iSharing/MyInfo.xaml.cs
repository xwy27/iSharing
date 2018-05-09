using System;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace iSharing {
  /// <summary>
  /// 可用于自身或导航至 Frame 内部的空白页。
  /// </summary>
  public sealed partial class MyInfo : Page {
    public MyInfo() {
      this.InitializeComponent();
    }

    private async void UpdateInfo(object sender, RoutedEventArgs e) {
      string error = "";
      if (IPhone.Text.Length != 13) {
        error += "手机号码位数应为13\n";
      }
      if (IPhone.Text[0] != '1') {
        error += "手机号码格式错误\n";
      }
      if (!IMail.Text.Contains("@")) {
        error += "邮箱格式错误\n";
      }

      if (error != "") {
        var dialog = new MessageDialog(error);
        await dialog.ShowAsync();
      } else {
        // post
        var dialog = new MessageDialog("更新成功!\n");
        await dialog.ShowAsync();
      }
    }

    /**
     * 本地选择图片上传头像
     */
    private async void UpdataPhoto(object sender, RoutedEventArgs e) {
      FileOpenPicker picker = new FileOpenPicker();
      // Initialize the picture file type to take
      picker.FileTypeFilter.Add(".jpg");
      picker.FileTypeFilter.Add(".jpeg");
      picker.FileTypeFilter.Add(".png");
      picker.FileTypeFilter.Add(".bmp");
      picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;

      StorageFile file = await picker.PickSingleFileAsync();

      if (file != null) {
        // Load the selected picture
        IRandomAccessStream ir = await file.OpenAsync(FileAccessMode.Read);
        BitmapImage bi = new BitmapImage();
        await bi.SetSourceAsync(ir);
        photo.Source = bi;
      }
    }
  }
}