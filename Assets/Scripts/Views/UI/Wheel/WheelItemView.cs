using Loxodon.Framework.Binding;
using Loxodon.Framework.Binding.Builder;
using Loxodon.Framework.Views;
using UnityEngine;
using UnityEngine.UI;

public class WheelItemView : UIView
{
    public Image image;
    public Button button;
    public GameObject handImage;

    protected override void Start()
    {
        BindingSet<WheelItemView, WheelItemViewModel> bindingSet = this.CreateBindingSet<WheelItemView, WheelItemViewModel>();
        bindingSet.Bind(this.image).For(v => v.sprite).To(vm => vm.NormalIcon).WithConversion("wheelConverter").OneWay();
        bindingSet.Bind(this.handImage).For(v => v.activeSelf).To(vm => vm.ShowHand).TwoWay();

        bindingSet.Bind(this.button).For(v => v.onClick).To(vm => vm.Command).OneWay();

        bindingSet.Build();

    }
}
