using Loxodon.Framework.Binding;
using Loxodon.Framework.Binding.Builder;
using Loxodon.Framework.Views;
using UnityEngine;
using UnityEngine.UI;

public class AwardItemView : UIView
{
    public Text text;


    protected override void Awake()
    {
        BindingSet<AwardItemView, AwardItemViewModel> bindingSet = this.CreateBindingSet<AwardItemView, AwardItemViewModel>();
        bindingSet.Bind(this.text).For(v => v.text).To(vm => vm.Name).OneWay();

        bindingSet.Build();

    }
}
