using Loxodon.Framework.Asynchronous;
using Loxodon.Framework.Contexts;
using Loxodon.Framework.Execution;
using Loxodon.Framework.Interactivity;
using Loxodon.Framework.Observables;
using Loxodon.Framework.ViewModels;
using Loxodon.Log;
using System.Collections.Generic;


public class Reward
{
    //图标
    public string Icon { get; set; }
    //名称
    public string Name { get; set; }
    //描述
    public string Desc { get; set; }
    //表述图标
    public string DescIcon { get; set; }
}

public class CardDrawModel : ViewModelBase
{

    private static readonly ILog log = LogManager.GetLogger(typeof(CardBagViewModel));

    private readonly ObservableList<CardViewModel> cards = new ObservableList<CardViewModel>();

    private InteractionRequest<RewardViewModel> openRewardRequest;

    private int openCount = 0;
    private int openFinish = 0;

    private List<Reward> rewards = new List<Reward>();

    private int countDown = 20;
    private IAsyncResult result;

    public CardDrawModel() : base()
    {
        for (int i = 0; i < 9; i++)
        {
            CardViewModel cardViewModel = new CardViewModel();
            cardViewModel.BackIcon = "a0";
            cardViewModel.FrontIcon = "CardBack";
            cards.Add(cardViewModel);
        }

        this.openRewardRequest = new InteractionRequest<RewardViewModel>(this);


        Reward reward1 = new Reward();
        reward1.Icon = "a7";
        reward1.Name = "海底捞全单7折券x1";
        reward1.Desc = "海底捞全单7折券";
        reward1.DescIcon = "haidilao";
        rewards.Add(reward1);

        Reward reward2 = new Reward();
        reward2.Icon = "a5";
        reward2.Name = "肯德基全家桶5折券x1";
        reward2.Desc = "肯德基全家桶5折券";
        reward2.DescIcon = "kendeji";
        rewards.Add(reward2);

        rewards.Reverse();

        this.openFinish = this.openCount = rewards.Count;


        ApplicationContext context = Context.GetApplicationContext();
        ITask task = context.GetService<ITask>();
        
        this.result = task.Scheduled.ScheduleAtFixedRate(() =>
        {
            CountDown--;
            if (countDown <= 0)
            {
                Executors.RunOnMainThread(() =>
                {
                    CloseCardBag();
                }, true);

            }

        }, 1000, 1000);
    }

    private void CloseCardBag()
    {
        this.result.Cancel();
        for (int i = 0; i < rewards.Count; i++)
        {
            DrawCard(i);
        }
    }

    public ObservableList<CardViewModel> Cards
    {
        get { return this.cards; }
    }

    public InteractionRequest<RewardViewModel> OpenRewardRequest
    {
        get { return this.openRewardRequest; }
    }

    public int CountDown
    {
        get { return this.countDown; }
        set { this.Set<int>(ref countDown, value, "CountDown"); }
    }
    public int OpenCount
    {
        get { return this.openCount; }
        set { this.Set<int>(ref openCount, value, "OpenCount"); }
    }

    public void Select(int index)
    {
        if (index <= -1 || index > this.cards.Count - 1)
            return;

        for (int i = 0; i < this.cards.Count; i++)
        {
            if (i == index)
            {
                DrawCard(i);
            }

        }
    }

    private void DrawCard(int index)
    {
        if (this.openCount <= 0)
        {
            return;
        }
        //log.DebugFormat("Select, Current Index:{0}", index);
        var cardModel = this.cards[index];

        var reward = rewards[openCount - 1];
        cardModel.BackIcon = reward.Icon;
        this.OpenCount--;

        cardModel.ClickedRequest.Raise(() => {
            openFinish--;
            if (this.openFinish <= 0)
            {
                //RewardViewModel rewardViewModel = new RewardViewModel();
                //this.openRewardRequest.Raise(rewardViewModel);
                AllDrawCard();

            }
        });
    }
    private void AllDrawCard()
    {
        int count = 0;
        foreach (CardViewModel cardModel in this.cards)
        {
            cardModel.ClickedRequest.Raise(() =>
            {
                count++;
                if (count >= this.cards.Count)
                {

                    ApplicationContext context = Context.GetApplicationContext();
                    ITask task = context.GetService<ITask>();
                    
                    int times = 0;
                    this.result = task.Scheduled.ScheduleAtFixedRate(() =>
                    {
                        times++;
                        
                        if (times >= 2)
                        {
                            Executors.RunOnMainThread(() =>
                            {
                                
                                result.Cancel();
                                RewardViewModel rewardViewModel = new RewardViewModel(this.rewards);
                                this.openRewardRequest.Raise(rewardViewModel);
                            }, true);

                        }

                    }, 1000, 1000);
                   

                }
            });
        }
    }
}
