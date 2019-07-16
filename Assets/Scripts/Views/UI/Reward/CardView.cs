using Loxodon.Framework.Binding;
using Loxodon.Framework.Binding.Builder;
using Loxodon.Framework.Views;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;
using Loxodon.Framework.Interactivity;

// 需要 EventTrigger 脚本的支援
[RequireComponent(typeof(EventTrigger))]
public class CardView : UIView
{
    public Button button;
    public Image frontImage;
    public Image backImage;

    private bool flag = false;

    protected override void Start()
    {
        BindingSet<CardView, CardViewModel> bindingSet = this.CreateBindingSet<CardView, CardViewModel>();
        bindingSet.Bind(this.frontImage).For(v => v.sprite).To(vm => vm.FrontIcon).WithConversion("spriteConverter").OneWay();
        bindingSet.Bind(this.backImage).For(v => v.sprite).To(vm => vm.BackIcon).WithConversion("spriteConverter").OneWay();

        bindingSet.Bind().For(v => v.OnClick(null, null)).To(vm => vm.ClickedRequest);

        bindingSet.Build();

        backImage.transform.localRotation = Quaternion.Euler(0, 90, 0);

    }

    public void OnClick(object sender, InteractionEventArgs args)
    {
        var callback = args.Callback;
        this.flag = true;
       
        Tweener tweener = frontImage.transform.DOLocalRotate(new Vector3(0, 90, 0), 0.8f);
        tweener.OnComplete(() => {
            backImage.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.8f).OnComplete(()=> {
                frontImage.gameObject.SetActive(false);
                if (callback != null)
                {
                    callback();
                }
            });


        });

    }

    public void OnMouseEnter(BaseEventData pointData)
    {
        if (this.flag) return;

        transform.DOScale(1.2f, 0.5f);
    }
    public void OnMouseExit(BaseEventData pointData)
    {
        if (this.flag) return;

        transform.DOScale(1f, 0.5f);
    }

}
