using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using ScientificCalculator_ver.MVVM.Models;

namespace ScientificCalculator_ver.MVVM.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {
        private string inputNumber = "";
        private string currentExpression = "";
        public bool _isInt = true;

        public ICommand NumberCommand { get; private set; }

        public ViewModel() 
        {
            InputNumber = "0";
            NumberCommand = new RelayCommand<object>(AddNumber);

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

        private void AddNumber(object parameter)
        {
            string str = "";
            FormatHelper formatHelper = new FormatHelper();

            if (parameter is string number)
            {
                if(inputNumber == "0")
                {
                    str = number;
                    InputNumber = formatHelper.FormatNumberWithCommas(str, this);
                }
                else
                {
                    str += inputNumber + number;
                    InputNumber = formatHelper.FormatNumberWithCommas(str, this);
                }
            }
        }//숫자패드

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
}
