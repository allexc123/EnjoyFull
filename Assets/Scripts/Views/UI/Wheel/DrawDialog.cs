using Loxodon.Framework.Binding;
using Loxodon.Framework.Contexts;
using Loxodon.Framework.Execution;
using Loxodon.Framework.Views;
using Loxodon.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DrawDialog : IDialog
{
    private static readonly ILog log = LogManager.GetLogger(typeof(DrawDialog));

    public const int BUTTON_POSITIVE = -1;
    public const int BUTTON_NEGATIVE = -2;


    private const string DEFAULT_VIEW_NAME = "UI/DrawDialog";

    private static string viewName;
    public static string ViewName
    {
        get { return string.IsNullOrEmpty(viewName) ? DEFAULT_VIEW_NAME : viewName; }
        set { viewName = value; }
    }


    private DrawDialogWindow window;
    private DrawDialogViewModel viewModel;

    public void Cancel()
    {
        this.viewModel.OnClick(BUTTON_NEGATIVE);
    }

    public void Show()
    {
        this.window.SetDataContext(viewModel);
        this.window.Create();
        this.window.Show();

    }

    public object WaitForClosed()
    {
        return Executors.WaitWhile(() => !this.viewModel.Closed);
    }

    

    private DrawDialog(DrawDialogWindow window, DrawDialogViewModel viewModel)
    {
        this.window = window;
        this.viewModel = viewModel;
    }

    public static DrawDialog ShowDrawDialog(int countDown, Action<int> afterHideCallback)
    {

        DrawDialogViewModel viewModel = new DrawDialogViewModel(countDown, afterHideCallback);
        ///viewModel.DrawCount = 20;

        ApplicationContext context = Context.GetApplicationContext();
        IUIViewLocator locator = context.GetService<IUIViewLocator>();
        if (locator == null)
        {
            if (log.IsWarnEnabled)
                log.Warn("Not found the \"IUIViewLocator\".");

            throw new NotFoundException("Not found the \"IUIViewLocator\".");
        }
        DrawDialogWindow window = locator.LoadView<DrawDialogWindow>(ViewName);
        if (window == null)
        {
            if (log.IsWarnEnabled)
                log.WarnFormat("Not found the dialog window named \"{0}\".", viewName);

            throw new NotFoundException(string.Format("Not found the dialog window named \"{0}\".", viewName));
        }
        DrawDialog drawDialog = new DrawDialog(window, viewModel);
        drawDialog.Show();
        return drawDialog;
    }
}

public class DrawDialogNotification
{
    private int dialogResult;
    private int countDown;

    public int DialogResult
    {
        get { return this.dialogResult; }
        set { this.dialogResult = value; }
    }

    public int CountDown
    {
        get { return this.countDown; }
        set { this.countDown = value; }
    }
}