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

public class IdleWindow : Window
{

    private static readonly ILog log = LogManager.GetLogger(typeof(IdleWindow));

    public Button showWheel;

    public Image image;
    protected override void OnCreate(IBundle bundle)
    {
        IdleModel idleModel = new IdleModel();
        this.SetDataContext(idleModel);
        BindingSet<IdleWindow, IdleModel> bindingSet = this.CreateBindingSet<IdleWindow, IdleModel>();
        bindingSet.Bind().For(v => v.OnOpenWheelWindow).To(vm => vm.ShowWheelRequest);

        bindingSet.Bind(this.showWheel).For(v => v.onClick).To(vm => vm.ShowWheel);

        bindingSet.Bind(this.image).For(v => v.sprite).To(vm => vm.Icon).WithConversion("merchandiseConverter").TwoWay();

        bindingSet.Build();

        idleModel.Startup();
    }

    protected void OnOpenWheelWindow(object sender, InteractionEventArgs args)
    {
        try
        {
            IUIViewLocator viewLocator = Context.GetApplicationContext().GetService<IUIViewLocator>();
            WheelWindow window = viewLocator.LoadWindow<WheelWindow>(this.WindowManager, "UI/Wheel");
            //var callback = args.Callback;
            var model = args.Context;

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

            window.SetDataContext(model);
            window.Create();
            window.Show();
        }
        catch (Exception e)
        {
            if (log.IsWarnEnabled)
                log.Warn(e);
        }
    }
}
