using Loxodon.Framework.Interactivity;
using Loxodon.Framework.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CardViewModel : ViewModelBase
{
    private string frontIcon;
    private string backIcon;

    private InteractionRequest clickedRequest;

    public CardViewModel():base()
    {
        this.clickedRequest = new InteractionRequest(this);
    }

    public string FrontIcon
    {
        get { return this.frontIcon; }
        set { this.Set<string>(ref frontIcon, value, "FrontIcon"); }
    }
    public string BackIcon
    {
        get { return this.backIcon; }
        set { this.Set<string>(ref backIcon, value, "BackIcon"); }
    }

    public InteractionRequest ClickedRequest
    {
        get { return this.clickedRequest; }
    }
}