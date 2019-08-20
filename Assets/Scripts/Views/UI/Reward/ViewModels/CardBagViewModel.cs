using Loxodon.Framework.Asynchronous;
using Loxodon.Framework.Commands;
using Loxodon.Framework.Contexts;
using Loxodon.Framework.Execution;
using Loxodon.Framework.Interactivity;
using Loxodon.Framework.Observables;
using Loxodon.Framework.ViewModels;
using Loxodon.Log;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CardBagViewModel : ViewModelBase
{
   
    private InteractionRequest<CardTurnModel> openCardBagRequest;


    private SimpleCommand openCardBag;


    private int countDown = 20;

    public CardBagViewModel() : base()
    {
        

        this.openCardBagRequest = new InteractionRequest<CardTurnModel>(this);
        this.openCardBag = new SimpleCommand(()=> 
        {
            this.openCardBag.Enabled = false;
            CardTurnModel cardDrawModel = new CardTurnModel();
            this.openCardBagRequest.Raise(cardDrawModel);
        });

        CountDown = 20;


    }

    public InteractionRequest<CardTurnModel> OpenCardBagRequest
    {
        get { return this.openCardBagRequest; }
    }
    public SimpleCommand OpenCardBag
    {
        get { return this.openCardBag; }
    }

    public int CountDown
    {
        get { return this.countDown; }
        set { this.Set<int>(ref countDown, value, "CountDown"); }
    }

    

   
}
