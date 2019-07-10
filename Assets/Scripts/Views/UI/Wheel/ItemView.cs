using Loxodon.Framework.Binding;
using Loxodon.Framework.Binding.Builder;
using Loxodon.Framework.Views;
using UnityEngine;
using UnityEngine.UI;

public class ItemView : UIView
{
    public Image image;
    protected override void Start()
    {
        BindingSet<ItemView, ItemViewModel> bindingSet = this.CreateBindingSet<ItemView, ItemViewModel>();
        bindingSet.Bind(this.image).For(v => v.sprite).To(vm => vm.Icon).WithConversion("spriteConverter").OneWay();
        bindingSet.Bind();
    }
}
