using Loxodon.Framework.Binding;
using Loxodon.Framework.Binding.Builder;
using Loxodon.Framework.Observables;
using Loxodon.Framework.Views;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WheelWindow : Window
{
    public class WheelItemClickedEvent : UnityEvent<int>
    {
        public WheelItemClickedEvent()
        {

        }
    }

    public Transform content;

    public Button brawButton;
    public GameObject itemTemplate1;
    public GameObject itemTemplate2;


    public WheelItemClickedEvent OnSelectChanged = new WheelItemClickedEvent();

    private ObservableList<WheelItemViewModel> items;

    public ObservableList<WheelItemViewModel> Items
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
        GameObject itemViewGo = null;
        if (index < 6)
        {
            itemViewGo = Instantiate(this.itemTemplate1);
        }
        else
        {
            itemViewGo = Instantiate(this.itemTemplate2);
        }
       
        itemViewGo.transform.SetParent(this.content, false);
        itemViewGo.transform.SetSiblingIndex(index);

        RectTransform rectTransform = itemViewGo.GetComponent<RectTransform>();
        int x = (index / 6) <=0?  -750 : 750;
        int y = 450 - 180 * (index % 6);
        rectTransform.anchoredPosition = new Vector2(x, y);

        Button button = itemViewGo.GetComponent<Button>();
        button.onClick.AddListener(() => OnSelectChange(itemViewGo));

        itemViewGo.SetActive(true);

        UIView itemView = itemViewGo.GetComponent<UIView>();
        itemView.SetDataContext(item);
    }

    protected virtual void OnSelectChange(GameObject itemViewGo)
    {
        if (this.OnSelectChanged == null || itemViewGo == null)
        {
            return;
        }
        for (int i = 0; i < this.content.childCount; i++)
        {
            var child = this.content.GetChild(i);
            if (itemViewGo.transform == child)
            {
                this.OnSelectChanged.Invoke(i);

                break;
            }
        }
    }

    protected virtual void OnItemsChanged()
    {
        for (int i = 0; i < this.items.Count; i++)
        {
            this.AddItem(i, items[i]);
        }
    }


    protected override void OnCreate(IBundle bundle)
    {
       
        BindingSet<WheelWindow, WheelViewModel> bindingSet= this.CreateBindingSet<WheelWindow, WheelViewModel>();

        bindingSet.Bind().For(v => v.Items).To(vm => vm.Items).OneWay();

        bindingSet.Bind(this).For(v => v.OnSelectChanged).To(vm => vm.Select(0)).OneWay();

        //bindingSet.Bind(this.brawButton).For(v=>v.onClick).To(vm=>vm.AddItem());

        bindingSet.Build();




    }

    
}
