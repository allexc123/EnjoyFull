using System;
using System.Collections.Generic;
using Loxodon.Framework.Asynchronous;
using Loxodon.Framework.Contexts;
using Loxodon.Framework.Observables;
using Loxodon.Framework.ViewModels;

public class AwardViewModel : ViewModelBase
{
    private ObservableList<AwardItemViewModel> awards = new ObservableList<AwardItemViewModel>();

    public AwardViewModel(): base()
    {
        
    }

    public ObservableList<AwardItemViewModel> Awards
    {
        get { return this.awards; }
    }

    public void LoadAward()
    {
        Awards.Clear();
        ApplicationContext context = Context.GetApplicationContext();

        IRewardRepository rewardRepository = context.GetService<IRewardRepository>();

        IAsyncResult<List<Award>> result = rewardRepository.GetAwards();
        List<Award>  awardList = result.Synchronized().WaitForResult();
        foreach (Award award in awardList)
        {
            AwardItemViewModel awardItemViewModel = new AwardItemViewModel();
            if (award.Quality == 5)
            {
                awardItemViewModel.Name = $"<color=#FF7F00>{award.Name}  {award.Count}</color>";
            }else if (award.Quality == 4)
            {
                awardItemViewModel.Name = $"<color=#8B00FF>{award.Name}  {award.Count}</color>";
            }
            else
            {
                awardItemViewModel.Name = $"<color=#00FF00>{award.Name}  {award.Count}</color>";
            }
            
            this.awards.Add(awardItemViewModel);
        }

    }
}
