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

    private int drawCount = 0;
    private int money = 0;

    public RewardRepository()
    {
        executor = new ThreadExecutor();

        money = 5;
        Award award1 = new Award();
        award1.Name = "海底捞免单券";
        award1.Count = 1;
        award1.Quality = (int)QualityType.Orange;
        Award award2 = new Award();
        award2.Name = "肯德基全家桶";
        award2.Count = 1;
        award2.Quality = (int)QualityType.Purple;
        Award award3 = new Award();
        award3.Name = "小豆面馆油条";
        award3.Count = 5;
        award3.Quality = (int)QualityType.Blue;

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
            List<Award> result = new List<Award>();
            int count = 0;
            foreach (Award award in awardCache)
            {
                result.Add(award);
                count++;
                if (this.drawCount < count)
                {
                    break;
                }
                
               
            }
            return result;
        });
    }

    public void AddDrawCount()
    {
        this.drawCount++;
    }

    public int GetDrawCount()
    {
        return this.drawCount;
    }

    public int GetMoney()
    {
        return (money - drawCount) < 1 ? 1 : (money - drawCount);
    }
}
