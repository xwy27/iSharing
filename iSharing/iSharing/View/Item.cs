using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace iSharing.Models {
  /**
   * 属性更新基类
   * 所有需要提醒更新的类都继承
   */
  public class NotifyBase : INotifyPropertyChanged {
    public event PropertyChangedEventHandler PropertyChanged;

    public void OnPropertyChanged([CallerMemberName] string propertyName = null) {
      if (PropertyChanged != null)
        this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }
  }

  class Item : NotifyBase {
    //物品名称
    private string itemname;

    //物品租赁价格
    private float price;

    //物品描述
    private string description;

    //物品图片
    private BitmapImage picture;

    //提供者ID?姓名?联系方式?
    private string provider;

    //物品名称接口
    public string Itemname {
      get { return this.itemname; }
      set {
        this.itemname = value;
        this.OnPropertyChanged();
      }
    }

    //物品租赁价格接口
    public float Price {
      get { return this.price; }
      set {
        this.price = value;
        this.OnPropertyChanged();
      }
    }

    //物品描述接口
    public string Description {
      get { return this.description; }
      set {
        this.description = value;
        this.OnPropertyChanged();
      }
    }

    //物品图片接口
    public BitmapImage Picture {
      get { return this.picture; }
      set {
        this.picture = value;
        this.OnPropertyChanged();
      }
    }

    //提供者ID?姓名?联系方式?
    public string Provider {
      get { return this.provider; }
      set {
        this.provider = value;
        this.OnPropertyChanged();
      }
    }

    /**
    * 构造函数，根据给定数据构造用户
    * @param {string} password 密码
    * @param {string} mail 邮箱
    * @param {string} phone 电话
    * @param {BitmapImage} photo 头像
    * @param {string} wechat 微信号，可为空
    * @param {string} qq qq号，可为空
    */
    public Item(string itemname, float price, string description, BitmapImage picture,
        string provider) {
      try {
        this.itemname = itemname;
        this.price = price;
        this.description = description;
        this.picture = picture;
        this.provider = provider;
      }
      catch (Exception ex) {
        Debug.WriteLine(ex.Message + ex.StackTrace);
      }
    }
  }
}
