using System.Collections;
using System.Collections.Generic;
using Loxodon.Framework.Asynchronous;
using Loxodon.Framework.Execution;
using Loxodon.Framework.Interactivity;
using Loxodon.Framework.ViewModels;
using Loxodon.Log;
using UnityEngine;

public class PayModel : ViewModelBase
{
    private static readonly ILog log = LogManager.GetLogger(typeof(PayModel));

    private int countDown = 30;

    private InteractionRequest dismissRequest;

    private ThreadScheduledExecutor scheduled;

    public PayModel() : base()
    {

        this.dismissRequest = new InteractionRequest(this);

        scheduled = new ThreadScheduledExecutor();
        scheduled.Start();

        IAsyncResult result = scheduled.ScheduleAtFixedRate(() =>
        {
            CountDown -= 1;
            if (countDown <= 0)
            {
                scheduled.Stop();
                scheduled.Dispose();
                scheduled = null;
                this.dismissRequest.Raise();
            }
           
        }, 1000, 1000);
    }

    public int CountDown
    {
        get { return this.countDown; }
        set { this.Set<int>(ref countDown, value, "CountDown");}
    }

    public IInteractionRequest DismissRequest
    {
        get { return this.dismissRequest; }
    }
}
