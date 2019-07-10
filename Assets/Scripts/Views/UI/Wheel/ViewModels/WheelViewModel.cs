using Loxodon.Framework.Observables;
using Loxodon.Framework.ViewModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelViewModel : ViewModelBase
{
    private readonly ObservableList<ItemViewModel> items = new ObservableList<ItemViewModel>();

    public ObservableList<ItemViewModel> Items
    {
        get { return this.items; }
    }

    public void AddItem()
    {
        this.items.Add(new ItemViewModel() { Icon = "aaa" });
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
