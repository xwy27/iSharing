using iSharing.Models;
using iSharing.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.UI.Popups;
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
    
    /** 图片选择器并替换当前图片显示
     */
    private async void Pick_Click (object sender, RoutedEventArgs e) {
      var picker = new Windows.Storage.Pickers.FileOpenPicker ();
      picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
      picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
      picker.FileTypeFilter.Add (".jpg");
      picker.FileTypeFilter.Add (".jpeg");
      picker.FileTypeFilter.Add (".png");
      picker.FileTypeFilter.Add (".bmp");

      StorageFile file = await picker.PickSingleFileAsync ();
      if (file != null) {
        ApplicationData.Current.LocalSettings.Values["ItemPic"] =
            StorageApplicationPermissions.FutureAccessList.Add(file);
        using (var stream = await file.OpenAsync (Windows.Storage.FileAccessMode.Read)) {
          var bitmap = new BitmapImage ();
          await bitmap.SetSourceAsync (stream);
          Picture.Source = bitmap;
        }
      }
    }
    

    /** 点击将物品信息提交至服务器
     * Index: 判断物品是新的还是已存在的
     */
    private async void Submit_Click (object sender, RoutedEventArgs e) {
      string error = "";
      if (Itemname.Equals("")) { 
        error += "物品名不能为空!";
        var dialog = new MessageDialog(error);
        await dialog.ShowAsync();
      } else {
        string jsonString = "";
        string result = "";
        string picurl = await postPic();
        if (itemViewModel.SelectIndex == -1) {
          jsonString = "{\"item\":{" + "\"username\":\"" + userViewModel.CurrentUser.username +
                       "\",\"itemname\":\"" + Itemname.Text + "\",\"price\":" + float.Parse(Price.Text) +
                       ",\"description\":\"" + Description.Text + "\",\"leasetimes\":0" + ",\"icon\":\"" + picurl + "\"}}";
          result = await Post.PostHttp("/item_add", jsonString);
        
          JObject data = JObject.Parse(result);
          error = (data["status"].ToString() == "error") ? data["errorMsg"].ToString() : "提交成功！\n";
          var dialog = new MessageDialog(error);
          await dialog.ShowAsync();
        } else {
          jsonString = "{\"item\":{" + "\"username\":" + userViewModel.CurrentUser.username +
                       ",\"itemname\":" + itemViewModel.SelectItem.Itemname + ",\"itemid\":" + itemViewModel.SelectItem.Itemid +
                       ",\"price\":" + itemViewModel.SelectItem.Price + ",\"description\":" + itemViewModel.SelectItem.Description +
                       ",\"leasetimes\":0" + ",\"icon\":" + picurl + "}}";
          result = await Post.PostHttp("/item_update", jsonString);
        
          JObject data = JObject.Parse(result);
          error = (data["status"].ToString() == "error") ? data["errorMsg"].ToString() : "提交成功！\n";
          var dialog = new MessageDialog(error);
          await dialog.ShowAsync();
        }
      }
      
    }
    
    private async Task<string> postPic() { 
      string result = "";
      if (ApplicationData.Current.LocalSettings.Values.ContainsKey("ItemPic")) {
        string fileToken = (string)ApplicationData.Current.LocalSettings.Values["MyToken"];
        if (fileToken != "") {
          StorageFile file = await StorageApplicationPermissions.FutureAccessList.GetFileAsync(fileToken);
          /*if (file != null) {
            System.Net.Http.HttpClient http = new System.Net.Http.HttpClient();
            var content = new MultipartFormDataContent();
            var stream = await file.OpenReadAsync();
            var bytes = new byte[stream.Size];
            using (var dataReader = new DataReader(stream)) { 
              await dataReader.LoadAsync((uint)stream.Size);
              dataReader.ReadBytes(bytes);
            }
            content.Add(new StreamContent(new MemoryStream(bytes)), "file", "icon.jpg");

            var response = await http.PostAsync(new Uri("http://localhost:8000/image_upload", UriKind.Absolute), content);
            if (response.IsSuccessStatusCode) {
              Byte[] responseByte = await response.Content.ReadAsByteArrayAsync();
              result = Encoding.GetEncoding("UTF-8").GetString(responseByte);
              JObject json = JObject.Parse(result);
              result = json["url"].ToString();
            }
          }*/
          result = await Post.PostPhoto(file);
          JObject json = JObject.Parse(result);
          result = json["url"].ToString();
        }
      }
      return result;  
    }

    private void Price_TextChanged(object sender, TextChangedEventArgs e) {
      float price;
      if(!float.TryParse(Price.Text.ToString(), out price)) {
        Regex number = new Regex("[^0-9]");
        string after = Price.Text.ToString();
        after = number.Replace(after, "");
        Price.Text = after;
      }
    }
  }
}