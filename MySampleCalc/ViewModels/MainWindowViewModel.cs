using System;
using System.Reactive;
using ReactiveUI;
using MySampleCalc.Models;
using Avalonia.Controls.Platform;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using Tmds.DBus.Protocol;

namespace MySampleCalc.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private string _operation = String.Empty;
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
    public ReactiveCommand<Unit, Unit> Delete { get; }
    public ReactiveCommand<Unit, Unit> DeleteAll { get; }

    public ReactiveCommand<Unit, Unit>[] EnterNumber { get; }
    public Dictionary<String, ReactiveCommand<Unit, Unit>> EnterSymbol { get; }
    public ReactiveCommand<Unit, Unit> EnterDecimal => EnterSymbol["."];
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
                x => Calculator.CouldBeValidOperation(x, number.ToString()[0])
            );
            EnterNumber[number] = ReactiveCommand.Create(() => { Operation += number; }, couldBeValidNumber);
        }

        var _enterSymbol = new Dictionary<String, ReactiveCommand<Unit, Unit>>();
        foreach (var _symbol in new string[] { "+", "-", "/", "*", "(", ")", "." })
        {
            var symbol = _symbol;
            IObservable<bool> couldBeValidSymbol = this.WhenAnyValue(
             x => x.Operation,
             x => Calculator.CouldBeValidOperation(x, symbol[0])
            );

            _enterSymbol.Add(symbol, ReactiveCommand.Create(() => { Operation += symbol; }, couldBeValidSymbol));
        }
        EnterSymbol = _enterSymbol;

        IObservable<bool> isOperationNotEmpty = this.WhenAnyValue(
          x => x.Operation,
          x => x.Length > 0);

        Delete = ReactiveCommand.Create(
          void () => Operation = Operation.Substring(0, Operation.Length - 1),
          isOperationNotEmpty
        );

        DeleteAll = ReactiveCommand.Create(
                void () => Operation = String.Empty,
                isOperationNotEmpty
            );
    }


}
