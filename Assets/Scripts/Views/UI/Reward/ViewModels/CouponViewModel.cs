using Loxodon.Framework.Commands;
using Loxodon.Framework.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CouponViewModel : ViewModelBase
{
    //图标
    private string icon;
    //名称
    private string name;
    //描述
    private string desc;

    private SimpleCommand viewPic;

    public CouponViewModel(Action<CouponViewModel> callback = null) : base()
    {
        viewPic = new SimpleCommand(() => {
            if (callback != null)
            {
                callback(this);
            }
        });
    }

    //图标
    public string Icon
    {
        get { return this.icon; }
        set { this.Set<string>(ref icon, value, "Icon"); }
    }
    //名称
    public string Name
    {
        get { return this.name; }
        set { this.Set<string>(ref name, value, "Name"); }
    }
    //描述
    public string Desc
    {
        get { return this.desc; }
        set { this.Set<string>(ref desc, value, "Desc"); }
    }

    public SimpleCommand ViewPic
    {
        get { return this.viewPic; }
    }
}