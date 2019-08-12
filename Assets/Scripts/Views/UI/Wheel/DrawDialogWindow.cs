using System.Collections;
using System.Collections.Generic;
using Loxodon.Framework.Binding;
using Loxodon.Framework.Binding.Builder;
using Loxodon.Framework.Views;
using UnityEngine;
using UnityEngine.UI;

public class DrawDialogWindow : Window
{

    public Text countDown;
    public Text pay;
    public Text drawCount;

    public Button ConfirmButton;

    public Button CancelButton;

    public Button OutsideButton;

    public DrawDialogViewModel viewModel;

    protected override void OnCreate(IBundle bundle)
    {
        this.WindowType = WindowType.DIALOG;

        BindingSet<DrawDialogWindow, DrawDialogViewModel> bindingSet = this.CreateBindingSet<DrawDialogWindow, DrawDialogViewModel>();
        bindingSet.Bind(this.ConfirmButton).For(v => v.onClick).To(vm => vm.ConfirmCommand).OneWay();
        bindingSet.Bind(this.CancelButton).For(v => v.onClick).To(vm => vm.CancelCommand).OneWay();
        bindingSet.Bind(this.OutsideButton).For(v => v.onClick).To(vm => vm.CancelCommand).OneWay();

        bindingSet.Bind(this.countDown).For(v => v.text).ToExpression(vm => string.Format("{0}", vm.CountDown)).OneWay();
        bindingSet.Bind(this.pay).For(v => v.text).ToExpression(vm => string.Format("{0}", vm.Pay)).OneWay();
        bindingSet.Bind(this.drawCount).For(v => v.text).ToExpression(vm => string.Format("{0}", vm.DrawCount)).OneWay();

        bindingSet.Build();
    }

    protected virtual void OnClick(int which)
    {
        this.viewModel.OnClick(which);
    }
}
