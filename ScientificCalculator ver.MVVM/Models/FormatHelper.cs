using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ScientificCalculator_ver.MVVM.ViewModels;

namespace ScientificCalculator_ver.MVVM.Models
{
    internal class FormatHelper
    {
        internal string FormatNumberWithCommas(string str, ViewModel viewmodel)
        {
            string numberString = FormatNumberDelCommas(str);
            bool isMin = false;

            if (numberString.StartsWith("-"))
            {
                numberString =  numberString.Replace("-", "");
                isMin = true;
            }

            if (viewmodel._isInt)
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
                numberString = arr[1];

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

                formattedString = formattedString + "." + arr[0];

                if (isMin)
                {
                    formattedString = "-" + formattedString;
                }

                return formattedString;
            }

        }

        internal string FormatNumberDelCommas(string str)
        {
            return str.Replace(",", "");
        }

    }
}
