using Loxodon.Framework.Observables;
using Loxodon.Framework.ViewModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBagViewModel : ViewModelBase
{
    private readonly ObservableList<CardViewModel> cards = new ObservableList<CardViewModel>();

    public CardBagViewModel() : base()
    {
        for (int i = 0; i < 9; i++)
        {
            CardViewModel cardViewModel = new CardViewModel();
            cardViewModel.BackImage = "a0";
            cardViewModel.FrontImage = "a1";
            cards.Add(cardViewModel);
        }
    }

    public ObservableList<CardViewModel> Cards
    {
        get { return this.cards; }
    }
}
