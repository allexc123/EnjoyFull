using Loxodon.Framework.Binding;
using Loxodon.Framework.Binding.Builder;
using Loxodon.Framework.Interactivity;
using Loxodon.Framework.Views;
using Loxodon.Log;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartupWindow : Window
{
    private static readonly ILog log = LogManager.GetLogger(typeof(StartupWindow));

    public Text progressBarText;
    public Slider progressBarSlider;
    public Text tipText;

    private StartupViewModel viewModel;
    private IDisposable subscription;

    protected override void OnCreate(IBundle bundle)
    {
        this.viewModel = new StartupViewModel();

        /* databinding, Bound to the ViewModel. */
        BindingSet<StartupWindow, StartupViewModel> bindingSet = this.CreateBindingSet(viewModel);

        bindingSet.Bind().For(v => v.OnDismissRequest(null, null)).To(vm => vm.DismissRequest);


        bindingSet.Bind(this.progressBarSlider).For("value", "onValueChanged").To("ProgressBar.Progress").TwoWay();
        bindingSet.Bind(this.progressBarSlider.gameObject).For(v=>v.activeSelf).To(vm=>vm.ProgressBar.Enable).OneWay();
        /* expression binding,support only OneWay mode. */
        bindingSet.Bind(this.progressBarText).For(v => v.text).ToExpression(vm=>string.Format("{0}%", Mathf.FloorToInt(vm.ProgressBar.Progress * 100f))).OneWay();
        bindingSet.Bind(this.tipText).For(v => v.text).To(vm => vm.ProgressBar.Tip).OneWay();
        bindingSet.Build();

        this.viewModel.Download();

    }
    public override void DoDismiss()
    {
        base.DoDismiss();
        if (this.subscription != null)
        {
            this.subscription.Dispose();
            this.subscription = null;
        }
    }

    public void OnDismissRequest(object sender, InteractionEventArgs args)
    {
        this.DoDismiss();
    }
}
