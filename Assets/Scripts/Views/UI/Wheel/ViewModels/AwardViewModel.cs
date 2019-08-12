using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Loxodon.Framework.Asynchronous;
using Loxodon.Framework.Binding;
using Loxodon.Framework.Binding.Builder;
using Loxodon.Framework.Contexts;
using Loxodon.Framework.Observables;
using Loxodon.Framework.ViewModels;
using Loxodon.Framework.Views;
using UnityEngine;

public class AwardViewModel : ViewModelBase
{
    private ObservableList<AwardItemViewModel> awards = new ObservableList<AwardItemViewModel>();

    public void LoadAward()
    {
        Awards.Clear();
        ApplicationContext context = Context.GetApplicationContext();

        IRewardRepository rewardRepository = context.GetService<IRewardRepository>();

        IAsyncResult<List<Award>> result = rewardRepository.GetAwards();
        List<Award> awardList = result.Synchronized().WaitForResult();
        foreach (Award award in awardList)
        {
            AwardItemViewModel awardItemViewModel = new AwardItemViewModel();
            if (award.Quality == (int)QualityType.Orange)
            {
                awardItemViewModel.Name = $"<color=#FF7F00>{award.Name}  {award.Count}</color>";
            }
            else if (award.Quality == (int)QualityType.Purple)
            {
                awardItemViewModel.Name = $"<color=#8B00FF>{award.Name}  {award.Count}</color>";
            }
            else
            {
                awardItemViewModel.Name = $"<color=#00FF00>{award.Name}  {award.Count}</color>";
            }

            this.Awards.Add(awardItemViewModel);
        }

    }
    public ObservableList<AwardItemViewModel> Awards
    {
        get { return this.awards; }
    }

}
