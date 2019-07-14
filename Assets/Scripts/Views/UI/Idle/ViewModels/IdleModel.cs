using System.Collections;
using System.Collections.Generic;
using Loxodon.Framework.Commands;
using Loxodon.Framework.Interactivity;
using Loxodon.Framework.ViewModels;
using UnityEngine;

public class IdleModel : ViewModelBase
{
    private InteractionRequest showPayRequest;

    private SimpleCommand showPay;

    public IdleModel():base()
    {
        this.showPayRequest = new InteractionRequest(this);

        this.showPay = new SimpleCommand(()=> {
            showPayRequest.Raise();
        });
    }

    public InteractionRequest ShowPayRequest
    {
        get { return this.showPayRequest; }
    }
    public ICommand ShowPay 
    {
        get { return this.showPay; }
    }

}
