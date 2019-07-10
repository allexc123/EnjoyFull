using Loxodon.Framework.Binding;
using Loxodon.Framework.Binding.Builder;
using Loxodon.Framework.Observables;
using Loxodon.Framework.Views;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;

public class WheelWindow : Window
{

    public Transform big;
    public Transform middle;
    public Transform small;
    public Button brawButton;
    public GameObject itemTemplate;

    private ObservableList<ItemViewModel> items;

    public ObservableList<ItemViewModel> Items
    {
        get { return this.items; }
        set
        {
            if (this.items == value)
                return;

            if (this.items != null)
                this.items.CollectionChanged -= OnCollectionChanged;

            this.items = value;

            this.OnItemsChanged();

            if (this.items != null)
                this.items.CollectionChanged += OnCollectionChanged;
        }
    }
    protected void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs eventArgs)
    {
        switch (eventArgs.Action)
        {
            case NotifyCollectionChangedAction.Add:
                this.AddItem(eventArgs.NewStartingIndex, eventArgs.NewItems[0]);
                break;
            case NotifyCollectionChangedAction.Remove:
                //this.RemoveItem(eventArgs.OldStartingIndex, eventArgs.OldItems[0]);
                break;
            case NotifyCollectionChangedAction.Replace:
                //this.ReplaceItem(eventArgs.OldStartingIndex, eventArgs.OldItems[0], eventArgs.NewItems[0]);
                break;
            case NotifyCollectionChangedAction.Reset:
                //this.ResetItem();
                break;
            case NotifyCollectionChangedAction.Move:
                //this.MoveItem(eventArgs.OldStartingIndex, eventArgs.NewStartingIndex, eventArgs.NewItems[0]);
                break;
        }
    }

    public virtual void AddItem(int index, object item)
    {
        //float dist = 395f;
        //GameObject itemViewGo = new GameObject(index + "");
        //itemViewGo.layer = LayerMask.NameToLayer("UI");

        //itemViewGo.transform.SetParent(this.big, false);
        //itemViewGo.transform.SetSiblingIndex(index);

        //RectTransform rectTrans = itemViewGo.AddComponent<RectTransform>();
        ////rectTrans.SetParent(this.big.transform);
        //rectTrans.anchoredPosition3D = Vector3.zero;
        //rectTrans.localScale = Vector3.one;
        //rectTrans.anchorMin = new Vector2(0.5f, 0.5f);
        //rectTrans.anchorMax = new Vector2(0.5f, 0.5f);
        //rectTrans.pivot = new Vector2(0.5f, 0.5f);
        //rectTrans.offsetMin = Vector2.zero;
        //rectTrans.offsetMax = Vector2.zero;
        //float x = dist * Mathf.Sin(30 * index * Mathf.Deg2Rad);
        //float y = dist * Mathf.Cos(30 * index * Mathf.Deg2Rad);
        //rectTrans.localPosition = new Vector3(x, y, 0);
        //rectTrans.localEulerAngles = new Vector3(0, 0, -30 * index);

        //itemViewGo.transform.localScale = Vector3.one;

        //itemViewGo.SetActive(true);

        //UIView itemView = itemViewGo.GetComponent<UIView>();
        //itemView.SetDataContext(item);

        var itemViewGo = Instantiate(this.itemTemplate);
        itemViewGo.transform.SetParent(this.big, false);
        itemViewGo.transform.SetSiblingIndex(index);

        itemViewGo.SetActive(true);

        UIView itemView = itemViewGo.GetComponent<UIView>();
        itemView.SetDataContext(item);
    }

    protected virtual void OnItemsChanged()
    {
        for (int i = 0; i < this.items.Count; i++)
        {
            //this.AddItem(i, items[i]);
        }
    }


    protected override void OnCreate(IBundle bundle)
    {
       
        BindingSet<WheelWindow, WheelViewModel> bindingSet= this.CreateBindingSet<WheelWindow, WheelViewModel>();

        bindingSet.Bind().For(v => v.Items).To(vm => vm.Items).OneWay();

        bindingSet.Bind(this.brawButton).For(v=>v.onClick).To(vm=>vm.AddItem());

        bindingSet.Build();




    }

    
}
