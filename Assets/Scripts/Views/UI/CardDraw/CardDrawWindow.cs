﻿using Loxodon.Framework.Binding;
using Loxodon.Framework.Binding.Builder;
using Loxodon.Framework.Observables;
using Loxodon.Framework.Views;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class CardDrawWindow : Window
{

    public Transform content;
    public GameObject cardTemplate;

    private ObservableList<CardViewModel> cards;

    protected override void OnCreate(IBundle bundle)
    {
        BindingSet<CardDrawWindow, CardDrawViewModel> bindingSet = this.CreateBindingSet<CardDrawWindow, CardDrawViewModel>();
        bindingSet.Bind().For(v => v.Cards).To(vm => vm.Cards).OneWay();

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
        cardViewGo.transform.localPosition = new Vector3(-300 + 300 * x, 300 - 300 * y, 0);


        cardViewGo.SetActive(true);

        UIView cardView = cardViewGo.GetComponent<UIView>();
        cardView.SetDataContext(card);
    }

    
}