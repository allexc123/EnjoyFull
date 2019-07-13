using Loxodon.Framework.Binding;
using Loxodon.Framework.Binding.Builder;
using Loxodon.Framework.Views;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

// 需要 EventTrigger 脚本的支援
[RequireComponent(typeof(EventTrigger))]
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
        backImage.transform.localRotation = Quaternion.Euler(0, 90, 0);
        //backImage.gameObject.SetActive(false);

        //Button btn = this.GetComponent<Button>();
        //EventTrigger trigger = this.GetComponent<EventTrigger>();
        //EventTrigger.Entry entry = new EventTrigger.Entry();
        //// 鼠标点击事件
        //entry.eventID = EventTriggerType.PointerClick;
        //// 鼠标进入事件 
        //entry.eventID = EventTriggerType.PointerEnter;
        //// 鼠标滑出事件 
        //entry.eventID = EventTriggerType.PointerExit;

        //entry.callback = new EventTrigger.TriggerEvent();

        //entry.callback.AddListener(OnClick);

        //trigger.triggers.Add(entry);

    }

    void OnTweenComplete()
    {
        backImage.transform.DOLocalRotate(new Vector3(0, 0, 0), 2);
        frontImage.gameObject.SetActive(false);
        //backImage.gameObject.SetActive(true);
    }

    public void OnClick(BaseEventData pointData)
    {
        //InvokeRepeating("BE", 0.5f, 0.05f);
        //CancelInvoke("BD");
        //transform.DOScale(1.2f, 0f);
        Tweener tweener = frontImage.transform.DOLocalRotate(new Vector3(0, 90, 0), 2);
        tweener.OnComplete(OnTweenComplete);
        //this.transform.DOBlendableRotateBy(new Vector3(0, 180, 0), 2f);
        //frontImage.gameObject.SetActive(false);
        //this.transform.DOBlendableRotateBy(new Vector3(0, 360, 0), 2f);
    }

    public void OnMouseEnter(BaseEventData pointData)
    {
        //Debug.Log("Button Enter. EventTrigger..");
        transform.DOScale(1.2f, 1f);
    }
    public void OnMouseExit(BaseEventData pointData)
    {
        transform.DOScale(1f, 1f);
    }

    private void BE()
    {
        T = Quaternion.Euler(0, 90, 0);
        V = Quaternion.Euler(0, 0, 0);

        frontImage.transform.localRotation = Quaternion.RotateTowards(frontImage.transform.localRotation, T, 4f);
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
