using System.Collections;
using System.Collections.Generic;
using Loxodon.Framework.Asynchronous;
using UnityEngine;

public interface IAwardRepository  {

    void SelectIndex(int index);

    IAsyncResult<List<Award>> Get(int quality);

    void Clear();

}

