

using Loxodon.Framework.Contexts;
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

    IDisposable disposable;

    public WheelViewModel()
    {
        WheelItemViewModel itemModel1 = new WheelItemViewModel();
        itemModel1.Icon = "shengxiao_shu1";
        WheelItemViewModel itemModel2 = new WheelItemViewModel();
        itemModel2.Icon = "shengxiao_niu1";
        WheelItemViewModel itemModel3 = new WheelItemViewModel();
        itemModel3.Icon = "shengxiao_hu1";
        WheelItemViewModel itemModel4 = new WheelItemViewModel();
        itemModel4.Icon = "shengxiao_tu1";
        WheelItemViewModel itemModel5 = new WheelItemViewModel();
        itemModel5.Icon = "shengxiao_long1";
        WheelItemViewModel itemModel6 = new WheelItemViewModel();
        itemModel6.Icon = "shengxiao_she1";
        WheelItemViewModel itemModel7 = new WheelItemViewModel();
        itemModel7.Icon = "shengxiao_ma1";
        WheelItemViewModel itemModel8 = new WheelItemViewModel();
        itemModel8.Icon = "shengxiao_yang1";
        WheelItemViewModel itemModel9 = new WheelItemViewModel();
        itemModel9.Icon = "shengxiao_hou1";
        WheelItemViewModel itemModel10 = new WheelItemViewModel();
        itemModel10.Icon = "shengxiao_ji1";
        WheelItemViewModel itemModel11 = new WheelItemViewModel();
        itemModel11.Icon = "shengxiao_gou1";
        WheelItemViewModel itemModel12 = new WheelItemViewModel();
        itemModel12.Icon = "shengxiao_zhu1";
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

        WheelItemViewModel item = Items[index];
        item.Command.Execute(null);
    }

    ~WheelViewModel()
    {
        this.disposable.Dispose();
    }

}

