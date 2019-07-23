using System.Collections;
using System.Collections.Generic;
using Loxodon.Framework.Asynchronous;
using Loxodon.Framework.Commands;
using Loxodon.Framework.Contexts;
using Loxodon.Framework.Execution;
using Loxodon.Framework.Interactivity;
using Loxodon.Framework.ViewModels;
using Loxodon.Log;
using UnityEngine;

public class PayModel : ViewModelBase
{
    private static readonly ILog log = LogManager.GetLogger(typeof(PayModel));

    private int countDown = 30;

    //private InteractionRequest dismissRequest;

    private InteractionRequest<CardBagViewModel> cardBagRequest;

    private SimpleCommand showCardBag;

    private IAsyncResult result;

    public PayModel() : base()
    {

        //this.dismissRequest = new InteractionRequest(this);

        this.cardBagRequest = new InteractionRequest<CardBagViewModel>(this);

        

        ApplicationContext context = Context.GetApplicationContext();
        ITask task = context.GetService<ITask>();

        result = task.Scheduled.ScheduleAtFixedRate(() =>
        {
            CountDown -= 1;
            if (countDown <= 0)
            {
                ClosePay();

            }

        }, 1000, 1000);

       

        this.showCardBag = new SimpleCommand(()=>
        {
            this.showCardBag.Enabled = false;
          
            ClosePay();

        });

    }
    private void ClosePay()
    {
        this.result.Cancel();
        var cardBagModel = new CardBagViewModel();
        this.cardBagRequest.Raise(cardBagModel);
        //dismissRequest.Raise();


    }

    public int CountDown
    {
        get { return this.countDown; }
        set { this.Set<int>(ref countDown, value, "CountDown");}
    }

    //public IInteractionRequest DismissRequest
    //{
    //    get { return this.dismissRequest; }
    //}

    public InteractionRequest<CardBagViewModel> CardBagRequest
    {
        get { return this.cardBagRequest; }
    }

    public ICommand Click
    {
        get { return this.showCardBag; }
    }
}
