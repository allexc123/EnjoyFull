using System;
using System.Collections.Specialized;
using Loxodon.Framework.Binding;
using Loxodon.Framework.Binding.Builder;
using Loxodon.Framework.Observables;
using Loxodon.Framework.Views;
using UnityEngine;

public class AwardView : UIView
{

    public Transform content;
    public GameObject awardTemplate;

    private ObservableList<AwardItemViewModel> awards;

    public ObservableList<AwardItemViewModel> Awards
    {
        get { return awards; }
        set
        {
            if (this.awards == value) 
            {
                return;
            }
            if (this.awards != null)
            {
                this.awards.CollectionChanged -= OnCollectionChanged;
            }
            this.awards = value;
            this.OnChanged();

            if (this.awards != null)
            {
                this.awards.CollectionChanged += OnCollectionChanged;
            }
        }
    }

    protected override void Start()
    {
        AwardViewModel awardModel = new AwardViewModel();
        this.SetDataContext(awardModel);

        BindingSet<AwardView, AwardViewModel> bindingSet = this.CreateBindingSet<AwardView, AwardViewModel>();

        bindingSet.Bind(this).For(v => v.Awards).To(vm => vm.Awards).OneWay();

        bindingSet.Build();

        awardModel.LoadAward();

    }


    protected void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs eventArgs)
    {
        switch (eventArgs.Action)
        {
            case NotifyCollectionChangedAction.Add:
                this.AddAward(eventArgs.NewStartingIndex, eventArgs.NewItems[0]);
                break;
            case NotifyCollectionChangedAction.Remove:
                //this.RemoveItem(eventArgs.OldStartingIndex, eventArgs.OldItems[0]);
                break;
            case NotifyCollectionChangedAction.Replace:
                //this.ReplaceItem(eventArgs.OldStartingIndex, eventArgs.OldItems[0], eventArgs.NewItems[0]);
                break;
            case NotifyCollectionChangedAction.Reset:
                this.ResetAward();
                break;
            case NotifyCollectionChangedAction.Move:
                //this.MoveItem(eventArgs.OldStartingIndex, eventArgs.NewStartingIndex, eventArgs.NewItems[0]);
                break;
        }
    }

    protected virtual void OnChanged()
    {
        for (int i = 0; i < this.awards.Count; i++)
        {
            this.AddAward(i, awards[i]);
        }
    }

    protected virtual void AddAward(int index, object item)
    {
        var viewGo = Instantiate(this.awardTemplate);
        viewGo.transform.SetParent(this.content, false);
        viewGo.transform.SetSiblingIndex(index);

        viewGo.SetActive(true);

        UIView itemView = viewGo.GetComponent<UIView>();
        itemView.SetDataContext(item);
    }

    protected virtual void ResetAward()
    {
        for (int i = this.content.childCount - 1; i >= 0; i--)
        {
            Transform transform = this.content.GetChild(i);
            Destroy(transform.gameObject);
        }
    }
}
