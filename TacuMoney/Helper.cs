using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TacuMoney
{
    public static class Helper
    {
        static public decimal MakeDouble(this string money)
        {
            money = money.Replace("$", "");

            money = money.Replace("(", "-");
            money = money.Replace(")", "");
            
            return System.Convert.ToDecimal(money);
        }
    }
}
