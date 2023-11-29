using System.Linq;
using System.Text;
using ScientificCalculator_ver.MVVM.ViewModels;

namespace ScientificCalculator_ver.MVVM.Models
{
    internal class FormatHelper
    {
        internal string FormatNumberWithCommas(string str)
        {
            string numberString = FormatNumberDelCommas(str);
            if(double.TryParse(numberString, out double test))
            {
                bool isMin = false;

                if (numberString.StartsWith("-"))
                {
                    numberString = numberString.Replace("-", "");
                    isMin = true;
                }

                if (!str.Contains("."))
                {
                    int length = numberString.Length;
                    int commaCount = (length - 1) / 3;
                    StringBuilder formatted = new StringBuilder(length + commaCount);

                    int commaPosition = 3 - (length % 3);
                    if (commaPosition == 3)
                    {
                        commaPosition = 0;
                    }

                    for (int i = 0; i < length; i++)
                    {
                        if (i != 0 && commaPosition == 0)
                        {
                            formatted.Append(",");
                        }

                        formatted.Append(numberString[i]);

                        commaPosition = (commaPosition + 1) % 3;
                    }

                    string formattedString = formatted.ToString();

                    if (isMin)
                    {
                        formattedString = "-" + formattedString;
                    }

                    return formattedString;

                }
                else
                {
                    string[] arr = numberString.Split('.');


                    int length = arr[0].Length;
                    int commaCount = (length - 1) / 3;
                    StringBuilder formatted = new StringBuilder(length + commaCount);

                    int commaPosition = 3 - (length % 3);
                    if (commaPosition == 3)
                    {
                        commaPosition = 0;
                    }

                    for (int i = 0; i < length; i++)
                    {
                        if (i != 0 && commaPosition == 0)
                        {
                            formatted.Append(",");
                        }

                        formatted.Append(numberString[i]);

                        commaPosition = (commaPosition + 1) % 3;
                    }

                    string formattedString = formatted.ToString();

                    formattedString = formattedString + "." + arr[1];

                    if (isMin)
                    {
                        formattedString = "-" + formattedString;
                    }

                    return formattedString;
                }
            }
            return numberString;

        }//콤마추가

        internal string FormatNumberDelCommas(string str)
        {
            string[] arr = str.Split(" ");
            for (int i = 0; i < arr.Length; i++)
            {
                // arr[i]가 쉼표만 포함하고 있지 않다면 쉼표 제거
                if (!arr[i].All(c => c == ','))
                {
                    arr[i] = arr[i].Replace(",", "");
                }
            }
            str = string.Join(" ", arr);

            return str;
        }//콤마제거

        internal bool CountNumber(string str)
        {
            bool isEndDot = false;
            
            str = FormatNumberDelCommas(str);
            str = str.Replace(".", "");
            

            if(str.Length >= 20)
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }//자릿수제한

        internal string ButtonCeUpdate(string inputNumber)
        {
            if (inputNumber != "0")
                return "CE";
            else
                return "C";
        }//C버튼 설정


    }
}
