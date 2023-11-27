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
            expr.EvaluateFunction += (name, args) =>
            {
                if (name == "Fact")
                {
                    int value = Convert.ToInt32(args.Parameters[0].Evaluate());
                    args.Result = Factorial(value);
                }
                if (name == "abs")
                {
                    args.Result = Math.Abs(Convert.ToDouble(args.Parameters[0].Evaluate()));
                }
                if (name == "sqr" && args.Parameters.Length == 1)
                {
                    var parameter = Convert.ToDouble(args.Parameters[0].Evaluate());
                    args.Result = Math.Pow(parameter, 2);
                }

            };
            System.Diagnostics.Debug.WriteLine(expr);
            expr.Parameters["Pi"] = Math.PI;
            object result = expr.Evaluate();

            return Convert.ToString(result);

        }//연산

        private string SignConverter(string str)
        {
            str = str.Replace("÷", "/");
            str = str.Replace("×", "*");
            str = str.Replace("mod", "%");
            

            return str;
        }

        private int Factorial(int n)
        {
            return n <= 1 ? 1 : n * Factorial(n - 1);
        }//팩토리얼

        internal string DelNumber(string inputNumber)
        {
            if(inputNumber == "0" || inputNumber.Length == 1)
            {
                return "0";
            }
            else
            {
                inputNumber = inputNumber.Substring(0, inputNumber.Length - 1);
                return inputNumber;
            }
            //최종 수정값
        }

    }
}
