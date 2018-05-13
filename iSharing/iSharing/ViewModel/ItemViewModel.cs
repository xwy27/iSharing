using iSharing.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace iSharing.ViewModel
{
    class ItemViewModel : NotifyBase
    {
        // 单例模式
        private static ItemViewModel instance;

        /**
        * 单例模式
        * @return {ItemViewModel} 返回物品单例视图模型
         */
        public static ItemViewModel GetInstance()
        {
            return instance ?? (instance = new ItemViewModel());
        }

        /**
        * 构造函数
        * 测试：使用静态数据构造
        * 上线：使用服务器返回数据构造
        */
        private ItemViewModel()
        {
            try
            {
                this.observableItemCollection.Add(new Item("Test", 6.6f, "testing", new BitmapImage(new Uri("ms-appx:///Assets/photo.jpg")), "testing"));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        //可更新物品数据模型集合
        ObservableCollection<Item> observableItemCollection = new ObservableCollection<Item>();
        public ObservableCollection<Item> Items
        {
            get { return observableItemCollection; }
        }

        //当前物品的index
        int SelectedIndex;
        public int SelectIndex
        {
            get { return SelectedIndex; }
            set
            {
                SelectedIndex = value;
                if (SelectIndex >= 0 && SelectIndex < observableItemCollection.Count)
                {
                    SelectItem = observableItemCollection[SelectedIndex];
                }
                else
                {
                    SelectItem = new Item("", 0, "", null, "");
                }
                OnPropertyChanged();
            }
        }

        //当前物品
        Item SelectedItem;
        public Item SelectItem
        {
            get
            {
                if (SelectedItem != null)
                {
                    return SelectedItem;
                }
                else
                {
                    return new Item("", 0, "", null, "");
                }
            }
            set
            {
                On_Update(value, new PropertyChangedEventArgs(nameof(SelectItem)));
                SelectedItem = value;
                OnPropertyChanged();
            }
        }

        //更新物品
        public void On_Update(Object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "SelectItem")
            {
                
            }
        }
    }
}
