using Loxodon.Framework.Observables;
using Loxodon.Framework.ViewModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDrawViewModel : ViewModelBase
{
    private readonly ObservableList<CardViewModel> cards = new ObservableList<CardViewModel>();

    public CardDrawViewModel() : base()
    {
        for (int i = 0; i < 9; i++)
        {
            cards.Add(new CardViewModel());
        }
    }

    public ObservableList<CardViewModel> Cards
    {
        get { return this.cards; }
    }
}
