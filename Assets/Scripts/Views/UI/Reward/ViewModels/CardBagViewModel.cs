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
   
    private InteractionRequest<CardDrawModel> openCardBagRequest;


    private SimpleCommand openCardBag;


    private int countDown = 20;
    private IAsyncResult result;

    public CardBagViewModel() : base()
    {
        

        this.openCardBagRequest = new InteractionRequest<CardDrawModel>(this);
        this.openCardBag = new SimpleCommand(()=> 
        {
            this.openCardBag.Enabled = false;
            CancelTask();
        });

        ApplicationContext context = Context.GetApplicationContext();
        ITask task = context.GetService<ITask>();

        CountDown = 20;
        this.result = task.Scheduled.ScheduleAtFixedRate(() =>
        {
            CountDown--;
            if (countDown <= 0)
            {
               CancelTask();
            }

        }, 1000, 1000);

    }
    private void CancelTask()
    {
        this.result.Cancel();
        CardDrawModel cardDrawModel = new CardDrawModel();
        this.openCardBagRequest.Raise(cardDrawModel);
    }

    public InteractionRequest<CardDrawModel> OpenCardBagRequest
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
