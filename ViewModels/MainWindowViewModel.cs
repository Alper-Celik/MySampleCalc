using System;
using System.Reactive;
using ReactiveUI;
using MySampleCalc.Models;
using Avalonia.Controls.Platform;
using System.Globalization;

namespace MySampleCalc.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private string _operation = "Welcome to My Calculator!";
    public string Operation
    {
        get => _operation;
        set => this.RaiseAndSetIfChanged(ref _operation, value);
    }

    private string _result = String.Empty;
    public string Result
    {
        get => _result;
        set => _result = this.RaiseAndSetIfChanged(ref _result, value);
    }


    public ReactiveCommand<Unit, Unit> Calculate { get; }

    public ReactiveCommand<Unit, Unit>[] EnterNumber { get; }
    public MainWindowViewModel()
    {
        IObservable<bool> isOperationValid = this.WhenAnyValue(
            x => x.Operation,
            x => Calculator.IsValidOperation(x)
        );
        Calculate = ReactiveCommand.Create(() =>
            {
                Result = Calculator.Calculate(Operation);
            }, isOperationValid);

        EnterNumber = new ReactiveCommand<Unit, Unit>[10];
        for (int i = 0; i < EnterNumber.Length; i++)
        {
            int number = i;
            IObservable<bool> couldBeValidNumber = this.WhenAnyValue(
                x => x.Operation,
                x => Calculator.CouldBeValidOperation(x + number)
            );
            EnterNumber[number] = ReactiveCommand.Create(() => { Operation += number; }, couldBeValidNumber);
        }
    }


}
