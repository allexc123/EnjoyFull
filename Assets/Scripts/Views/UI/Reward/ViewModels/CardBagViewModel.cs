using Loxodon.Framework.Commands;
using Loxodon.Framework.Interactivity;
using Loxodon.Framework.Observables;
using Loxodon.Framework.ViewModels;
using Loxodon.Log;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward
{
    //图标
    public string Icon { get; set; }
    //名称
    public string Name { get; set; }
    //描述
    public string Desc { get; set; }
}

public class CardBagViewModel : ViewModelBase
{
    private static readonly ILog log = LogManager.GetLogger(typeof(CardBagViewModel));

    private readonly ObservableList<CardViewModel> cards = new ObservableList<CardViewModel>();

    private InteractionRequest<CardBagViewModel> openCardBagRequest;

    private InteractionRequest<CardBagViewModel> openRewardRequest;

    private SimpleCommand openCardBag;

    private int openCount = 0;
    private int openFinish = 0;

    private List<Reward> rewards = new List<Reward>();

    public CardBagViewModel() : base()
    {
        for (int i = 0; i < 9; i++)
        {
            CardViewModel cardViewModel = new CardViewModel();
            cardViewModel.BackIcon = "a0";
            cardViewModel.FrontIcon = "a1";
            cards.Add(cardViewModel);
        }

        this.openCardBagRequest = new InteractionRequest<CardBagViewModel>(this);
        this.openCardBag = new SimpleCommand(()=> 
        {
            this.openCardBag.Enabled = false;
            this.openCardBagRequest.Raise(this);
        });

        this.openRewardRequest = new InteractionRequest<CardBagViewModel>(this);


        Reward reward1 = new Reward();
        reward1.Icon = "a9";
        reward1.Name = "全聚德";
        reward1.Desc = "全聚德9折优惠券";
        rewards.Add(reward1);

        Reward reward2 = new Reward();
        reward2.Icon = "a4";
        reward2.Name = "肯德基";
        reward2.Desc = "肯德基4折优惠券";
        rewards.Add(reward2);

        Reward reward3 = new Reward();
        reward3.Icon = "a0";
        reward3.Name = "乐享";
        reward3.Desc = "乐享免单券";
        rewards.Add(reward3);

        rewards.Reverse();

        this.openFinish = this.openCount = rewards.Count;

    }

    public ObservableList<CardViewModel> Cards
    {
        get { return this.cards; }
    }

    public InteractionRequest<CardBagViewModel> OpenCardBagRequest
    {
        get { return this.openCardBagRequest; }
    }
    public SimpleCommand OpenCardBag
    {
        get { return this.openCardBag; }
    }

    public InteractionRequest<CardBagViewModel> OpenRewardRequest
    {
        get { return this.openRewardRequest; }
    }

    public void Select(int index)
    {
        if (index <= -1 || index > this.cards.Count - 1)
            return;

        for (int i = 0; i < this.cards.Count; i++)
        {
            if (i == index)
            {
                if (this.openCount <= 0)
                {
                    return;
                }
                log.DebugFormat("Select, Current Index:{0}", index);
                var cardModel = this.cards[i];

                var reward = rewards[openCount - 1];
                cardModel.BackIcon = reward.Icon;
                this.openCount--;

                cardModel.ClickedRequest.Raise(() => {
                    openFinish--;
                    if (this.openFinish <= 0)
                    {
                        this.openRewardRequest.Raise(this);
                    }
                });


                
            }
           
        }
    }
}
