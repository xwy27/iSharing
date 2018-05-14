﻿using System;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace iSharing {
  /// <summary>
  /// 可用于自身或导航至 Frame 内部的空白页。
  /// </summary>
  public sealed partial class EditItem : Page {
    public EditItem () {
      this.InitializeComponent ();
    }

    private async void Pick_Click (object sender, RoutedEventArgs e) {
      var picker = new Windows.Storage.Pickers.FileOpenPicker ();
      picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
      picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
      picker.FileTypeFilter.Add (".jpg");
      picker.FileTypeFilter.Add (".jpeg");
      picker.FileTypeFilter.Add (".png");
      picker.FileTypeFilter.Add (".bmp");

      Windows.Storage.StorageFile file = await picker.PickSingleFileAsync ();
      if (file != null) {
        using (var stream = await file.OpenAsync (Windows.Storage.FileAccessMode.Read)) {
          SharedStorageAccessManager.AddFile (file);
          var bitmap = new BitmapImage ();
          await bitmap.SetSourceAsync (stream);
          Picture.Source = bitmap;
        }
      }
    }

    private void Submit_Click (object sender, RoutedEventArgs e) {

    }
  }
}