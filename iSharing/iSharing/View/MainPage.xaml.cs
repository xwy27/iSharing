using System.Linq;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace iSharing {
  /// <summary>
  /// 可用于自身或导航至 Frame 内部的空白页。
  /// </summary>
  public sealed partial class MainPage : Page {
    public MainPage() {
      this.InitializeComponent();
    }

    private void InvokeNavigationItem(NavigationView sender, NavigationViewItemInvokedEventArgs args) {
      // find NavigationViewItem with Content that equals InvokedItem
      var item = sender.MenuItems.OfType<NavigationViewItem>().First(x => (string)x.Content == (string)args.InvokedItem);
      NavView_Navigate(item as NavigationViewItem);
    }

    private void NavView_Navigate(NavigationViewItem item) {
      switch (item.Tag) {
        /*case "home":
          ContentFrame.Navigate(typeof(IndexPage));
          break;
        */
        case "items":
          ContentFrame.Navigate(typeof(ViewItem));
          break;

        case "upload":
          ContentFrame.Navigate(typeof(EditItem), "new");
          break;

        case "me":
          ContentFrame.Navigate(typeof(MyInfo));
          break;
      }
    }
  }
}
