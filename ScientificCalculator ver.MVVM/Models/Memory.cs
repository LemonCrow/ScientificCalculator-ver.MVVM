using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScientificCalculator_ver.MVVM.ViewModels;

namespace ScientificCalculator_ver.MVVM.Models
{
    internal class Memory
    {
        private string? num;
        
        internal void Clear()
        {
            num = null;
        }

        internal void Create(string str)
        {
            num = str;
        }

        internal void Add(string str)
        {
            if(num == null)
            {
                num = "0";
            }
            else
            {
                if (!str.Contains(".") && !num.Contains("."))
                {
                    num = Convert.ToString(int.Parse(num) + int.Parse(str));
                }
                else
                {
                    num = Convert.ToString(double.Parse(num) + double.Parse(str));
                }
            }
        }

        internal void Min(string str)
        {
            if(num == null)
            {
                num = "0";
            }
            else
            {
                if (!str.Contains(".") && !num.Contains("."))
                {
                    num = Convert.ToString(int.Parse(num) - int.Parse(str));
                }
                else
                {
                    num = Convert.ToString(double.Parse(num) - double.Parse(str));
                }
            }
        }

        internal string Return()
        {
            if (num != null)
                return num;
            else
                return null;
        }
    }
}
