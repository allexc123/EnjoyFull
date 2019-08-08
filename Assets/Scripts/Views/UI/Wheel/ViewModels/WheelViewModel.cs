

using Loxodon.Framework.Commands;
using Loxodon.Framework.Contexts;
using Loxodon.Framework.Interactivity;
using Loxodon.Framework.Observables;
using Loxodon.Framework.ViewModels;
using Loxodon.Log;
using LX;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelViewModel : ViewModelBase
{
    private static readonly ILog log = LogManager.GetLogger(typeof(MessageDispatcher));

    private readonly ObservableList<WheelItemViewModel> items = new ObservableList<WheelItemViewModel>();

    private SimpleCommand drawCommand;

    private string drawIcon;
    private string hintIcon;

    private InteractionRequest<int> wheelTurnRequest;

    private InteractionRequest<CardBagViewModel> cardBagRequest;

    private int wheelIndex = 0;

    //IDisposable disposable;

    public WheelViewModel()
    {
        WheelItemViewModel itemModel1 = new WheelItemViewModel();
        itemModel1.Icon = "shengxiao_shu1";
        itemModel1.Icon1 = "shengxiao_shu0";
        WheelItemViewModel itemModel2 = new WheelItemViewModel();
        itemModel2.Icon = "shengxiao_niu1";
        itemModel2.Icon1 = "shengxiao_niu0";
        WheelItemViewModel itemModel3 = new WheelItemViewModel();
        itemModel3.Icon = "shengxiao_hu1";
        itemModel3.Icon1 = "shengxiao_hu0";
        WheelItemViewModel itemModel4 = new WheelItemViewModel();
        itemModel4.Icon = "shengxiao_tu1";
        itemModel4.Icon1 = "shengxiao_tu0";
        WheelItemViewModel itemModel5 = new WheelItemViewModel();
        itemModel5.Icon = "shengxiao_long1";
        itemModel5.Icon1 = "shengxiao_long0";
        WheelItemViewModel itemModel6 = new WheelItemViewModel();
        itemModel6.Icon = "shengxiao_she1";
        itemModel6.Icon1 = "shengxiao_she0";
        WheelItemViewModel itemModel7 = new WheelItemViewModel();
        itemModel7.Icon = "shengxiao_ma1";
        itemModel7.Icon1 = "shengxiao_ma0";
        WheelItemViewModel itemModel8 = new WheelItemViewModel();
        itemModel8.Icon = "shengxiao_yang1";
        itemModel8.Icon1 = "shengxiao_yang0";
        WheelItemViewModel itemModel9 = new WheelItemViewModel();
        itemModel9.Icon = "shengxiao_hou1";
        itemModel9.Icon1 = "shengxiao_hou0";
        WheelItemViewModel itemModel10 = new WheelItemViewModel();
        itemModel10.Icon = "shengxiao_ji1";
        itemModel10.Icon1 = "shengxiao_ji0";
        WheelItemViewModel itemModel11 = new WheelItemViewModel();
        itemModel11.Icon = "shengxiao_gou1";
        itemModel11.Icon1 = "shengxiao_gou0";
        WheelItemViewModel itemModel12 = new WheelItemViewModel();
        itemModel12.Icon = "shengxiao_zhu1";
        itemModel12.Icon1 = "shengxiao_zhu0";
        items.Add(itemModel1);
        items.Add(itemModel2);
        items.Add(itemModel3);
        items.Add(itemModel4);
        items.Add(itemModel5);
        items.Add(itemModel6);
        items.Add(itemModel7);
        items.Add(itemModel8);
        items.Add(itemModel9);
        items.Add(itemModel10);
        items.Add(itemModel11);
        items.Add(itemModel12);

        wheelTurnRequest = new InteractionRequest<int>();

        this.cardBagRequest = new InteractionRequest<CardBagViewModel>(this);

        this.drawCommand = new SimpleCommand(()=> {
            drawCommand.Enabled = false;
            wheelTurnRequest.Raise(wheelIndex, (int index)=> {
                drawCommand.Enabled = true;

                CardBagViewModel cardBagViewModel = new CardBagViewModel();

                cardBagRequest.Raise(cardBagViewModel);
            });

        });
        drawCommand.Enabled = false;

        this.DrawIcon = "suo";
        this.HintIcon = "tishiyu1";

    }

    public ObservableList<WheelItemViewModel> Items
    {
        get { return this.items; }
    }

    public void AddItem()
    {
        //this.items.Add(new ItemViewModel() { Icon = "aaa" });
        ApplicationContext context = ApplicationContext.GetApplicationContext();
        ISession session = context.GetService<ISession>();
        session.Send(10001, new Login() { DriveId = "default" });

    }

    public void Select(int index)
    {
        if (index <= -1 || index > this.items.Count - 1)
            return;

        wheelIndex = index;
        WheelItemViewModel item = Items[index];
        item.Command.Execute(null);

        for (int i = 0; i < items.Count; i++)
        {
            if (i == index) continue;

            WheelItemViewModel itemViewModel = Items[i];
            itemViewModel.Command.Enabled = false;
            itemViewModel.ChangeIcon();

            this.HintIcon = "tishiyu2";

        }
        this.DrawIcon = "go";
        drawCommand.Enabled = true;
    }

    public ICommand DrawCommand
    {
        get { return this.drawCommand; }
    }

    public string DrawIcon {
        get { return this.drawIcon; }
        set { Set<string>(ref this.drawIcon, value, "DrawIcon"); }
    }
    public string HintIcon
    {
        get { return this.hintIcon; }
        set { Set<string>(ref this.hintIcon, value, "HintIcon"); }
    }

    public InteractionRequest<int> WheelTurnRequest
    {
        get { return this.wheelTurnRequest; }
    }

    public InteractionRequest<CardBagViewModel> CardBagRequest
    {
        get { return this.cardBagRequest; }
    }

    ~WheelViewModel()
    {
        //this.disposable.Dispose();
    }

}

