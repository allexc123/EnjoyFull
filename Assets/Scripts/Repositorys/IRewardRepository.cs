using System.Collections;
using System.Collections.Generic;
using Loxodon.Framework.Asynchronous;
using UnityEngine;

public interface IRewardRepository {

    IAsyncResult<List<Reward>> Get();

    void Clear();
}
