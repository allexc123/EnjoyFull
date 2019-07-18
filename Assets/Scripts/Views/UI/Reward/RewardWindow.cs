using Loxodon.Framework.Binding;
using Loxodon.Framework.Binding.Builder;
using Loxodon.Framework.Interactivity;
using Loxodon.Framework.Observables;
using Loxodon.Framework.Views;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;

public class RewardWindow : Window
{
    private ObservableList<CouponViewModel> coupons;

    public Transform content;

    public GameObject couponTemplate;

    public Image image;

    public InputField phoneNumber;

    public Button receiveReward;

    public Button terminate;

    public Text countDown;

    private AlertDialog alertDialog;

    protected override void OnCreate(IBundle bundle)
    {
        //RewardViewModel rewardViewModel = new RewardViewModel();

        //this.SetDataContext(rewardViewModel);

        BindingSet<RewardWindow, RewardViewModel> bindingSet = this.CreateBindingSet<RewardWindow, RewardViewModel>();

        bindingSet.Bind().For(v => v.Coupons).To(vm => vm.Coupons).OneWay();

        bindingSet.Bind(this.image).For(v => v.sprite).To(vm => vm.Icon).WithConversion("spriteConverter").OneWay();

        bindingSet.Bind(this.phoneNumber).For(v => v.text, v => v.onEndEdit).To(vm => vm.PhoneNumber).TwoWay();

        bindingSet.Bind().For(v => v.OnOpenAlert(null, null)).To(vm => vm.AlertDialogRequest);

        bindingSet.Bind(this.receiveReward).For(v => v.onClick).To(vm => vm.ReceiveReward).OneWay();

        bindingSet.Bind(this.terminate).For(v => v.onClick).To(vm => vm.Terminate).OneWay();

        bindingSet.Bind().For(v => v.OnInteractionFinished(null, null)).To(vm => vm.InteractionFinished);

        bindingSet.Bind(this.countDown).For(v => v.text).ToExpression(vm => string.Format("{0}", vm.CountDown)).TwoWay();

        bindingSet.Build();
    }

    public ObservableList<CouponViewModel> Coupons
    {
        get { return this.coupons; }
        set
        {
            if (this.coupons == value)
                return;

            if (this.coupons != null)
                this.coupons.CollectionChanged -= OnCollectionChanged;

            this.coupons = value;

            this.OnItemsChanged();

            if (this.coupons != null)
                this.coupons.CollectionChanged += OnCollectionChanged;
        }
    }


    protected void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs eventArgs)
    {
        switch (eventArgs.Action)
        {
            case NotifyCollectionChangedAction.Add:
                //this.AddItem(eventArgs.NewStartingIndex, eventArgs.NewItems[0]);
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

    protected virtual void OnItemsChanged()
    {
        for (int i = 0; i < this.coupons.Count; i++)
        {
            this.AddItem(i, coupons[i]);
        }
    }
    protected virtual void AddItem(int index, object item)
    {
        var itemViewGo = Instantiate(this.couponTemplate);
        itemViewGo.transform.SetParent(this.content, true);
        itemViewGo.transform.SetSiblingIndex(index);

        RectTransform rectTransform = itemViewGo.GetComponent<RectTransform>();
        //int y = -250 / coupons.Count * index;
        rectTransform.anchoredPosition = new Vector2(0,  -80 * index);

        //Button button = itemViewGo.GetComponent<Button>();
        //button.onClick.AddListener(() => OnSelectChange(itemViewGo));
        itemViewGo.SetActive(true);

        UIView itemView = itemViewGo.GetComponent<UIView>();
        itemView.SetDataContext(item);
    }

    private void OnOpenAlert(object sender, InteractionEventArgs args)
    {
        DialogNotification notification = args.Context as DialogNotification;
        var callback = args.Callback;

        if (notification == null)
            return;

        alertDialog = AlertDialog.ShowMessage(notification.Message, notification.Title, notification.ConfirmButtonText, null, notification.CancelButtonText, notification.CanceledOnTouchOutside, (result) =>
        {
            notification.DialogResult = result;

            if (callback != null)
                callback();
        });
    }

    public virtual void OnInteractionFinished(object sender, InteractionEventArgs args)
    {
        if (alertDialog != null)
        {
            alertDialog.Cancel();
        }
        this.Dismiss();
    }
}
