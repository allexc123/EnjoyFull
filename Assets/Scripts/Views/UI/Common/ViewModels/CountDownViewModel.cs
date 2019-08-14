using Loxodon.Framework.ViewModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownViewModel : ViewModelBase
{

    private int countDown;

    public int CountDown
    {
        get { return this.countDown; }
        set { Set<int>(ref this.countDown, value, "CountDown"); }
    }
}
