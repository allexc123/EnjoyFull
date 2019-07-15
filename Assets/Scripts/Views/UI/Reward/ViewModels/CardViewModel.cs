using Loxodon.Framework.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CardViewModel : ViewModelBase
{
    private string frontImage;
    private string backImage;

    public string FrontImage
    {
        get { return this.frontImage; }
        set { this.Set<string>(ref frontImage, value, "FrontImage"); }
    }
    public string BackImage
    {
        get { return this.backImage; }
        set { this.Set<string>(ref backImage, value, "BackImage"); }
    }
}