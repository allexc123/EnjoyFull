using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Loxodon.Framework.Asynchronous;
using Loxodon.Framework.Binding;
using Loxodon.Framework.Binding.Builder;
using Loxodon.Framework.Commands;
using Loxodon.Framework.Contexts;
using Loxodon.Framework.Interactivity;
using Loxodon.Framework.Observables;
using Loxodon.Framework.ViewModels;
using Loxodon.Framework.Views;
using UnityEngine;

public class DrawDialogViewModel : ViewModelBase
{
    private IRewardRepository rewardRepository;

    private ICommand confirmCommand;

    private ICommand cancelCommand;

    private InteractionRequest dismissRequest;

    private int countDown;
    private int money;
    private int drawCount;

    protected bool closed;
    protected int result;
    protected Action<int> click;

    public DrawDialogViewModel(int countDown, Action<int> afterHideCallback) : base()
    {
        this.countDown = countDown;
        this.Click = afterHideCallback;

        ApplicationContext context = Context.GetApplicationContext();
        rewardRepository = context.GetService<IRewardRepository>();

        this.dismissRequest = new InteractionRequest(this);

        confirmCommand = new SimpleCommand(()=> {
            this.OnClick(DrawDialog.BUTTON_POSITIVE);
        });
        cancelCommand = new SimpleCommand(() => {
            this.OnClick(DrawDialog.BUTTON_NEGATIVE);
        });

        this.DrawCount = rewardRepository.GetDrawCount();
        this.Money = rewardRepository.GetMoney();
    }

    public ICommand ConfirmCommand
    {
        get { return this.confirmCommand; }
    }

    public ICommand CancelCommand
    {
        get { return this.cancelCommand; }
    }

    public int CountDown
    {
        get { return countDown; }
        set { this.Set<int>(ref this.countDown, value, "CountDown"); }
    }
    public int Money
    {
        get { return money; }
        set { this.Set<int>(ref this.money, value, "Pay"); }
    }
    public int DrawCount
    {
        get { return drawCount; }
        set { this.Set<int>(ref this.drawCount, value, "DrawCount"); }
    }

    public virtual Action<int> Click
    {
        get { return this.click; }
        set { this.Set<Action<int>>(ref this.click, value, "Click"); }
    }


    public virtual bool Closed
    {
        get { return this.closed; }
        protected set { this.Set<bool>(ref this.closed, value, "Closed"); }
    }

    public virtual int Result
    {
        get { return this.result; }
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
