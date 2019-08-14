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

    public Button drawButton;
    public Image drawImage;

    public GameObject wheel;

    //public Image hintImage;

    public GameObject itemTemplate;

    public GameObject left;
    public GameObject right;

    public Button rule;

    public AwardView awardView;


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
        GameObject itemViewGo = Instantiate(this.itemTemplate);
        //if (index < 6)
        //{
        //    itemViewGo = Instantiate(this.itemTemplate1);
        //}
        //else
        //{
        //    itemViewGo = Instantiate(this.itemTemplate2);
        //}
       
        itemViewGo.transform.SetParent(this.wheel.transform, false);
        itemViewGo.transform.SetSiblingIndex(index);

        RectTransform rectTransform = itemViewGo.GetComponent<RectTransform>();
        float dist = 310f;
        float x = dist * Mathf.Sin(30 * index * Mathf.Deg2Rad);
        float y = dist * Mathf.Cos(30 * index * Mathf.Deg2Rad);
        rectTransform.localPosition = new Vector3(x, y, 0);
        rectTransform.localEulerAngles = new Vector3(0, 0, -30 * index);

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
        for (int i = 0; i < this.wheel.transform.childCount; i++)
        {
            var child = this.wheel.transform.GetChild(i);
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

        //bindingSet.Bind(this.hintImage).For(v => v.sprite).To(vm => vm.HintIcon).WithConversion("wheelConverter").OneWay();

        bindingSet.Bind().For(v => v.WheelTurn(null, null)).To(vm => vm.WheelTurnRequest);


        bindingSet.Bind().For(v => v.OnOpenCardBagWindow(null, null)).To(vm => vm.CardBagRequest);
        //bindingSet.Bind(this.awardView).For(v => v.Awards).To(vm => vm.Awards).OneWay();

        bindingSet.Bind().For(v => v.OnOpenDrawDialog(null, null)).To(vm => vm.DrawDialogRequest);
        bindingSet.Bind().For(v => v.OnDismissRequest(null, null)).To(vm => vm.DismissRequest);

        bindingSet.Build();

        this.rule.onClick.AddListener(RuleAnimation);



        WheelViewModel wheelViewModel = this.GetDataContext() as WheelViewModel;
        this.awardView.BindingContext().DataContext = wheelViewModel.AwardViewModel;


    }
    bool flag = false;
    private void RuleAnimation()
    {
        if (!flag) 
        {
            left.transform.DOLocalMoveX(-400, 2f);
            right.transform.DOLocalMoveX(560, 2f);
            flag = true;
        }
        else {
            left.transform.DOLocalMoveX(0, 2f);
            right.transform.DOLocalMoveX(1260, 2f);
            flag = false;
        }

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
    protected void OnDismissRequest(object sender, InteractionEventArgs args)
    {
        this.Dismiss();
    }

    protected void OnOpenCardBagWindow(object sender, InteractionEventArgs args)
    {
        try
        {
            IUIViewLocator viewLocator = Context.GetApplicationContext().GetService<IUIViewLocator>();
            CardBagWindow cardBagWindow = viewLocator.LoadWindow<CardBagWindow>(this.WindowManager, "UI/CardBag");
            var cardBagModel = args.Context;


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

    private void OnOpenDrawDialog(object sender, InteractionEventArgs args)
    {
        DrawDialogNotification notification = args.Context as DrawDialogNotification;
        var callback = args.Callback;

        DrawDialog.ShowDrawDialog((result) => {
            notification.DialogResult = result;
            if (callback != null)
            {
                callback();
            }
        });
    }

}
