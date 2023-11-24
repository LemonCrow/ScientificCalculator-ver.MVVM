using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.TextFormatting;
using CommunityToolkit.Mvvm.Input;
using ScientificCalculator_ver.MVVM.Models;

namespace ScientificCalculator_ver.MVVM.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {
        internal string inputNumber = "";
        internal string currentExpression = "";
        internal bool _isInt = true;

        private double _inputNumberFontSize;
        private double _currentExpressionFontSize;
        private readonly TextSIze _textSize = new TextSIze();

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
            UpdateFontSizes();
        }

        public double InputNumberFontSize
        {
            get => _inputNumberFontSize;
            set => SetProperty(ref _inputNumberFontSize, value);
        }

        public double CurrentExpressionFontSize
        {
            get => _currentExpressionFontSize;
            set => SetProperty(ref _currentExpressionFontSize, value);
        }

        internal void UpdateFontSizes()
        {
            InputNumberFontSize = TextSIze.CalculateFontSizeForExpression(this);
            CurrentExpressionFontSize = TextSIze.CalculateFontSize(this);
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
                    viewModel.InputNumber = formatHelper.FormatNumberWithCommas(str);
                    viewModel.UpdateFontSizes();
                }
                else
                {
                    str = viewModel.inputNumber + str;
                    viewModel.InputNumber = formatHelper.FormatNumberWithCommas(str);
                    viewModel.UpdateFontSizes();
                }
            }

        }//숫자패드

        internal void AddDotNumber(object parameter, ViewModel viewModel)
        {
            string str = viewModel.inputNumber + Convert.ToString(parameter);

            if (viewModel.inputNumber != "0" && viewModel._isInt == true)
            {
                viewModel._isInt = false;
                viewModel.InputNumber = formatHelper.FormatNumberWithCommas(str);
                viewModel.UpdateFontSizes();
            }
        }//소수점

        internal void ChangePM(object parameter, ViewModel viewModel)
        {
            string str = viewModel.inputNumber;

            str = !str.Contains("-") && str != "0" ? "-" + str : str.Replace("-", "");

            System.Diagnostics.Debug.WriteLine(str);

            viewModel.InputNumber = formatHelper.FormatNumberWithCommas(str);
            viewModel.UpdateFontSizes();
        }// +/-

        internal void ExpressionUp(object parameter, ViewModel viewModel)
        {
            string str = viewModel.InputNumber;
            if (str.EndsWith("."))
            {
                str = str.Replace(".", "");
            }
            viewModel.currentExpression += str;
            viewModel.InputNumber = Convert.ToString(formatHelper.FormatNumberWithCommas(calculration.MathResult(formatHelper.FormatNumberDelCommas(viewModel.currentExpression))));
            viewModel.CurrentExpression += " " + Convert.ToString(parameter) + " ";
            viewModel.UpdateFontSizes();
            viewModel._isInt = true;

        }//연산
    }//숫자, 소수점 패드.

    class TextSIze
    {
        public static double CalculateFontSizeForExpression(ViewModel viewModel)
        {
            if (viewModel.inputNumber.Length < 20) return 30;
            if (viewModel.inputNumber.Length < 30) return 20;
            if (viewModel.inputNumber.Length < 40) return 15;

            return 14;
        } //숫자 텍스트 사이즈 길이 비례 조절

        public static double CalculateFontSize(ViewModel viewModel)
        {

            if (viewModel.currentExpression.Length < 10) return 20;
            if (viewModel.currentExpression.Length < 20) return 15;
            if (viewModel.currentExpression.Length < 30) return 10;

            return 12;
        } // 텍스트 사이즈 자동조절
    }
}
