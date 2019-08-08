using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Award
{
    private string name;
    private int count;

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
}
