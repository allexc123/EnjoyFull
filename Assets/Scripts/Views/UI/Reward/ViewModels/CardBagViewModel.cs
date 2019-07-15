using Loxodon.Framework.Commands;
using Loxodon.Framework.Interactivity;
using Loxodon.Framework.Observables;
using Loxodon.Framework.ViewModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBagViewModel : ViewModelBase
{
    private readonly ObservableList<CardViewModel> cards = new ObservableList<CardViewModel>();

    private InteractionRequest<CardBagViewModel> openCardBagRequest;

    private SimpleCommand openCardBag;

    public CardBagViewModel() : base()
    {
        for (int i = 0; i < 9; i++)
        {
            CardViewModel cardViewModel = new CardViewModel();
            cardViewModel.BackImage = "a0";
            cardViewModel.FrontImage = "a1";
            cards.Add(cardViewModel);
        }

        this.openCardBagRequest = new InteractionRequest<CardBagViewModel>(this);
        this.openCardBag = new SimpleCommand(()=> 
        {
            this.openCardBag.Enabled = false;
            this.openCardBagRequest.Raise(this);
        });

    }

    public ObservableList<CardViewModel> Cards
    {
        get { return this.cards; }
    }

    public InteractionRequest<CardBagViewModel> OpenCardBagRequest
    {
        get { return this.openCardBagRequest; }
    }
    public SimpleCommand OpenCardBag
    {
        get { return this.openCardBag; }
    }

}
