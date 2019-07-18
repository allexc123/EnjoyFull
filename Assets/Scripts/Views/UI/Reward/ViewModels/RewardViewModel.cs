using Loxodon.Framework.Asynchronous;
using Loxodon.Framework.Commands;
using Loxodon.Framework.Contexts;
using Loxodon.Framework.Execution;
using Loxodon.Framework.Interactivity;
using Loxodon.Framework.Observables;
using Loxodon.Framework.ViewModels;
using Loxodon.Framework.Views;


public class RewardViewModel : ViewModelBase
{
    private readonly ObservableList<CouponViewModel> coupons = new ObservableList<CouponViewModel>();

    private string icon;

    private string phoneNumber;

    private SimpleCommand receiveReward;
    private SimpleCommand terminate;

    private InteractionRequest<DialogNotification> alertDialogRequest;

    private InteractionRequest interactionFinished;

    private int countDown = 60;

    private IAsyncResult result;

    public RewardViewModel(): base()
    {

        ApplicationContext context = Context.GetApplicationContext();
        ITask task = context.GetService<ITask>();

        CouponViewModel couponViewModel = new CouponViewModel((model) => {
            Icon = model.Icon;
        });
        couponViewModel.Icon = "a4";
        couponViewModel.Name = "肯德基";
        couponViewModel.Desc = "肯德基4折优惠券";

        coupons.Add(couponViewModel);

        CouponViewModel couponViewModel1 = new CouponViewModel((model)=> {
            Icon = model.Icon;
        });
        couponViewModel1.Icon = "a9";
        couponViewModel1.Name = "肯德基1";
        couponViewModel1.Desc = "肯德基4折优惠券";
        coupons.Add(couponViewModel1);

        Icon = couponViewModel1.Icon;

        this.alertDialogRequest = new InteractionRequest<DialogNotification>(this);

        this.interactionFinished = new InteractionRequest(this);

        this.receiveReward = new SimpleCommand(() =>
        {
            this.receiveReward.Enabled = false;

            DialogNotification notification = new DialogNotification("请确认你的手机号",$"{this.phoneNumber}", "Yes", "No", true);

            System.Action<DialogNotification> callback = n =>
            {
                this.receiveReward.Enabled = true;

                if (n.DialogResult == AlertDialog.BUTTON_POSITIVE)
                {
                    ClosePage();
                }
                else if (n.DialogResult == AlertDialog.BUTTON_NEGATIVE)
                {
                    
                }
            };

            this.alertDialogRequest.Raise(notification, callback);
        });

        this.terminate = new SimpleCommand(() =>
        {
            this.receiveReward.Enabled = false;

            DialogNotification notification = new DialogNotification("", "你有奖券未领取，现在关闭奖券奖消失，是否强制关闭？", "继续关闭", "去领奖券", true);

            System.Action<DialogNotification> callback = n =>
            {
                this.receiveReward.Enabled = true;

                if (n.DialogResult == AlertDialog.BUTTON_POSITIVE)
                {
                    ClosePage();
                }
                else if (n.DialogResult == AlertDialog.BUTTON_NEGATIVE)
                {

                }
            };

            this.alertDialogRequest.Raise(notification, callback);
        });

        this.result = task.Scheduled.ScheduleAtFixedRate(() =>
        {
            CountDown -= 1;
            if (countDown <= 0)
            {
                Executors.RunOnMainThread(() =>
                {
                    ClosePage();
                }, true);
               
            }

        }, 1000, 1000);

    }

    private void ClosePage()
    {
        this.result.Cancel();
        interactionFinished.Raise();
    }


    public ObservableList<CouponViewModel> Coupons
    {
        get { return this.coupons; }
    }

    public string Icon
    {
        get { return this.icon; }
        set { Set<string>(ref this.icon, value, "Icon"); }
    }
    public string PhoneNumber
    {
        get { return this.phoneNumber; }
        set { Set<string>(ref this.phoneNumber, value, "PhoneNumber"); }
    }
    public IInteractionRequest AlertDialogRequest
    {
        get { return this.alertDialogRequest; }
    }

    public ICommand ReceiveReward
    {
        get { return this.receiveReward; }
    }

    public ICommand Terminate
    {
        get { return this.terminate; }
    }

    public IInteractionRequest InteractionFinished
    {
        get { return this.interactionFinished; }
    }

    public int CountDown
    {
        get { return this.countDown; }
        set { this.Set<int>(ref countDown, value, "CountDown"); }
    }
}