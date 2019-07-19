using System.Collections;
using System.Collections.Generic;
using Loxodon.Framework.Binding;
using Loxodon.Framework.Binding.Builder;
using Loxodon.Framework.Contexts;
using Loxodon.Framework.Interactivity;
using Loxodon.Framework.Views;
using UnityEngine;
using UnityEngine.UI;

public class IdleWindow : Window
{
    public Button showPay;

    public Image image;
    protected override void OnCreate(IBundle bundle)
    {
        IdleModel idleModel = new IdleModel();
        this.SetDataContext(idleModel);
        BindingSet<IdleWindow, IdleModel> bindingSet = this.CreateBindingSet<IdleWindow, IdleModel>();
        bindingSet.Bind().For(v => v.OnShowPay(null, null)).To(vm => vm.ShowPayRequest);

        bindingSet.Bind(this.showPay).For(v => v.onClick).To(vm => vm.ShowPay);

        bindingSet.Bind(this.image).For(v => v.sprite).To(vm => vm.Icon).WithConversion("merchandiseConverter").TwoWay();

        bindingSet.Build();

        idleModel.Startup();
    }

    private void OnShowPay(object sender, InteractionEventArgs args)
    {
        IUIViewLocator viewLocator = Context.GetApplicationContext().GetService<IUIViewLocator>();
        PayWindow payView = viewLocator.LoadWindow<PayWindow>(this.WindowManager, "UI/Pay");
        payView.Create();
        payView.Show();
    }
}
