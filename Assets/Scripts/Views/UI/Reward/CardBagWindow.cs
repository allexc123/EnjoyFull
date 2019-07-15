using Loxodon.Framework.Binding;
using Loxodon.Framework.Binding.Builder;
using Loxodon.Framework.Contexts;
using Loxodon.Framework.Interactivity;
using Loxodon.Framework.Views;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardBagWindow : Window
{
    public Button openCardBag;
    protected override void OnCreate(IBundle bundle)
    {
        BindingSet<CardBagWindow, CardBagViewModel> bindingSet = this.CreateBindingSet<CardBagWindow, CardBagViewModel>();
        bindingSet.Bind().For(v => v.OnOpenCardBagWindow(null, null)).To(vm => vm.OpenCardBagRequest);

        bindingSet.Bind(this.openCardBag).For(v => v.onClick).To(vm => vm.OpenCardBag).OneWay();

        bindingSet.Build();
    }

    protected void OnOpenCardBagWindow(object sender, InteractionEventArgs args)
    {
        IUIViewLocator viewLocator = Context.GetApplicationContext().GetService<IUIViewLocator>();
        CardDrawWindow cardDrawWindow = viewLocator.LoadWindow<CardDrawWindow>(this.WindowManager, "UI/CardDraw");
        var cardDrawModel = args.Context;
        cardDrawWindow.SetDataContext(cardDrawModel);
        cardDrawWindow.Create();
        cardDrawWindow.Show().OnFinish(()=>{
            this.Dismiss();
        });
    }
}
