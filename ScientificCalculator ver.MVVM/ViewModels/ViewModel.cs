using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using ScientificCalculator_ver.MVVM.Models;

namespace ScientificCalculator_ver.MVVM.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {
        internal string inputNumber = "";
        internal string currentExpression = "";
        internal bool _isInt = true;

        NumPad numPad = new NumPad();
        FormatHelper formatHelper = new FormatHelper();

        public ICommand NumberCommand { get; private set; }
        public ICommand DotNumberCommand { get; private set; }
        public ICommand ChangePMCommand { get; private set; }
        public ICommand CalculationCommand { get; private set; }

        public ViewModel()
        {
            InputNumber = "0";
            NumberCommand = new RelayCommand<object>(AddNumberWrapper);
            DotNumberCommand = new RelayCommand<object>(AddDotNumberWrapper);
            ChangePMCommand = new RelayCommand<object>(ChangePMWrapper);
            CalculationCommand = new RelayCommand<object>(CalculationWrapper);
        }

        public string InputNumber
        {
            get { return inputNumber; }
            set { SetProperty(ref inputNumber, value); }
        }

        public string CurrentExpression
        {
            get { return currentExpression; }
            set { SetProperty(ref currentExpression, value); }
        }

        private void AddNumberWrapper(object parameter)
        {
            if (parameter is string number)
            {
                numPad.AddNumber(number, this);
            }
        }

        private void AddDotNumberWrapper(object parameter)
        {
            if (parameter is string number)
            {
               
                numPad.AddDotNumber(number, this);
            }
        }

        private void ChangePMWrapper(object parameter)
        {
            if (parameter is string number)
            {
                numPad.ChangePM(number, this);
            }
        }

        private void CalculationWrapper(object parameter)
        {
            numPad.ExpressionUp(parameter, this);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return;
            field = value;
            OnPropertyChanged(propertyName);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // 추가적인 메소드 및 로직
    }

    class NumPad
    {
        FormatHelper formatHelper = new FormatHelper();
        Calculration calculration = new Calculration();
        internal void AddNumber(object parameter, ViewModel viewModel)
        {
            string str = Convert.ToString(parameter);
            if (formatHelper.CountNumber(viewModel.inputNumber))
            {
                if (viewModel.inputNumber == "0")
                {
                    viewModel.InputNumber = formatHelper.FormatNumberWithCommas(str, viewModel);
                }
                else
                {
                    str = viewModel.inputNumber + str;
                    viewModel.InputNumber = formatHelper.FormatNumberWithCommas(str, viewModel);
                }
            }

        }//숫자패드

        internal void AddDotNumber(object parameter, ViewModel viewModel)
        {
            string str = viewModel.inputNumber + Convert.ToString(parameter);

            if (viewModel.inputNumber != "0" && viewModel._isInt == true)
            {
                viewModel._isInt = false;
                viewModel.InputNumber = formatHelper.FormatNumberWithCommas(str, viewModel);
            }
        }//소수점

        internal void ChangePM(object parameter, ViewModel viewModel)
        {
            string str = viewModel.inputNumber;

            str = !str.Contains("-") && str != "0" ? "-" + str : str.Replace("-", "");

            System.Diagnostics.Debug.WriteLine(str);

            viewModel.InputNumber = formatHelper.FormatNumberWithCommas(str, viewModel);
        }// +/-

        internal void ExpressionUp(object parameter, ViewModel viewModel)
        {
            string str = viewModel.InputNumber;
            
            viewModel.currentExpression += str;
            viewModel.InputNumber = Convert.ToString(calculration.MathResult(viewModel.currentExpression));
            viewModel.CurrentExpression += " " + Convert.ToString(parameter) + " ";
            viewModel._isInt = true;

        }//연산
    }//숫자, 소수점 패드
}
