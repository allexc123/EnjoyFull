using Loxodon.Framework.Binding;
using Loxodon.Framework.Binding.Builder;
using Loxodon.Framework.Interactivity;
using Loxodon.Framework.Observables;
using Loxodon.Framework.Views;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
using Loxodon.Framework.Contexts;
using Loxodon.Log;
using System;

public class WheelWindow : Window
{
    private static readonly ILog log = LogManager.GetLogger(typeof(WheelWindow));

    public class WheelItemClickedEvent : UnityEvent<int>
    {
        public WheelItemClickedEvent()
        {

        }
    }

    public Transform content;
    public Button drawButton;
    public Image drawImage;

    public GameObject wheel;

    public Image hintImage;

    public GameObject itemTemplate1;
    public GameObject itemTemplate2;


    public WheelItemClickedEvent OnSelectChanged = new WheelItemClickedEvent();

    private ObservableList<WheelItemViewModel> items;

    public ObservableList<WheelItemViewModel> Items
    {
        get { return this.items; }
        set
        {
            if (this.items == value)
                return;

            if (this.items != null)
                this.items.CollectionChanged -= OnCollectionChanged;

            this.items = value;

            this.OnItemsChanged();

            if (this.items != null)
                this.items.CollectionChanged += OnCollectionChanged;
        }
    }
    protected void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs eventArgs)
    {
        switch (eventArgs.Action)
        {
            case NotifyCollectionChangedAction.Add:
                this.AddItem(eventArgs.NewStartingIndex, eventArgs.NewItems[0]);
                break;
            case NotifyCollectionChangedAction.Remove:
                //this.RemoveItem(eventArgs.OldStartingIndex, eventArgs.OldItems[0]);
                break;
            case NotifyCollectionChangedAction.Replace:
                //this.ReplaceItem(eventArgs.OldStartingIndex, eventArgs.OldItems[0], eventArgs.NewItems[0]);
                break;
            case NotifyCollectionChangedAction.Reset:
                //this.ResetItem();
                break;
            case NotifyCollectionChangedAction.Move:
                //this.MoveItem(eventArgs.OldStartingIndex, eventArgs.NewStartingIndex, eventArgs.NewItems[0]);
                break;
        }
    }

    public virtual void AddItem(int index, object item)
    {
        GameObject itemViewGo = null;
        if (index < 6)
        {
            itemViewGo = Instantiate(this.itemTemplate1);
        }
        else
        {
            itemViewGo = Instantiate(this.itemTemplate2);
        }
       
        itemViewGo.transform.SetParent(this.content, false);
        itemViewGo.transform.SetSiblingIndex(index);

        RectTransform rectTransform = itemViewGo.GetComponent<RectTransform>();
        int x = (index / 6) <=0?  -750 : 750;
        int y = 450 - 180 * (index % 6);
        rectTransform.anchoredPosition = new Vector2(x, y);

        Button button = itemViewGo.GetComponent<Button>();
        button.onClick.AddListener(() => OnSelectChange(itemViewGo));

        itemViewGo.SetActive(true);

        UIView itemView = itemViewGo.GetComponent<UIView>();
        itemView.SetDataContext(item);
    }

    protected virtual void OnSelectChange(GameObject itemViewGo)
    {
        if (this.OnSelectChanged == null || itemViewGo == null)
        {
            return;
        }
        for (int i = 0; i < this.content.childCount; i++)
        {
            var child = this.content.GetChild(i);
            if (itemViewGo.transform == child)
            {
                this.OnSelectChanged.Invoke(i);

                break;
            }
        }
    }

    protected virtual void OnItemsChanged()
    {
        for (int i = 0; i < this.items.Count; i++)
        {
            this.AddItem(i, items[i]);
        }
    }


    protected override void OnCreate(IBundle bundle)
    {
       
        BindingSet<WheelWindow, WheelViewModel> bindingSet= this.CreateBindingSet<WheelWindow, WheelViewModel>();

        bindingSet.Bind().For(v => v.Items).To(vm => vm.Items).OneWay();

        bindingSet.Bind(this).For(v => v.OnSelectChanged).To(vm => vm.Select(0)).OneWay();

        bindingSet.Bind(this.drawButton).For(v=>v.onClick).To(vm=>vm.DrawCommand).OneWay();

        bindingSet.Bind(this.drawImage).For(v => v.sprite).To(vm => vm.DrawIcon).WithConversion("wheelConverter").OneWay();

        bindingSet.Bind(this.hintImage).For(v => v.sprite).To(vm => vm.HintIcon).WithConversion("wheelConverter").OneWay();

        bindingSet.Bind().For(v => v.WheelTurn(null, null)).To(vm => vm.WheelTurnRequest);


        bindingSet.Bind().For(v => v.OnOpenCardBagWindow(null, null)).To(vm => vm.CardBagRequest);

        bindingSet.Build();


    }


    protected void WheelTurn(object sender, InteractionEventArgs args)
    {
        var callback = args.Callback;
        int rotNum = (int)args.Context;
        int s = 0;

        //选择的圈数
        int cyclesNum = 5;
        //持续的时间
        int duration = 7; 

        //随机60度的倍数
        s = (cyclesNum * 360 - rotNum * 30) * -1;
        //Debug.Log("本次旋转的度数： " + s + "\r\n" + "本次随机的结果： " + rotNum);
        //本次旋转的度数
        wheel.transform.DORotate(new Vector3(0, 0, s), duration, RotateMode.FastBeyond360).SetEase(Ease.OutQuad).OnComplete(delegate
        {
            if (callback != null)
            {
                callback();
            }
            
        });

        
    }

    protected void OnOpenCardBagWindow(object sender, InteractionEventArgs args)
    {
        try
        {
            IUIViewLocator viewLocator = Context.GetApplicationContext().GetService<IUIViewLocator>();
            CardBagWindow cardBagWindow = viewLocator.LoadWindow<CardBagWindow>(this.WindowManager, "UI/CardBag");
            //var callback = args.Callback;
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
            cardBagWindow.Show();
        }
        catch (Exception e)
        {
            if (log.IsWarnEnabled)
                log.Warn(e);
        }
    }

    private void OnShowPay(object sender, InteractionEventArgs args)
    {
        IUIViewLocator viewLocator = Context.GetApplicationContext().GetService<IUIViewLocator>();
        PayWindow payView = viewLocator.LoadWindow<PayWindow>(this.WindowManager, "UI/Pay");
        payView.Create();
        payView.Show();
    }


}
