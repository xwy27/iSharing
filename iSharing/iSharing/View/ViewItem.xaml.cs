using iSharing.ViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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

    private void ListView_ItemClick (object sender, ItemClickEventArgs e) {
      ItemDetail.Visibility = Visibility.Visible;
      ItemList.Visibility = Visibility.Collapsed;
      itemViewModel.SelectIndex = list.Items.IndexOf (e.ClickedItem);
      itemViewModel.SelectItem = itemViewModel.Items[itemViewModel.SelectIndex];
    }
  }
}