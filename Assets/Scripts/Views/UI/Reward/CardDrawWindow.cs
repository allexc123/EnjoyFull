using Loxodon.Framework.Binding;
using Loxodon.Framework.Binding.Builder;
using Loxodon.Framework.Contexts;
using Loxodon.Framework.Interactivity;
using Loxodon.Framework.Observables;
using Loxodon.Framework.Views;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CardDrawWindow : Window
{

    public class CardClickedEvent : UnityEvent<int>
    {
        public CardClickedEvent()
        {

        }
    }

    public Transform content;
    public GameObject cardTemplate;
    public Text countDown;
    public Text openCount;

    private ObservableList<CardViewModel> cards;

    public CardClickedEvent OnSelectChanged = new CardClickedEvent();

    
    protected override void OnCreate(IBundle bundle)
    {
        BindingSet<CardDrawWindow, CardDrawModel> bindingSet = this.CreateBindingSet<CardDrawWindow, CardDrawModel>();
        bindingSet.Bind().For(v => v.Cards).To(vm => vm.Cards).OneWay();
        bindingSet.Bind().For(v => v.OnSelectChanged).To(vm => vm.Select(0)).OneWay();
        bindingSet.Bind().For(v => v.OnOpenRewardWindow(null, null)).To(vm => vm.OpenRewardRequest);

        bindingSet.Bind(this.countDown).For(v => v.text).ToExpression(vm => string.Format("{0}", vm.CountDown)).OneWay();
        bindingSet.Bind(this.openCount).For(v => v.text).ToExpression(vm => string.Format("{0}", vm.OpenCount)).OneWay();

        bindingSet.Build();
    }

    public ObservableList<CardViewModel> Cards
    {
        get { return this.cards; }
        set
        {
            if (this.cards == value)
            {
                return;
            }
            if (this.cards != null)
            {
                this.cards.CollectionChanged -= OnCollectionChanged;
            }
            this.cards = value;

            OnCardChanged();

            if (this.cards  != null)
            {
                this.cards.CollectionChanged += OnCollectionChanged;
            }
        }
    }
    protected void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs eventArgs)
    {
        switch (eventArgs.Action)
        {
            case NotifyCollectionChangedAction.Add:
                this.AddCard(eventArgs.NewStartingIndex, eventArgs.NewItems[0]);
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

    protected virtual void OnCardChanged()
    {
        for (int i = 0; i < this.cards.Count; i++)
        {
            this.AddCard(i, cards[i]);
        }
    }

    protected virtual void AddCard(int index, object card)
    {
        var cardViewGo = Instantiate(this.cardTemplate);
        cardViewGo.transform.SetParent(this.content, false);
        cardViewGo.transform.SetSiblingIndex(index);
       
        int x = index % 3;
        int y = index / 3;
        cardViewGo.transform.localPosition = new Vector3(-410 + 410 * x, 360 - 325 * y, 0);

        Button button = cardViewGo.GetComponent<Button>();
        button.onClick.AddListener(() => OnSelectChange(cardViewGo));

        cardViewGo.SetActive(true);

        CardView cardView = cardViewGo.GetComponent<CardView>();
        cardView.SetDataContext(card);
       
    }

    protected virtual void OnSelectChange(GameObject cardViewGo)
    {
        if (this.OnSelectChanged == null || cardViewGo == null)
        {
            return;
        }
        for (int i = 0; i < this.content.childCount; i++)
        {
            var child = this.content.GetChild(i);
            if (cardViewGo.transform == child)
            {
                this.OnSelectChanged.Invoke(i);
                break;
            }
        }
    }

    protected void OnOpenRewardWindow(object sender, InteractionEventArgs args)
    {
        var cardBagViewModel = args.Context;
        IUIViewLocator viewLocator = Context.GetApplicationContext().GetService<IUIViewLocator>();
        RewardWindow rewardWindow = viewLocator.LoadWindow<RewardWindow>(this.WindowManager, "UI/Reward");

        rewardWindow.SetDataContext(cardBagViewModel);
        rewardWindow.Create();
        rewardWindow.Show();
    }

}
