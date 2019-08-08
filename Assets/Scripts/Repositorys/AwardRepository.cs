using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loxodon.Framework.Asynchronous;
using Loxodon.Framework.Execution;

public class AwardRepository : IAwardRepository
{
    
    private List<Award> awardCache = new List<Award>();

    private int selectIndex = -1;

    private IThreadExecutor executor;

    public AwardRepository()
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

    public void SelectIndex(int index)
    {
        this.selectIndex = index;
    }

    public virtual IAsyncResult<List<Award>> Get(int quality)
    {
        return executor.Execute(()=> {
           
            return awardCache;
        });
    }

    public virtual void Clear()
    {
        this.selectIndex = -1;
        //this.awardCache.Clear();
    }
}
