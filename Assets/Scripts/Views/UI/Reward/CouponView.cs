using Loxodon.Framework.Binding;
using Loxodon.Framework.Binding.Builder;
using Loxodon.Framework.Views;
using UnityEngine.UI;

public class CouponView : UIView
{ 

    public Text title;
    public Button viewPic;

    protected override void Start()
    {
        BindingSet<CouponView, CouponViewModel> bindingSet = this.CreateBindingSet<CouponView, CouponViewModel>();
        bindingSet.Bind(this.title).For(v => v.text).To(vm => vm.Name).OneWay();

        bindingSet.Bind(this.viewPic).For(v => v.onClick).To(vm => vm.ViewPic).OneWay();

        bindingSet.Build();
    }
}