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

    //抽奖次数
    private int drawCount = 0;
    //需要的钱
    private int money = 0;
    //概率
    private int probability = 3;
    //付款二维码
    private string qrCode;
    //选择的转盘下标
    private int selectIndex;

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


        //测试数据
        for (int i = 0; i < 12; i++)
        {
            idxs.Add(i);
        }
        qrCode = "";
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
        probability = 3;
        selectIndex = 0;
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

    public int Probability()
    {
        return this.probability;
    }

    public string QRCode()
    {
        return this.qrCode;
    }
    public void SetSelectIndex(int index)
    {
        this.selectIndex = index;
    }

    public int GetDrawIndex()
    {
        return draw();
    }

    //测试代码
    List<int> idxs = new List<int>();
    private int draw()
    {
        System.Random random = new System.Random();
        int rand = random.Next(100);
        if (rand < probability)
        {
            return selectIndex;
        }
        else
        {
            int idx = random.Next(idxs.Count - 1);
            int data = idxs[idx];
            idxs.Remove(data);
            return data;
        }


    }


}
