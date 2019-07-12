

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

    private readonly ObservableList<ItemViewModel> items = new ObservableList<ItemViewModel>();

    IDisposable disposable;
    public WheelViewModel()
    {
        ApplicationContext context = ApplicationContext.GetApplicationContext();
        IMessageDispatcher dispatcher = context.GetService<IMessageDispatcher>();
        this.disposable = dispatcher.Subscribe<LoginResult>(20001, message => {
            log.Debug("ABC = " + message.PlayerId);
        });
    }

    public ObservableList<ItemViewModel> Items
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

    ~WheelViewModel()
    {
        this.disposable.Dispose();
    }

}

public class ItemViewModel : ViewModelBase
{
    private string icon;

    public string Icon
    {
        get { return this.icon; }
        set { this.Set<string>(ref icon, value, "Icon"); }
    }
}
