using System;
using System.Diagnostics;
using iSharing.ViewModel;
using Windows.Storage;
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
      string error = "";
      if (viewModel.CurrentUser.Phone.Length != 13) {
        error += "手机号码位数应为13\n";
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
        // post
        //viewModel.UpdateUserInfo();
        var dialog = new MessageDialog ("更新成功!\n");
        await dialog.ShowAsync ();
      }
    }

    /**
     * 本地选择图片上传头像
     */
    private async void UpdataPhoto (object sender, RoutedEventArgs e) {
      FileOpenPicker picker = new FileOpenPicker ();
      // Initialize the picture file type to take
      picker.FileTypeFilter.Add (".jpg");
      picker.FileTypeFilter.Add (".jpeg");
      picker.FileTypeFilter.Add (".png");
      picker.FileTypeFilter.Add (".bmp");
      picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;

      StorageFile file = await picker.PickSingleFileAsync ();

      if (file != null) {
        // Load the selected picture
        IRandomAccessStream ir = await file.OpenAsync (FileAccessMode.Read);
        BitmapImage bi = new BitmapImage ();
        await bi.SetSourceAsync (ir);
        photo.Source = bi;
      }
    }
  }
}