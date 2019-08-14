using Loxodon.Framework.Binding;
using Loxodon.Framework.Binding.Builder;
using Loxodon.Framework.Views;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class CountDownView : UIView
{
    public Text countDown;

    private CountDownViewModel viewModel;

    protected override void Start()
    {
        viewModel = new CountDownViewModel();
        this.SetDataContext(viewModel);

        BindingSet<CountDownView, CountDownViewModel> bindingSet = this.CreateBindingSet<CountDownView, CountDownViewModel>();
        bindingSet.Bind(this.countDown).For(v => v.text).ToExpression(vm => string.Format("{0}", vm.CountDown)).OneWay();

        bindingSet.Build();
    }

}
