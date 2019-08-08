using System.Collections;
using System.Collections.Generic;
using Loxodon.Framework.Asynchronous;
using Loxodon.Framework.Execution;
using UnityEngine;

public class RewardRepository : IRewardRepository {

    private List<Reward> rewards;

    private IThreadExecutor executor;

    public RewardRepository()
    {
        this.rewards = new List<Reward>();
        executor = new ThreadExecutor();
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
}
