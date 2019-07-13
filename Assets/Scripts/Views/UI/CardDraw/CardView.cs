using Loxodon.Framework.Binding;
using Loxodon.Framework.Binding.Builder;
using Loxodon.Framework.Views;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardView : UIView
{
    private Quaternion T;
    private Quaternion V;

    public Image frontImage;
    public Image backImage;

    protected override void Start()
    {
        BindingSet<CardView, CardViewModel> bindingSet = this.CreateBindingSet<CardView, CardViewModel>();
        bindingSet.Bind(this.frontImage).For(v => v.sprite).To(vm => vm.FrontImage).OneWay();
        bindingSet.Bind(this.backImage).For(v => v.sprite).To(vm => vm.BackImage).OneWay();

        bindingSet.Build();

        //frontImage.transform.rotation = Quaternion.Euler(0, 0, 0);
        backImage.transform.rotation = Quaternion.Euler(0, 90, 0);

    }
    public void OnClick()
    {
        InvokeRepeating("BE", 0.5f, 0.05f);
        CancelInvoke("BD");
    }

    private void BE()
    {
        T = Quaternion.Euler(0, 90, 0);
        V = Quaternion.Euler(0, 0, 0);

        frontImage.transform.rotation = Quaternion.RotateTowards(frontImage.transform.rotation, T, 4f);
        if (frontImage.transform.eulerAngles.y > 89 && frontImage.transform.eulerAngles.y < 91)
        {

            backImage.transform.rotation = Quaternion.RotateTowards(backImage.transform.rotation, V, 4f);


        }
    }
    private void BD()
    {

        //返回动作
        // C.transform.GetComponent<Button>().enabled = false;
        T = Quaternion.Euler(0, 90, 0);
        V = Quaternion.Euler(0, 0, 0);
        backImage.transform.rotation = Quaternion.RotateTowards(backImage.transform.rotation, T, 4f);
        if (backImage.transform.eulerAngles.y > 89 && backImage.transform.eulerAngles.y < 91)
        {//加这个判断是因为写的是90度，但是实际不可能直接是90度整，所以加了一个这样的判断

            frontImage.transform.rotation = Quaternion.RotateTowards(frontImage.transform.rotation, V, 4f);

          
            //C.transform.GetComponent<Button>().enabled = true;
        }

    }
}
