using iSharing.ViewModel;
using System;
using Windows.ApplicationModel.DataTransfer;
using Windows.Data.Json;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace iSharing {
  /// <summary>
  /// 可用于自身或导航至 Frame 内部的空白页。
  /// </summary>
  public sealed partial class EditItem : Page {

    private ItemViewModel itemViewModel = ItemViewModel.GetInstance();
    private UserViewModel userViewModel = UserViewModel.GetInstance ();
    public EditItem () {
      this.InitializeComponent ();
    }
    
    protected override void OnNavigatedTo(NavigationEventArgs e) {
      if ((String)e.Parameter == "new") {
        itemViewModel.SelectIndex = -1;
      }
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
      string jsonString = "";
      if (itemViewModel.SelectIndex == -1) {
        jsonString = "{\"item\":{" + "\"username\":\"" + userViewModel.CurrentUser.username +
                     "\",\"itemname\":" + Itemname.Text + ",\"price\":" + Price.Text +
                     ",\"description\":" + Description.Text + ",\"leasetimes\":0}}";

        
      } else {
        jsonString = "{\"item\":{" + "\"username\":" + userViewModel.CurrentUser.username +
                     ",\"itemname\":" + itemViewModel.SelectItem.Itemname + ",\"itemid\":" + itemViewModel.SelectItem.Itemid +
                     ",\"price\":" + itemViewModel.SelectItem.Price + ",\"description\":" + itemViewModel.SelectItem.Description +
                     ",\"leasetimes\":0}}";
      }
      JsonObject json = JsonObject.Parse(jsonString);
      
    }
  }
}