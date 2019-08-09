using Loxodon.Framework.Observables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Award: ObservableObject
{
    private string name;
    private int count;
    private int quality;

    public string Name
    {
        get { return this.name; }
        set { Set<string>(ref this.name, value, "Name"); }
    }

    public int Count
    {
        get { return this.count; }
        set { Set<int>(ref this.count, value, "Count"); }
    }

    public int Quality
    {
        get { return this.quality; }
        set { Set<int>(ref this.quality, value, "Quality"); }
    }
}
