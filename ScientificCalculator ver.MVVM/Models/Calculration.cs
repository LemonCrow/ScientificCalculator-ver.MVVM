﻿using System;
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
                if (name == "power")
                {
                    double x = Convert.ToDouble(args.Parameters[0].Evaluate());
                    double y = Convert.ToDouble(args.Parameters[1].Evaluate());
                    args.Result = Math.Pow(x, y);
                }
                if (name == "log")
                {
                    double value = Convert.ToDouble(args.Parameters[0].Evaluate());
                    args.Result = Math.Log10(value);
                }
                if (name == "Sec")
                {
                    args.Result = 1 / Math.Cos(Convert.ToDouble(args.Parameters[0].Evaluate()));
                }
                else if (name == "Csc")
                {
                    args.Result = 1 / Math.Sin(Convert.ToDouble(args.Parameters[0].Evaluate()));
                }
                else if (name == "Cot")
                {
                    args.Result = 1 / Math.Tan(Convert.ToDouble(args.Parameters[0].Evaluate()));
                }
                if (name == "Asec")
                {
                    args.Result = Math.Acos(1 / Convert.ToDouble(args.Parameters[0].Evaluate()));
                }
                else if (name == "Acsc")
                {
                    args.Result = Math.Asin(1 / Convert.ToDouble(args.Parameters[0].Evaluate()));
                }
                else if (name == "Acot")
                {
                    double x = Convert.ToDouble(args.Parameters[0].Evaluate());
                    args.Result = Math.PI / 2 - Math.Atan(x);
                }
                if (name == "Sinh")
                {
                    double x = Convert.ToDouble(args.Parameters[0].Evaluate());
                    args.Result = (Math.Exp(x) - Math.Exp(-x)) / 2;
                }
                else if (name == "Cosh")
                {
                    double x = Convert.ToDouble(args.Parameters[0].Evaluate());
                    args.Result = (Math.Exp(x) + Math.Exp(-x)) / 2;
                }
                else if (name == "Tanh")
                {
                    double x = Convert.ToDouble(args.Parameters[0].Evaluate());
                    args.Result = (Math.Exp(x) - Math.Exp(-x)) / (Math.Exp(x) + Math.Exp(-x));
                }
                else if (name == "Sech")
                {
                    double x = Convert.ToDouble(args.Parameters[0].Evaluate());
                    args.Result = 2 / (Math.Exp(x) + Math.Exp(-x));
                }
                else if (name == "Csch")
                {
                    double x = Convert.ToDouble(args.Parameters[0].Evaluate());
                    args.Result = 2 / (Math.Exp(x) - Math.Exp(-x));
                }
                else if (name == "Coth")
                {
                    double x = Convert.ToDouble(args.Parameters[0].Evaluate());
                    args.Result = (Math.Exp(x) + Math.Exp(-x)) / (Math.Exp(x) - Math.Exp(-x));
                }
                if (name == "Asinh")
                {
                    double x = Convert.ToDouble(args.Parameters[0].Evaluate());
                    args.Result = Math.Log(x + Math.Sqrt(x * x + 1));
                }
                else if (name == "Acosh")
                {
                    double x = Convert.ToDouble(args.Parameters[0].Evaluate());
                    args.Result = Math.Log(x + Math.Sqrt(x * x - 1));
                }
                else if (name == "Atanh")
                {
                    double x = Convert.ToDouble(args.Parameters[0].Evaluate());
                    args.Result = 0.5 * Math.Log((1 + x) / (1 - x));
                }
                else if (name == "Asech")
                {
                    double x = Convert.ToDouble(args.Parameters[0].Evaluate());
                    args.Result = Math.Log(1 / x + Math.Sqrt(1 / (x * x) - 1));
                }
                else if (name == "Acsch")
                {
                    double x = Convert.ToDouble(args.Parameters[0].Evaluate());
                    args.Result = Math.Log(1 / x + Math.Sqrt(1 / (x * x) + 1));
                }
                else if (name == "Acoth")
                {
                    double x = Convert.ToDouble(args.Parameters[0].Evaluate());
                    args.Result = 0.5 * Math.Log((x + 1) / (x - 1));
                }
                if (name == "floor")
                {
                    args.Result = Math.Floor(Convert.ToDouble(args.Parameters[0].Evaluate()));
                }
                else if (name == "ceil")
                {
                    args.Result = Math.Ceiling(Convert.ToDouble(args.Parameters[0].Evaluate()));
                }
                else if (name == "rand")
                {
                    Random rnd = new Random();
                    args.Result = rnd.NextDouble();
                }
                else if (name == "dms")
                {
                    double decimalDegrees = Convert.ToDouble(args.Parameters[0].Evaluate());

                    if (decimalDegrees % 1 == 0) 
                    {
                        args.Result = decimalDegrees;
                    }
                    else
                    {
                        int degrees = (int)decimalDegrees;
                        double decimalMinutes = (decimalDegrees - degrees) * 60;
                        int minutes = (int)Math.Round(decimalMinutes);

                        args.Result = degrees + minutes / 100.0;
                    }
                }
                else if (name == "degrees")
                {
                    args.Result = Convert.ToDouble(args.Parameters[0].Evaluate()) * (180 / Math.PI);
                }

            };
            expr.Parameters["Pi"] = Math.PI;
            expr.Parameters["e"] = Math.E;
            object result;
            try
            {
                result = expr.Evaluate();
                return Convert.ToString(result);
            }
            catch (EvaluationException ex)
            {
                return "error: 계산 중 오류가 발생했습니다";
            }
            catch (OverflowException)
            {
                return "error: 계산 결과가 너무 크거나 작습니다.";
                // 계산 결과가 너무 크거나 작은 경우의 처리 로직
            }
            catch (Exception ex)
            {
                return "error: 알 수 없는 오류가 발생했습니다";
                // 다른 유형의 예외에 대한 처리 로직
            }

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

            if (str.Contains("exp"))
            {
                string pattern = @"(\d+(\.\d+)?)\s+exp\s+(\d+(\.\d+)?)";
                string replacement = "exp($1, $3)";

                str = Regex.Replace(str, pattern, replacement);
            }
            if (str.Contains("pow"))
            {
                string pattern = @"(\d+(\.\d+)?)\s+pow\s+(\d+(\.\d+)?)";
                string replacement = "power($1, $3)";

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
