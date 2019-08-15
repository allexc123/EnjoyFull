using Loxodon.Framework.Binding;
using Loxodon.Framework.Binding.Builder;
using Loxodon.Framework.Views;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class CountDownView : MonoBehaviour
{
    public class CoundDownFinish : UnityEvent
    {
        public CoundDownFinish()
        {

        }
    }
    public CoundDownFinish OnFinish = new CoundDownFinish();

    public Text text;

    private float countDown = 0;

    private CountDownViewModel viewModel;
    private bool isStart = false;

    //protected override void Start()
    //{
    //    viewModel = new CountDownViewModel();
    //    this.SetDataContext(viewModel);

    //    BindingSet<CountDownView, CountDownViewModel> bindingSet = this.CreateBindingSet<CountDownView, CountDownViewModel>();
    //    bindingSet.Bind(this.countDown).For(v => v.text).ToExpression(vm => string.Format("{0}", vm.CountDown)).OneWay();

    //    bindingSet.Build();
    //}

    public void Update()
    {
        if (!isStart)
        {
            return;
        }
        if (countDown <= 0)
        {
            isStart = false;
            OnFinish.Invoke();
        }
        countDown = countDown - Time.deltaTime;
        text.text = string.Format("{0}", (int)countDown);
    }

    public float CountDown
    {
        get { return this.countDown; }
        set { this.countDown = value; isStart = true; }
    }

}
