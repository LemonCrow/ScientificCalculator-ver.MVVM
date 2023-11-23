using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ScientificCalculator_ver.MVVM.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {
        private string inputNumber = "";
        private string currentExpression = "";
        
        public ViewModel() 
        {
            InputNumber = "0";

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
