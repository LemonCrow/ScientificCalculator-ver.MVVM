using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Reflection.Metadata;
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
        internal string resultNumber = "0";
        internal string currentExpression = "";
        internal string degres = "angle * " + Math.PI + " / 180"; //각도 DEG버튼

        internal bool _isInt = true;
        internal bool _isBreaket = false;
        internal bool _isExp = false;

        //메모리
        internal bool _isMC = false;
        internal bool _isMR = false;
        internal bool _isMS = true;
        internal bool _isMP = true;
        internal bool _isMM = true;

        private bool _isToggled = false;
        private bool _isFE = false;
        private bool _isTrigonometryPopupOpen;
        private bool _isFunctuonPopupOpen;
        private bool _is2nd = false;
        private bool _isHyp = false;
        

        private string _buttonSqr = "x²";
        private string _buttonRoot = "2√x";
        private string _buttonSquare = " x^y";
        private string _buttonSquare10 = "10^x";
        private string _buttonLog = "log";
        private string _buttonNSquare = "ln";

        private string _buttonSin = "sin";
        private string _buttonCos = "cos";
        private string _buttonTan = "tan";
        private string _buttonSec = "sec";
        private string _buttonCsc = "csc";
        private string _buttonCot = "cot";

        private string _buttonABS = "abs";
        private string _buttonFloor = "floor";
        private string _buttonCeil = "ceil";
        private string _buttonRand = "rand";
        private string _buttonDMS = "->dms";
        private string _buttonDEG = "->deg";

        private string _buttonCE = "C";


        private string _angleContent;

        private double _inputNumberFontSize;
        private double _currentExpressionFontSize;
        
        private readonly TextSIze _textSize = new TextSIze();


        NumPad numPad = new NumPad();
        FormatHelper formatHelper = new FormatHelper();
        OperatorButton operatorButton = new OperatorButton();
        MemoryButton memoryButton = new MemoryButton();

        public bool IsToggled
        {
            get => _isToggled;
            set
            {
                if (_isToggled != value)
                {
                    _isToggled = value;
                    UpdateButtonContents();
                    OnPropertyChanged(nameof(IsToggled));
                }
            }
        }
        public bool IsFE
        {
            get => _isFE;
            set
            {
                if (_isFE != value)
                {
                    _isFE = value;
                    if (_isFE)
                    {
                        InputNumber = int.Parse(inputNumber).ToString("E2");
                        resultNumber = inputNumber;
                    }
                    OnPropertyChanged(nameof(_isFE));
                }
            }
        }
        public bool IsMC
        {
            get => _isMC;
            set
            {
                _isMC = value;
                OnPropertyChanged(nameof(IsMC));
            }
        }
        public bool IsMR
        {
            get => _isMR;
            set
            {
                _isMR = value;
                OnPropertyChanged(nameof(IsMR));
            }
        }
        public bool IsMS
        {
            get => _isMS;
            set
            {
                _isMS = value;
                OnPropertyChanged(nameof(IsMS));
            }
        }
        public bool IsMP
        {
            get => _isMP;
            set
            {
                _isMP = value;
                OnPropertyChanged(nameof(IsMP));
            }
        }
        public bool IsMM
        {
            get => _isMM;
            set
            {
                _isMM = value;
                OnPropertyChanged(nameof(IsMM));
            }
        }

        public bool IsTrigonometryPopupOpen
        {
            get => _isTrigonometryPopupOpen;
            set => SetProperty(ref _isTrigonometryPopupOpen, value);
        }
        public bool IsFunctionPopupOpen
        {
            get => _isFunctuonPopupOpen;
            set => SetProperty(ref _isFunctuonPopupOpen, value);
        }

        public bool Is2nd
        {
            get => _is2nd;
            set
            {
                if (_is2nd != value)
                {
                    _is2nd = value;
                    UpdateButtonContents();
                    OnPropertyChanged(nameof(_is2nd));
                }
            }
        }

        public bool IsHyp
        {
            get => _isHyp;
            set
            {
                if (_isHyp != value)
                {
                    _isHyp = value;
                    UpdateButtonContents();
                    OnPropertyChanged(nameof(_isHyp));
                }
            }
        }

        //넘패드 2nd
        public string ButtonSqr
        {
            get => _buttonSqr;
            set => SetProperty(ref _buttonSqr, value);
        }

        public string ButtonRoot
        {
            get => _buttonRoot;
            set => SetProperty(ref _buttonRoot, value);
        }

        public string ButtonSquare
        {
            get => _buttonSquare;
            set => SetProperty(ref _buttonSquare, value);
        }

        public string ButtonSquare10
        {
            get => _buttonSquare10;
            set => SetProperty(ref _buttonSquare10, value);
        }

        public string ButtonLog
        {
            get => _buttonLog;
            set => SetProperty(ref _buttonLog, value);
        }

        public string ButtonNSquare
        {
            get => _buttonNSquare;
            set => SetProperty(ref _buttonNSquare, value);
        }
        public string AngleChange
        {
            get => _angleContent;
            set => SetProperty(ref _angleContent, value);
        }
        
        //삼각법
        public string ButtonSin
        {
            get => _buttonSin;
            set => SetProperty(ref _buttonSin, value);
        }
        public string ButtonCos
        {
            get => _buttonCos;
            set => SetProperty(ref _buttonCos, value);
        }
        public string ButtonTan
        {
            get => _buttonTan;
            set => SetProperty(ref _buttonTan, value);
        }
        public string ButtonSec
        {
            get => _buttonSec;
            set => SetProperty(ref _buttonSec, value);
        }
        public string ButtonCsc
        {
            get => _buttonCsc;
            set => SetProperty(ref _buttonCsc, value);
        }
        public string ButtonCot
        {
            get => _buttonCot;
            set => SetProperty(ref _buttonCot, value);
        }

        //함수
        public string ButtonABS
        {
            get => _buttonABS;
            set => SetProperty(ref _buttonABS, value);
        }
        public string ButtonFloor
        {
            get => _buttonFloor;
            set => SetProperty(ref _buttonFloor, value);
        }
        public string ButtonCeil
        {
            get => _buttonCeil;
            set => SetProperty(ref _buttonCeil, value);
        }
        public string ButtonRand
        {
            get => _buttonRand;
            set => SetProperty(ref _buttonRand, value);
        }
        public string ButtonDMS
        {
            get => _buttonDMS;
            set => SetProperty(ref _buttonDMS, value);
        }
        public string ButtonDEG
        {
            get => _buttonDEG;
            set => SetProperty(ref _buttonDEG, value);
        }

        public string ButtonCE
        {
            get => _buttonCE;
            set => SetProperty(ref _buttonCE, value);
        }

        private void UpdateButtonContents()
        {
            if (_isToggled)
            {
                ButtonSqr = "x³";
                ButtonRoot = "3√x";
                ButtonSquare = "y√x";
                ButtonSquare10 = "2^x";
                ButtonLog = "log_y(x)";
                ButtonNSquare = "e^x";
            }
            else
            {
                ButtonSqr = "x²";
                ButtonRoot = "2√x";
                ButtonSquare = " x^y";
                ButtonSquare10 = "10^x";
                ButtonLog = "log";
                ButtonNSquare = "ln";
            }

            if(_is2nd && _isHyp)
            {
                ButtonSin = "sinh^-1";
                ButtonCos = "cosh^-1";
                ButtonTan = "tanh^-1";
                ButtonSec = "sech^-1";
                ButtonCsc = "csch^-1";
                ButtonCot = "coth^-1";
            }
            else if(_is2nd && !_isHyp)
            {
                ButtonSin = "sin^-1";
                ButtonCos = "cos^-1";
                ButtonTan = "tan^-1";
                ButtonSec = "sec^-1";
                ButtonCsc = "csc^-1";
                ButtonCot = "cot^-1";
            }
            else if( !_is2nd && _isHyp)
            {
                ButtonSin = "sinh";
                ButtonCos = "cosh";
                ButtonTan = "tanh";
                ButtonSec = "sech";
                ButtonCsc = "csch";
                ButtonCot = "coth";
            }
            else 
            {
                ButtonSin = "sin";
                ButtonCos = "cos";
                ButtonTan = "tan";
                ButtonSec = "sec";
                ButtonCsc = "csc";
                ButtonCot = "cot";
            }
        }//토글 버튼 활성화에 따른 버튼 콘텐츠 값

        public ICommand NumberCommand { get; private set; }
        public ICommand DotNumberCommand { get; private set; }
        public ICommand ChangePMCommand { get; private set; }
        public ICommand CalculationCommand { get; private set; }
        public ICommand BasketCommand { get; private set; }
        public ICommand AngleChangeCommand { get; private set; }
        public ICommand DelCommand { get; private set; }
        public ICommand SurdCommand { get; private set; }
        public ICommand SpecialNumberCommand { get; private set; }
        public ICommand MemoryCommand { get; private set; }
        public ICommand ToggleTrigonometryPopupCommand { get; }
        public ICommand ToggleFunctionPopupCommand { get; }
        public ICommand CECommand { get; }
        public ICommand EqualsButtonCommand { get; }
    
        public ViewModel()
        {
            InputNumber = "0";
            NumberCommand = new RelayCommand<object>(AddNumberWrapper);
            DotNumberCommand = new RelayCommand<object>(AddDotNumberWrapper);
            ChangePMCommand = new RelayCommand<object>(ChangePMWrapper);
            CalculationCommand = new RelayCommand<object>(CalculationWrapper);
            BasketCommand = new RelayCommand<object>(BasketWrapper);
            AngleChangeCommand = new RelayCommand(ChangeAngleContent);
            AngleChange = "DEG";
            ToggleTrigonometryPopupCommand = new RelayCommand(ToggleTrigonometryPopup);
            ToggleFunctionPopupCommand = new RelayCommand(ToggleFunctionPopup);
            DelCommand = new RelayCommand(DelNumberWrapper);
            SurdCommand = new RelayCommand<object>(SurdNumberWrapper);
            SpecialNumberCommand = new RelayCommand<object>(SpecialNumberWrapper);
            CECommand = new RelayCommand(CEWrapper);
            EqualsButtonCommand = new RelayCommand(EqualsButtonWrapper);
            MemoryCommand = new RelayCommand<object>(MemoryWrapper); 
            UpdateFontSizes();
        }

        private void ToggleTrigonometryPopup()
        {
            IsTrigonometryPopupOpen = !IsTrigonometryPopupOpen;
        }

        private void ToggleFunctionPopup()
        {
            IsFunctionPopupOpen = !IsFunctionPopupOpen;
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
            set 
            {
                SetProperty(ref inputNumber, value);
                ButtonCE = formatHelper.ButtonCeUpdate(inputNumber);
            }
        }

        public string CurrentExpression
        {
            get { return currentExpression; }
            set 
            { 
                SetProperty(ref currentExpression, value); 
            }
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

        private void BasketWrapper(object parameter)
        {
            operatorButton.Basket(parameter, this);
        }

        private void DelNumberWrapper()
        {
            operatorButton.DelNumber(this);
        }

        private void SurdNumberWrapper(object parameter)
        {
            operatorButton.SurdNumber(parameter ,this);
        }

        private void SpecialNumberWrapper(object parameter)
        {
            operatorButton.SpecialNumber(parameter ,this);
        }

        private void MemoryWrapper(object parameter)
        {
            memoryButton.MemoryController(parameter, this);
        }

        private void CEWrapper()
        {
            numPad.ButtonCE(_buttonCE, this);
        }

        private void EqualsButtonWrapper()
        {
            numPad.EqualsButton(this);
        }

        private void ChangeAngleContent()
        {
            if (AngleChange == "DEG")
            {
                AngleChange = "RAD";
                degres = "angle";
            }
            else if (AngleChange == "RAD")
            {
                AngleChange = "GRAD";
                degres = "angle * " + Math.PI + " / 200";
            }
            else
            {
                AngleChange = "DEG";
                degres = "angle * " + Math.PI + " / 180";
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
                    if(viewModel.resultNumber == viewModel.inputNumber)
                    {
                        viewModel.InputNumber = formatHelper.FormatNumberWithCommas(str);
                    }
                    else
                    {
                        str = viewModel.inputNumber + str;
                        viewModel.InputNumber = formatHelper.FormatNumberWithCommas(str);
                    }
                    viewModel.UpdateFontSizes();
                }
            }

        }//숫자패드

        internal void AddDotNumber(object parameter, ViewModel viewModel)
        {
            string str = viewModel.inputNumber + Convert.ToString(parameter);

            if (viewModel._isInt == true && !viewModel._isExp)
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
            if (viewModel.currentExpression.Contains("="))
            {
                viewModel.CurrentExpression = "";
            }
            if (viewModel.currentExpression.Trim().EndsWith(")"))
            {
                viewModel.CurrentExpression += Convert.ToString(parameter);
            }
            else if (Convert.ToString(viewModel.currentExpression).Trim().EndsWith("d"))
            {
                if(viewModel.inputNumber != "0" && viewModel.inputNumber != viewModel.resultNumber)
                {
                    string str = viewModel.InputNumber;
                    if (str.EndsWith("."))
                    {
                        str = str.Replace(".", "");
                    }
                    viewModel.currentExpression += str;
                    if (!viewModel._isBreaket)
                    {
                        viewModel.InputNumber = Convert.ToString(formatHelper.FormatNumberWithCommas(calculration.MathResult(formatHelper.FormatNumberDelCommas(viewModel.currentExpression))));
                    }
                    viewModel.resultNumber = viewModel.inputNumber;
                    viewModel.CurrentExpression += " " + Convert.ToString(parameter) + " ";
                    viewModel.UpdateFontSizes();
                    viewModel._isInt = true;
                }
                else
                {
                    viewModel.CurrentExpression = viewModel.currentExpression.Replace("mod", Convert.ToString(parameter));
                }
            }
            else
            {
                string str = viewModel.InputNumber;
                if (str.EndsWith("."))
                {
                    str = str.Replace(".", "");
                }
                if (viewModel.IsFE)
                {
                    viewModel.currentExpression += double.Parse(str).ToString("E2");
                }
                else
                    viewModel.currentExpression += str;
                if (!viewModel._isBreaket)
                {
                    viewModel.InputNumber = Convert.ToString(formatHelper.FormatNumberWithCommas(calculration.MathResult(formatHelper.FormatNumberDelCommas(viewModel.currentExpression))));
                }
                viewModel.resultNumber = viewModel.inputNumber;
                viewModel.CurrentExpression += " " + Convert.ToString(parameter) + " ";
                viewModel.UpdateFontSizes();
                viewModel._isInt = true;
            }

        }//연산

        internal void ButtonCE(string buttonText ,ViewModel viewModel)
        {
            if(buttonText == "C")
            {
                viewModel.CurrentExpression = "";
            }
            viewModel.InputNumber = "0";
            viewModel.UpdateFontSizes();
            viewModel._isInt = true;
            viewModel._isBreaket = false;
        }

        internal void EqualsButton(ViewModel viewModel)
        {
            if(viewModel.currentExpression != null && viewModel.currentExpression != "" && !viewModel.currentExpression.Contains("="))
            {
                if(!viewModel.currentExpression.Trim().EndsWith(")"))
                    viewModel.CurrentExpression += " " + viewModel.inputNumber;
                viewModel.InputNumber = Convert.ToString(formatHelper.FormatNumberWithCommas(calculration.MathResult(formatHelper.FormatNumberDelCommas(viewModel.currentExpression))));
                viewModel.CurrentExpression += " = ";
                viewModel.resultNumber = viewModel.inputNumber;
                viewModel.UpdateFontSizes();
                viewModel._isInt = true;
                viewModel._isBreaket = false;
            }
            //
        }




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
            if (viewModel.currentExpression.Length < 20) return 18;
            if (viewModel.currentExpression.Length < 30) return 15;
            if (viewModel.currentExpression.Length < 40) return 12;
            if (viewModel.currentExpression.Length < 50) return 9;
            if (viewModel.currentExpression.Length < 60) return 5;

            return 12;
        } // 텍스트 사이즈 자동조절
    }

    class OperatorButton
    {
        FormatHelper formatHelper = new FormatHelper();
        Calculration calculration = new Calculration();

        internal void Basket(object parameter, ViewModel viewModel)
        {
            if(Convert.ToString(parameter) == "(" && viewModel._isBreaket == false)
            {
               if(viewModel.resultNumber == viewModel.inputNumber || viewModel.inputNumber == "0" || viewModel.inputNumber == "1")
                {
                    if (!viewModel.currentExpression.EndsWith("("))
                    {
                        viewModel.CurrentExpression += " ( ";
                    }
                }
                else
                {
                    viewModel.CurrentExpression += viewModel.inputNumber + " × ( ";
                    viewModel.resultNumber = viewModel.inputNumber; 
                }
               viewModel._isBreaket = true;
            }
            else
            {
                if (viewModel._isBreaket)
                {
                    if(viewModel.currentExpression.Trim().EndsWith(")"))
                        viewModel.CurrentExpression += " ) ";
                    else
                        viewModel.CurrentExpression += viewModel.inputNumber + " ) ";
                    viewModel.InputNumber = Convert.ToString(formatHelper.FormatNumberWithCommas(calculration.MathResult(formatHelper.FormatNumberDelCommas((viewModel.currentExpression)))));
                    viewModel.resultNumber = viewModel.inputNumber;
                }
                viewModel._isBreaket = false;
            }
            //calculration.Basket(parameter.ToString, viewModel);
            //여기서 수식 저장 및 결과 반환
        }
        
        internal void DelNumber(ViewModel viewModel)
        {
            viewModel.InputNumber = calculration.DelNumber(viewModel.inputNumber);
        }//뒷글자 제거

        internal void SurdNumber(object parameter, ViewModel viewModel)
        {
            viewModel.InputNumber = calculration.MathResult(Convert.ToString(parameter));
        }

        internal void SpecialNumber(object parameter ,ViewModel viewModel)
        {
            bool isOperator = false;
            bool isExp = false;

            if (viewModel.currentExpression.Contains("="))
            {
                viewModel.currentExpression = "";
            }

            if (Convert.ToString(parameter) == "Fact")
            {
                viewModel.CurrentExpression += " Fact(" + viewModel.inputNumber + ") ";
                isOperator = true;
            }
            else if (Convert.ToString(parameter) == "abs")
            {
                viewModel.CurrentExpression += " abs(" + viewModel.inputNumber + ") ";
                isOperator = true;
            }
            else if (Convert.ToString(parameter) == "mod" && !Convert.ToString(viewModel.currentExpression).Trim().EndsWith("d"))
            {
                viewModel.CurrentExpression += viewModel.inputNumber + " mod ";
                viewModel.resultNumber = viewModel.inputNumber;
            }
            else if (Convert.ToString(parameter) == "x²")
            {
                viewModel.CurrentExpression += " sqr(" + viewModel.inputNumber + ") ";
                isOperator = true;
            }
            else if (Convert.ToString(parameter) == "x³")
            {
                viewModel.CurrentExpression += " cube(" + viewModel.inputNumber + ") ";
                isOperator = true;
            }
            else if (Convert.ToString(parameter) == "e^x")
            {
                if (!viewModel._isBreaket)
                    isOperator = true;
                else
                    viewModel.resultNumber = viewModel.inputNumber;
                viewModel.CurrentExpression += " Pow(" + Math.E + " , " + viewModel.inputNumber + ") ";
            }
            else if (Convert.ToString(parameter) == "10^x")
            {
                if (!viewModel._isBreaket)
                    isOperator = true;
                else
                    viewModel.resultNumber = viewModel.inputNumber;
                viewModel.CurrentExpression += " Pow(" + 10 + " , " + viewModel.inputNumber + ") ";
            }
            else if (Convert.ToString(parameter) == "log_y(x)")
            {
                viewModel.CurrentExpression += viewModel.inputNumber + " log_base ";
                viewModel.resultNumber = viewModel.inputNumber;
                viewModel._isInt = true;
            }
            else if (Convert.ToString(parameter) == "y√x")
            {
                viewModel.CurrentExpression += viewModel.inputNumber + " yroot ";
                viewModel.resultNumber = viewModel.inputNumber;
                viewModel._isInt = true;
            }
            else if (Convert.ToString(parameter) == "2√x")
            {
                viewModel.CurrentExpression += " root(" + viewModel.inputNumber + ") ";
                isOperator = true;
            }
            else if (Convert.ToString(parameter) == "3√x")
            {
                viewModel.CurrentExpression += " cuberoot(" + viewModel.inputNumber + ") ";
                isOperator = true;
            }
            else if (Convert.ToString(parameter) == "ln")
            {
                viewModel.CurrentExpression += " ln(" + viewModel.inputNumber + ") ";
                isOperator = true;
            }
            else if (Convert.ToString(parameter) == "exp")
            {

                if(!isExp)
                {
                    viewModel.CurrentExpression += viewModel.inputNumber + " exp ";
                    viewModel.resultNumber = viewModel.inputNumber;
                    viewModel._isExp = true;
                }
            }
            else if (Convert.ToString(parameter) == " x^y")
            {
                viewModel.CurrentExpression += viewModel.inputNumber + " pow ";
                viewModel.resultNumber = viewModel.inputNumber;
                viewModel._isInt = true;
            }
            else if (Convert.ToString(parameter) == "log")
            {
                viewModel.CurrentExpression += " log(" + viewModel.inputNumber + ") ";
                isOperator = true;
            }
            else if (Convert.ToString(parameter) == "1/x")
            {
                viewModel.CurrentExpression += " 1 / (" + viewModel.inputNumber + ") ";
                isOperator = true;
            }
            else if (Convert.ToString(parameter) == "2^x")
            {
                viewModel.CurrentExpression += " 2 pow " + viewModel.inputNumber + " ";
                isOperator = true;
            }
            //여기까지 밑의 함수

            else if (Convert.ToString(parameter) == "sin")
            {
                viewModel.CurrentExpression += " Sin(" + viewModel.degres.Replace("angle", viewModel.inputNumber) + ") ";
                isOperator = true;
            }
            else if (Convert.ToString(parameter) == "cos")
            {
                viewModel.CurrentExpression += " Cos(" + viewModel.degres.Replace("angle", viewModel.inputNumber) + ") ";
                isOperator = true;
            }
            else if (Convert.ToString(parameter) == "tan")
            {
                viewModel.CurrentExpression += " Tan(" + viewModel.degres.Replace("angle", viewModel.inputNumber) + ") ";
                isOperator = true;
            }
            else if (Convert.ToString(parameter) == "sec")
            {
                viewModel.CurrentExpression += " Sec(" + viewModel.degres.Replace("angle", viewModel.inputNumber) + ") ";
                isOperator = true;
            }
            else if (Convert.ToString(parameter) == "csc")
            {
                viewModel.CurrentExpression += " Csc(" + viewModel.degres.Replace("angle", viewModel.inputNumber) + ") ";
                isOperator = true;
            }
            else if (Convert.ToString(parameter) == "cot")
            {
                viewModel.CurrentExpression += " Cot(" + viewModel.degres.Replace("angle", viewModel.inputNumber) + ") ";
                isOperator = true;
            }
            //삼각법 all off

            else if (Convert.ToString(parameter) == "sin^-1" && double.Parse(viewModel.inputNumber) <= 1 && double.Parse(viewModel.inputNumber) >= -1)
            {
                viewModel.CurrentExpression += " Asin(" + viewModel.inputNumber + ") ";
                isOperator = true;
            }
            else if (Convert.ToString(parameter) == "cos^-1" && double.Parse(viewModel.inputNumber) <= 1 && double.Parse(viewModel.inputNumber) >= -1)
            {
                viewModel.CurrentExpression += " Acos(" + viewModel.inputNumber + ") ";
                isOperator = true;
            }
            else if (Convert.ToString(parameter) == "tan^-1" && double.Parse(viewModel.inputNumber) <= 1 && double.Parse(viewModel.inputNumber) >= -1)
            {
                viewModel.CurrentExpression += " Atan(" + viewModel.inputNumber + ") ";
                isOperator = true;
            }
            else if (Convert.ToString(parameter) == "sec^-1" && double.Parse(viewModel.inputNumber) <= 1 && double.Parse(viewModel.inputNumber) >= -1)
            {
                viewModel.CurrentExpression += " Asec(" + viewModel.inputNumber + ") ";
                isOperator = true;
            }
            else if (Convert.ToString(parameter) == "csc^-1" && double.Parse(viewModel.inputNumber) <= 1 && double.Parse(viewModel.inputNumber) >= -1)
            {
                viewModel.CurrentExpression += " Acsc(" + viewModel.inputNumber + ") ";
                isOperator = true;
            }
            else if (Convert.ToString(parameter) == "cot^-1" && double.Parse(viewModel.inputNumber) <= 1 && double.Parse(viewModel.inputNumber) >= -1)
            {
                viewModel.CurrentExpression += " Acot(" + viewModel.inputNumber + ") ";
                isOperator = true;
            }
            //삼각법 2nd on

            else if (Convert.ToString(parameter) == "sinh")
            {
                viewModel.CurrentExpression += " Sinh(" + viewModel.inputNumber + ") ";
                isOperator = true;
            }
            else if (Convert.ToString(parameter) == "cosh")
            {   
                viewModel.CurrentExpression += " Cosh(" + viewModel.inputNumber + ") ";
                isOperator = true;
            }
            else if (Convert.ToString(parameter) == "tanh")
            {
                viewModel.CurrentExpression += " Tanh(" + viewModel.inputNumber + ") ";
                isOperator = true;
            }
            else if (Convert.ToString(parameter) == "sech")
            {
                viewModel.CurrentExpression += " Sech(" + viewModel.inputNumber + ") ";
                isOperator = true;
            }
            else if (Convert.ToString(parameter) == "csch")
            {
                viewModel.CurrentExpression += " Csch(" + viewModel.inputNumber + ") ";
                isOperator = true;
            }
            else if (Convert.ToString(parameter) == "coth")
            {
                viewModel.CurrentExpression += " Coth(" + viewModel.inputNumber + ") ";
                isOperator = true;
            }
            //삼각법 hyp on

            else if (Convert.ToString(parameter) == "sinh^-1")
            {
                viewModel.CurrentExpression += " Asinh(" + viewModel.inputNumber + ") ";
                isOperator = true;
            }
            else if (Convert.ToString(parameter) == "cosh^-1")
            {
                viewModel.CurrentExpression += " Acosh(" + viewModel.inputNumber + ") ";
                isOperator = true;
            }
            else if (Convert.ToString(parameter) == "tanh^-1")
            {
                viewModel.CurrentExpression += " Atanh(" + viewModel.inputNumber + ") ";
                isOperator = true;
            }
            else if (Convert.ToString(parameter) == "sech^-1")
            {
                viewModel.CurrentExpression += " Asech(" + viewModel.inputNumber + ") ";
                isOperator = true;
            }
            else if (Convert.ToString(parameter) == "csch^-1")
            {
                viewModel.CurrentExpression += " Acsch(" + viewModel.inputNumber + ") ";
                isOperator = true;
            }
            else if (Convert.ToString(parameter) == "coth^-1")
            {
                viewModel.CurrentExpression += " Acoth(" + viewModel.inputNumber + ") ";
                isOperator = true;
            }
            //삼각법 all on
            //여기까지 삼각법

            else if (Convert.ToString(parameter) == "floor")
            {
                viewModel.CurrentExpression += " floor(" + viewModel.inputNumber + ") ";
                isOperator = true;
            }
            else if (Convert.ToString(parameter) == "ceil")
            {
                viewModel.CurrentExpression += " ceil(" + viewModel.inputNumber + ") ";
                isOperator = true;
            }
            else if (Convert.ToString(parameter) == "rand")
            {
                viewModel.InputNumber = Convert.ToString(formatHelper.FormatNumberWithCommas(calculration.MathResult(formatHelper.FormatNumberDelCommas("rand()"))));
                
            }
            else if (Convert.ToString(parameter) == "->dms")
            {
                viewModel.CurrentExpression += " dms(" + viewModel.inputNumber + ") ";
                isOperator = true;
            }
            else if (Convert.ToString(parameter) == "->deg")
            {
                viewModel.CurrentExpression += " degrees(" + viewModel.inputNumber + ") ";
                isOperator = true;
            }
            //여기까지 함수

            if (isOperator)
            {
                viewModel.InputNumber = Convert.ToString(formatHelper.FormatNumberWithCommas(calculration.MathResult(formatHelper.FormatNumberDelCommas(viewModel.currentExpression))));
                viewModel.resultNumber = viewModel.inputNumber;
                viewModel.UpdateFontSizes();
                viewModel._isInt = true;
                isOperator = false;
            }
        }

    }

    class MemoryButton
    {
        Memory memory = new Memory();

        internal void MemoryController(object parameter, ViewModel viewModel)
        {
            if (parameter != null) 
            {
                string type = parameter as string;
                System.Diagnostics.Debug.WriteLine(type);
                if (type == "MC") 
                {
                    memory.Clear();
                    viewModel.IsMC = false;
                    viewModel.IsMR = false;
                }
                else if (type == "MR")
                {
                    viewModel.InputNumber = memory.Return();
                }
                else if (type == "M+")
                {
                    memory.Add(viewModel.inputNumber);
                    viewModel.IsMC = true;
                    viewModel.IsMR = true;
                    viewModel.resultNumber = viewModel.inputNumber;
                }
                else if (type == "M-")
                {
                    memory.Min(viewModel.inputNumber);
                    viewModel.IsMC = true;
                    viewModel.IsMR = true;
                    viewModel.resultNumber = viewModel.inputNumber;
                }
                else if (type == "MS")
                {
                    memory.Create(viewModel.inputNumber);
                    viewModel.IsMC = true;
                    viewModel.IsMR = true;
                    viewModel.resultNumber = viewModel.inputNumber;
                }
            }
            
        }
    }
}

