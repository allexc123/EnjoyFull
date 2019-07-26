using Loxodon.Framework.Commands;
using Loxodon.Framework.ViewModels;
using Loxodon.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class WheelItemViewModel : ViewModelBase
{
    private static readonly ILog log = LogManager.GetLogger(typeof(WheelItemViewModel));
    private string icon;
    private bool showHand;

    private SimpleCommand command;

    public WheelItemViewModel():base()
    {
        this.showHand = false;
        this.command = new SimpleCommand(()=> {
            //log.Debug("AAAAAAAAAAAAAAAAAAA");
            this.ShowHand = true;
            this.command.Enabled = false;
           
        });
    }

    public string Icon
    {
        get { return this.icon; }
        set { this.Set<string>(ref icon, value, "Icon"); }
    }

    public SimpleCommand Command
    {
        get { return this.command; }
    }

    public bool ShowHand
    {
        get { return this.showHand; }
        set { this.Set<bool>(ref showHand, value, "ShowHand"); }
    }
}