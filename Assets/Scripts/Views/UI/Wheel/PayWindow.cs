using System;
using System.Collections;
using System.Collections.Generic;
using Loxodon.Framework.Binding;
using Loxodon.Framework.Binding.Builder;
using Loxodon.Framework.Contexts;
using Loxodon.Framework.Interactivity;
using Loxodon.Framework.Views;
using Loxodon.Log;
using UnityEngine;
using UnityEngine.UI;

public class PayWindow : Window
{

    private static readonly ILog log = LogManager.GetLogger(typeof(PayWindow));

    public CountDownView countDownView;
    public Button mask;

    protected override void OnCreate(IBundle bundle)
    {
        BindingSet<PayWindow, PayViewModel> bindingSet = this.CreateBindingSet<PayWindow, PayViewModel>();
       
        bindingSet.Bind(this.mask).For(v => v.onClick).To(vm => vm.ConfirmCommand).OneWay();

        bindingSet.Bind(this.countDownView).For(v => v.OnFinish).To(vm => vm.CancelCommand).OneWay();
        bindingSet.Bind(this.countDownView).For(v => v.CountDown).To(vm => vm.CountDown).OneWay();

        bindingSet.Bind().For(v => v.OnDismissRequest(null, null)).To(vm => vm.DismissRequest);

        bindingSet.Build();
    }



    protected void OnDismissRequest(object sender, InteractionEventArgs args)
    {
        this.Dismiss();
    }



}
