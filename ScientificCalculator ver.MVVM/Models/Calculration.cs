using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ScientificCalculator_ver.MVVM.ViewModels;
using NCalc;
using System.Windows.Navigation;

namespace ScientificCalculator_ver.MVVM.Models
{
    internal class Calculration
    {

        internal string MathResult(string str)
        {
            System.Diagnostics.Debug.WriteLine(SignConverter(str));
            Expression expr = new Expression(SignConverter(str));
            object result = expr.Evaluate();

            return Convert.ToString(result);

        }//연산

        private string SignConverter(string str)
        {
            str = str.Replace("÷", "/");
            str = str.Replace("×", "*");

            return str;
        }

    }
}
