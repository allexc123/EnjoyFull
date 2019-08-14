using System.Collections;
using System.Collections.Generic;
using Loxodon.Framework.Asynchronous;
using UnityEngine;

public interface IRewardRepository {

    IAsyncResult<List<Reward>> Get();

    IAsyncResult<List<Award>> GetAwards();

    void AddDrawCount();

    int GetDrawCount();

    int GetMoney();

    void Clear();
}
