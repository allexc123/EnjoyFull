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

    public Text countDown;
    public Button mask;

    protected override void OnCreate(IBundle bundle)
    {
        PayModel payModel = new PayModel();
        this.SetDataContext(payModel);

        BindingSet<PayWindow, PayModel> bindingSet = this.CreateBindingSet<PayWindow, PayModel>();

        //bindingSet.Bind().For(v => v.OnDismissRequest(null, null)).To(vm => vm.DismissRequest);
        bindingSet.Bind().For(v => v.OnOpenCardBagWindow(null, null)).To(vm => vm.CardBagRequest);

        bindingSet.Bind(this.countDown).For(v => v.text).ToExpression(vm => string.Format("{0}", vm.CountDown)).TwoWay();
        bindingSet.Bind(this.mask).For(v => v.onClick).To(vm => vm.Click).OneWay();

        bindingSet.Build();
    }

   

    //public void OnDismissRequest(object sender, InteractionEventArgs args)
    //{
    //    log.Debug("Close pay");
    //    this.Dismiss();

    //}

    protected void OnOpenCardBagWindow(object sender, InteractionEventArgs args)
    {
        try
        {
            IUIViewLocator viewLocator = Context.GetApplicationContext().GetService<IUIViewLocator>();
            CardBagWindow cardBagWindow = viewLocator.LoadWindow<CardBagWindow>(this.WindowManager, "UI/CardBag");
            var callback = args.Callback;
            var cardBagModel = args.Context;

            //if (callback != null)
            //{
            //    EventHandler handler = null;
            //    handler = (window, e) =>
            //    {
            //        cardBagWindow.OnDismissed -= handler;
            //        if (callback != null)
            //            callback();
            //    };
            //    cardBagWindow.OnDismissed += handler;
            //}

            cardBagWindow.SetDataContext(cardBagModel);
            cardBagWindow.Create();
            cardBagWindow.Show().OnFinish(()=> {
                if (callback != null)
                {
                    callback();
                }
            });
        }
        catch (Exception e)
        {
            if (log.IsWarnEnabled)
                log.Warn(e);
        }
    }
}
