using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ScientificCalculator_ver.MVVM.ViewModels;
using NCalc;
using System.Windows.Navigation;
using System.Text.RegularExpressions;

namespace ScientificCalculator_ver.MVVM.Models
{
    internal class Calculration
    {

        internal string MathResult(string str)
        {
            System.Diagnostics.Debug.WriteLine(str);
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
                if (name == "cube" && args.Parameters.Length == 1)
                {
                    var parameter = Convert.ToDouble(args.Parameters[0].Evaluate());
                    args.Result = Math.Pow(parameter, 3);
                }
                if (name == "log_base")
                {
                    double y = Convert.ToDouble(args.Parameters[0].Evaluate());
                    double x = Convert.ToDouble(args.Parameters[1].Evaluate());
                    args.Result = Math.Log(y) / Math.Log(x);
                }
                if (name == "yroot")
                {
                    double x = Convert.ToDouble(args.Parameters[0].Evaluate());
                    double y = Convert.ToDouble(args.Parameters[1].Evaluate());
                    args.Result = Math.Pow(x, 1.0 / y);
                }
                if (name == "cuberoot")
                {
                    double value = Convert.ToDouble(args.Parameters[0].Evaluate());
                    args.Result = Math.Pow(value, 1.0 / 3.0);
                }
                if (name == "root")
                {
                    double value = Convert.ToDouble(args.Parameters[0].Evaluate());
                    args.Result = Math.Pow(value, 1.0 / 2.0);
                }
                if (name == "ln")
                {
                    double value = Convert.ToDouble(args.Parameters[0].Evaluate());
                    args.Result = Math.Log(value);
                }
                if (name == "exp")
                {
                    double x = Convert.ToDouble(args.Parameters[0].Evaluate());
                    double y = Convert.ToDouble(args.Parameters[1].Evaluate());
                    args.Result = x * Math.Pow(10, y);
                }
            };
            expr.Parameters["Pi"] = Math.PI;
            expr.Parameters["e"] = Math.E;

            object result = expr.Evaluate();

            return Convert.ToString(result);

        }//연산

        private string SignConverter(string str)
        {
            str = str.Replace("÷", "/");
            str = str.Replace("×", "*");
            str = str.Replace("mod", "%");


            if (str.Contains("log_base"))
            {
                string pattern = @"(\d+(\.\d+)?)\s+log_base\s+(\d+(\.\d+)?)"; 
                string replacement = "log_base($1, $3)";

                str = Regex.Replace(str, pattern, replacement);
            }

            if (str.Contains("yroot"))
            {
                string pattern = @"(\d+(\.\d+)?)\s+yroot\s+(\d+(\.\d+)?)";
                string replacement = "yroot($1, $3)";

                str = Regex.Replace(str, pattern, replacement);
            }

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
