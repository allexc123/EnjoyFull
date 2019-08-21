using System;
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

public class PayViewModel : ViewModelBase
{
    private int countDown = 30;

    private InteractionRequest dismissRequest;

    private ICommand cancelCommand;
    private ICommand confirmCommand;

    protected bool closed;
    protected int result;
    protected Action<int> click;

    public PayViewModel(Action<int> afterHideCallback) : base()
    {
        this.Click = afterHideCallback;

        this.dismissRequest = new InteractionRequest(this);

        cancelCommand = new SimpleCommand(() => {
            this.OnClick(PayDialog.BUTTON_NEGATIVE);

        });
        confirmCommand = new SimpleCommand(() => {
            this.OnClick(PayDialog.BUTTON_POSITIVE);

        });

    }

    public int CountDown
    {
        get { return this.countDown; }
        set { this.Set<int>(ref countDown, value, "CountDown");}
    }

    public ICommand ConfirmCommand
    {
        get { return this.confirmCommand; }
    }

    public ICommand CancelCommand
    {
        get { return this.cancelCommand; }
    }

    public virtual bool Closed
    {
        get { return this.closed; }
        protected set { this.Set<bool>(ref this.closed, value, "Closed"); }
    }

    public virtual Action<int> Click
    {
        get { return this.click; }
        set { this.Set<Action<int>>(ref this.click, value, "Click"); }
    }

    public virtual void OnClick(int which)
    {
        try
        {
            this.result = which;
            var click = this.Click;
            if (click != null)
                click(which);
        }
        catch (Exception) { }
        finally
        {
            this.Closed = true;
            this.dismissRequest.Raise();
        }
    }

    public IInteractionRequest DismissRequest
    {
        get { return this.dismissRequest; }
    }
}
