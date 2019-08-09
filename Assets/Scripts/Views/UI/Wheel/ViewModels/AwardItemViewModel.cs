using Loxodon.Framework.Commands;
using Loxodon.Framework.ViewModels;
using Loxodon.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AwardItemViewModel : ViewModelBase
{
    private static readonly ILog log = LogManager.GetLogger(typeof(AwardItemViewModel));

    private string name;


    public AwardItemViewModel():base()
    {
        
    }

    public string Name
    {
        get { return this.name; }
        set { this.Set<string>(ref this.name, value, "Name"); }
    }

}