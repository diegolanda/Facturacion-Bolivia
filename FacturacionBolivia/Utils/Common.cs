using System;
using System.Linq;

namespace FacturacionBolivia.Utils
{
    public class Common
    {
        public void Swap(ref int num1, ref int num2)
        {
            int temp = num2;
            num2 = num1;
            num1 = temp;
        }

        public static string Reverse(string message)
        {
            message.Reverse();
            char[] str = message.ToCharArray();
            Array.Reverse(str);
            return new string(str);
        }
    }
}
