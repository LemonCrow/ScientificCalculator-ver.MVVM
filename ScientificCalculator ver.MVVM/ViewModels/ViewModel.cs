﻿using System;
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

        public ICommand NumberCommand { get; private set; }
        public ICommand DotNumberCommand { get; private set; }

        public ViewModel()
        {
            InputNumber = "0";
            NumberCommand = new RelayCommand<object>(AddNumberWrapper);
            DotNumberCommand = new RelayCommand<object>(AddDotNumberWrapper);
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
                NumPad numPad = new NumPad();
                numPad.AddNumber(number, this);
            }
        }

        private void AddDotNumberWrapper(object parameter)
        {
            if (parameter is string number)
            {
                NumPad numPad = new NumPad();
                numPad.AddDotNumber(number, this);
            }
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
        internal void AddNumber(object parameter, ViewModel viewModel)
        {
            string str = Convert.ToString(parameter);
            FormatHelper formatHelper = new FormatHelper();

            if (viewModel.inputNumber == "0")
            {
                viewModel.InputNumber = formatHelper.FormatNumberWithCommas(str, viewModel);
            }
            else
            {
                str = viewModel.inputNumber + str;
                viewModel.InputNumber = formatHelper.FormatNumberWithCommas(str, viewModel);
            }

        }//숫자패드

        internal void AddDotNumber(object parameter, ViewModel viewModel)
        {
            FormatHelper formatHelper = new FormatHelper();
            string str = viewModel.inputNumber + Convert.ToString(parameter);
            System.Diagnostics.Debug.WriteLine(str);

            if (viewModel.inputNumber != "0" && viewModel._isInt == true)
            {
                viewModel.InputNumber = formatHelper.FormatNumberWithCommas(str, viewModel);
                viewModel._isInt = false;
            }
        }//소수점
    }//숫자, 소수점 패드
}
