using iSharing.Models;
using iSharing.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
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
      //清空原列表
      itemViewModel.Items.Clear();
      //获取第一页物品
      getPage(1);
    }
    
    /** 点击显示物品详情
     * 1. 获取物品详情
     * 2. 获取物品图片
     * 3. 将物品信息写入SelectItem
     */
    private async void ListView_ItemClick (object sender, ItemClickEventArgs e) {
      
      itemViewModel.SelectIndex = list.Items.IndexOf (e.ClickedItem);
      string jsonString = "{\"item\":{" + "\"itemid\":" + itemViewModel.SelectItem.Itemid + "}}";
      string result = await Post.PostHttp("/item_getone", jsonString);
      
      if (result != "") {
        JObject data = JObject.Parse(result);
      
        BitmapImage image = await getPic(data["item"][0]["icon"].ToString());
        itemViewModel.SelectItem = new Item(data["item"][0]["itemname"].ToString(), float.Parse(data["item"][0]["price"].ToString()),
                                 data["item"][0]["description"].ToString(), image, data["item"][0]["username"].ToString(), int.Parse(data["item"][0]["itemid"].ToString()));
      
        ItemDetail.Visibility = Visibility.Visible;
        Exit.Visibility = Visibility.Visible;
        ItemList.Visibility = Visibility.Collapsed;
      }
    }
    
    /** 点击显示更多物品
     * 获取下一页
     * 存入本地的Items
     */
    private async void LoadMore_Click(object sender, RoutedEventArgs e) {
      int count = itemViewModel.Items.Count;
      if (count > 0 && count % 20 == 0) {
        await getPage((itemViewModel.Items.Count / 20 + 1));
      }
    }

    /** 获取指定页
     * int page指定的页数
     */
    private async Task<string> getPage(int page) { 
      string jsonString = "{\"pageNumber\":" + page.ToString() + "}";
      string result = await Post.PostHttp("/item_getpage", jsonString);
      if (result != "") {
        JObject data = JObject.Parse(result);
        var items = data["items"];
        foreach (var i in items) { 
          BitmapImage image = await getPic(i["icon"].ToString());
          itemViewModel.Items.Add(new Item(i["username"].ToString(), float.Parse(i["price"].ToString()),
                                       i["description"].ToString(), image, "provider", int.Parse(i["itemid"].ToString())));
        }
        return "success";
      }
      return "error";
    } 
    
    /** 点击关闭物品详情窗口
     */
    private void Exit_Click(object sender, RoutedEventArgs e) {
      ItemDetail.Visibility = Visibility.Collapsed;
      Exit.Visibility = Visibility.Collapsed;
      ItemList.Visibility = Visibility.Visible;
    }
    
    
    /** 将图片的url转为image
     * @param url 图片的url
     */
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