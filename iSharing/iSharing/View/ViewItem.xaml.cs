using iSharing.Models;
using iSharing.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Web.Http;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace iSharing {
  /// <summary>
  /// 可用于自身或导航至 Frame 内部的空白页。
  /// </summary>
  public sealed partial class ViewItem : Page {
    private ItemViewModel itemViewModel = ItemViewModel.GetInstance ();

    public ViewItem () {
      this.InitializeComponent ();
    }

    private async void ListView_ItemClick (object sender, ItemClickEventArgs e) {
      
      string jsonString = "{\"item\":{" + "\"itemid\":" + itemViewModel.SelectItem.Itemid + "}}";
      string result = await Post.PostHttp("/item_getone", jsonString);

      JObject data = JObject.Parse(result);
      
      BitmapImage image = await getPic(data["item"]["icon"].ToString());
      itemViewModel.SelectItem = new Item(data["item"]["itemname"].ToString(), float.Parse(data["item"]["price"].ToString()),
                                 data["item"]["description"].ToString(), image, data["item"]["username"].ToString(), int.Parse(data["item"]["itemid"].ToString()));
      
      ItemDetail.Visibility = Visibility.Visible;
      Exit.Visibility = Visibility.Visible;
      ItemList.Visibility = Visibility.Collapsed;
      itemViewModel.SelectIndex = list.Items.IndexOf (e.ClickedItem);
      
    }

    private async void LoadMore_Click(object sender, RoutedEventArgs e) {
      string jsonString = "{\"pageNumber\":" + (itemViewModel.Items.Count / 20 + 1).ToString() + "}";
      string result = await Post.PostHttp("/item_getpage", jsonString);
      
    }

    private void Exit_Click(object sender, RoutedEventArgs e) {
      ItemDetail.Visibility = Visibility.Collapsed;
      Exit.Visibility = Visibility.Collapsed;
      ItemList.Visibility = Visibility.Visible;
    }
    
    
    private async Task<BitmapImage> getPic(string url) {
      HttpClient http = new HttpClient();
      BitmapImage image = new BitmapImage();
      if (url != "") {
        var buffer = await http.GetBufferAsync(new Uri("http://" + url));
        using (IRandomAccessStream stream = new InMemoryRandomAccessStream()) { 
          await stream.WriteAsync(buffer);
          stream.Seek(0);
          await image.SetSourceAsync(stream);
        }
      }
      return image;
    }
  }
}