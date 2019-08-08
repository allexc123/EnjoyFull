using System;
using System.Collections.Specialized;
using Loxodon.Framework.Observables;
using Loxodon.Framework.Views;

public class AwardView : UIView
{
    private ObservableList<Award> awards;

    private ObservableList<Award> Awards
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
            this.OnChanged();

            if (this.awards != null)
            {
                this.awards.CollectionChanged += OnCollectionChanged;
            }
        }
    }

    protected override void Start()
    {

    }

    protected void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs eventArgs)
    {
        switch (eventArgs.Action)
        {
            case NotifyCollectionChangedAction.Add:
                //this.AddItem(eventArgs.NewStartingIndex, eventArgs.NewItems[0]);
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

    protected virtual void OnChanged()
    {

    }
}
