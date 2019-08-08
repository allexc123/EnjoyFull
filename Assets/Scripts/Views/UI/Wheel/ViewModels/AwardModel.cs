using System;
using Loxodon.Framework.Observables;
using Loxodon.Framework.ViewModels;

public class AwardModel : ViewModelBase
{
    private ObservableList<Award> awards = new ObservableList<Award>();
}
