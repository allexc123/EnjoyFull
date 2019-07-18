using DG.Tweening;
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
    public Text countDown;
    protected override void OnCreate(IBundle bundle)
    {
        BindingSet<CardBagWindow, CardBagViewModel> bindingSet = this.CreateBindingSet<CardBagWindow, CardBagViewModel>();
        bindingSet.Bind().For(v => v.OnOpenCardBagWindow(null, null)).To(vm => vm.OpenCardBagRequest);

        bindingSet.Bind(this.openCardBag).For(v => v.onClick).To(vm => vm.OpenCardBag).OneWay();

        bindingSet.Bind(this.countDown).For(v => v.text).ToExpression(vm => string.Format("{0}", vm.CountDown)).OneWay();

        bindingSet.Build();

        //第一个参数 ,抖动的方向,例如我这里是从当前位置向上抖动到1的位置
        //第二个参数,抖动的时间
        //第三个参数,抖动的次数
        //第四个参数 0-1之间的数,代表回弹的幅度,
        //假如为零从最高点当前位置 + Vector3.up回来的时候只会回到当前位置,如果为一就会回到当前位置 - Vector3.up的位置

        openCardBag.transform.DOShakePosition(1f, new Vector3(10, 10, 10), 10, 180, false).SetLoops(-1, LoopType.Incremental);
    }

    protected void OnOpenCardBagWindow(object sender, InteractionEventArgs args)
    {
        IUIViewLocator viewLocator = Context.GetApplicationContext().GetService<IUIViewLocator>();
        CardDrawWindow cardDrawWindow = viewLocator.LoadWindow<CardDrawWindow>(this.WindowManager, "UI/CardDraw");
        var cardDrawModel = args.Context;
        cardDrawWindow.SetDataContext(cardDrawModel);
        cardDrawWindow.Create();
        cardDrawWindow.Show();
    }
}
