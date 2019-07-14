using System.Collections;
using System.Collections.Generic;
using Loxodon.Framework.Binding;
using Loxodon.Framework.Binding.Builder;
using Loxodon.Framework.Interactivity;
using Loxodon.Framework.Views;
using Loxodon.Log;
using UnityEngine;
using UnityEngine.UI;

public class PayWindow : Window
{
    private static readonly ILog log = LogManager.GetLogger(typeof(PayWindow));
    public Text countDown;
    protected override void OnCreate(IBundle bundle)
    {
        PayModel payModel = new PayModel();
        this.SetDataContext(payModel);

        BindingSet<PayWindow, PayModel> bindingSet = this.CreateBindingSet<PayWindow, PayModel>();

        bindingSet.Bind().For(v => v.OnDismissRequest(null, null)).To(vm => vm.DismissRequest); ;

        bindingSet.Bind(this.countDown).For(v => v.text).ToExpression(vm => string.Format("{0}", vm.CountDown)).TwoWay();

        bindingSet.Build();
    }

    public override void DoDismiss()
    {
        base.DoDismiss();

        //this.OnDestroy();
    }

    public void OnDismissRequest(object sender, InteractionEventArgs args)
    {
        log.Debug("Close pay");
        this.DoDismiss();

    }
}
