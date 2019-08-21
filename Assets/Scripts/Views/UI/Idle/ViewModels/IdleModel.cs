using System.Collections;
using System.Collections.Generic;
using Loxodon.Framework.Asynchronous;
using Loxodon.Framework.Commands;
using Loxodon.Framework.Contexts;
using Loxodon.Framework.Interactivity;
using Loxodon.Framework.ViewModels;
using UnityEngine;

public class IdleModel : ViewModelBase
{
    private InteractionRequest<WheelViewModel> showWheelRequest;

    private SimpleCommand showWheel;

    private string icon;

    //private IAsyncResult result;

    private int index = 0;

    private List<string> icons = new List<string>();

    public IdleModel():base()
    {
        
        icons.Add("lexianggame");
        icons.Add("haidilao");
        icons.Add("kendeji");

        this.showWheelRequest = new InteractionRequest<WheelViewModel>(this);

        this.showWheel = new SimpleCommand(()=> {
            var model = new WheelViewModel();
            showWheelRequest.Raise(model);
        });

        Icon = icons[0];
    }

    public void Startup()
    {
        ApplicationContext context = Context.GetApplicationContext();
        ITask task = context.GetService<ITask>();

        //this.result = 
        task.Scheduled.ScheduleAtFixedRate(() =>
        {
            index++;
            if (index>= icons.Count)
            {
                index = 0;
            }
            Icon = icons[index];

        }, 1000, 3000);
    }

    public InteractionRequest<WheelViewModel> ShowWheelRequest
    {
        get { return this.showWheelRequest; }
    }
    public ICommand ShowWheel 
    {
        get { return this.showWheel; }
    }
    public string Icon
    {
        get { return this.icon; }
        set { Set<string>(ref this.icon, value, "Icon"); }
    }
}
