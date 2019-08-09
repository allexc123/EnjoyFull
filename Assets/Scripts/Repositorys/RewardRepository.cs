using System.Collections;
using System.Collections.Generic;
using Loxodon.Framework.Asynchronous;
using Loxodon.Framework.Execution;
using UnityEngine;

public class RewardRepository : IRewardRepository
{
    //奖品信息
    private List<Award> awardCache = new List<Award>();
    //中奖信息
    private List<Reward> rewards = new List<Reward>();

    private IThreadExecutor executor;

    public RewardRepository()
    {
        executor = new ThreadExecutor();

        Award award1 = new Award();
        award1.Name = "海底捞免单券";
        award1.Count = 1;
        award1.Quality = 5;
        Award award2 = new Award();
        award2.Name = "肯德基全家桶";
        award2.Count = 1;
        award2.Quality = 4;
        Award award3 = new Award();
        award3.Name = "小豆面馆油条";
        award3.Count = 5;
        award3.Quality = 3;

        awardCache.Add(award1);
        awardCache.Add(award2);
        awardCache.Add(award3);
    }

    public virtual IAsyncResult<List<Reward>> Get()
    {
        return executor.Execute(()=> {
            return this.rewards;
        });
    }
    public virtual void Clear()
    {
        this.rewards.Clear();
    }

    public IAsyncResult<List<Award>> GetAwards()
    {
        return executor.Execute(() =>{
            return this.awardCache;
        });
    }
}
