using Loxodon.Framework.Asynchronous;
using Loxodon.Framework.Commands;
using Loxodon.Framework.Contexts;
using Loxodon.Framework.Interactivity;
using Loxodon.Framework.Messaging;
using Loxodon.Framework.ViewModels;
using Loxodon.Log;
using LX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class StartupViewModel : ViewModelBase
{
   
    private ProgressBar progressBar = new ProgressBar();
    private SimpleCommand command;

    private InteractionRequest dismissRequest;
    private InteractionRequest idleRequest;
    //private InteractionRequest<CardBagViewModel> wheelRequest;

    public StartupViewModel() : this(null)
    {
    }

    public StartupViewModel(IMessenger messenger) : base(messenger)
    {

        //ApplicationContext context = Context.GetApplicationContext();

        this.dismissRequest = new InteractionRequest(this);
        //this.wheelRequest = new InteractionRequest<CardBagViewModel>(this);

        //var cardDrawViewModel = new CardBagViewModel();

        this.idleRequest = new InteractionRequest(this);

        this.command = new SimpleCommand(()=> {
            this.command.Enabled = false;
            //dismissRequest.Raise();
            //ISession session = context.GetService<ISession>();
            //session.Connect("127.0.0.1", 10001);

            //this.wheelRequest.Raise(cardDrawViewModel);
            //this.dismissRequest.Raise();
            this.idleRequest.Raise();
        });

    }

    public ProgressBar ProgressBar
    {
        get { return this.progressBar; }
    }
    public IInteractionRequest DismissRequest
    {
        get { return this.dismissRequest; }
    }
    public IInteractionRequest IdleRequest
    {
        get { return this.idleRequest; }
    }

    //public InteractionRequest<CardBagViewModel> WheelRequest
    //{
    //    get { return this.wheelRequest; }
    //}


    public void Download()
    {
        ProgressTask<float> task = new ProgressTask<float>(new Action<IProgressPromise<float>>(DoDownLoad));
        task.OnPreExecute(()=> {
            this.command.Enabled = false;
            this.progressBar.Enable = true;

        }).OnProgressUpdate(progress=> {
            this.progressBar.Progress = progress;
        }).OnFinish(()=> {
            this.command.Enabled = true;
            this.progressBar.Enable = false;
            this.command.Execute(null);
        }).Start();
    }

    protected void DoDownLoad(IProgressPromise<float> promise)
    {
        var progress = 0f;
        while (progress < 1f)
        {
            progress += 0.01f;
            promise.UpdateProgress(progress);
#if NETFX_CORE
                Task.Delay(30).Wait();       
#else
            //Thread.Sleep (50);
            Thread.Sleep(30);
#endif
        }
        promise.SetResult();
    }
}
