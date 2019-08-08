using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Award
{
    private string name;
    private int count;
    private int quality;

    public string Name
    {
        get { return this.name; }
        set { this.name = value; }
    }

    public int Count
    {
        get { return this.count; }
        set { this.count = value; }
    }

    public int Quality
    {
        get { return this.quality; }
        set { this.quality = value; }
    }
}
