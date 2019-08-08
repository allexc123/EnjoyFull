using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AwardRepository : IAwardRepository
{
    
    private Dictionary<int, Award> awardCache = new Dictionary<int, Award>();

    private int selectIndex = -1;

    public AwardRepository()
    {

    }

    public void SelectIndex(int index)
    {
        this.selectIndex = index;
    }
}
