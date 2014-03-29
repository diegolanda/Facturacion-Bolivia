using FacturacionBolivia.Utils;

namespace FacturacionBolivia.Crypto
{
    public class Verhoeff
    {
        #region Verhoeff Tables
        static int[,] _tableD = new int[,]{
			{0,1,2,3,4,5,6,7,8,9},
			{1,2,3,4,0,6,7,8,9,5},
			{2,3,4,0,1,7,8,9,5,6},
			{3,4,0,1,2,8,9,5,6,7},
			{4,0,1,2,3,9,5,6,7,8},
			{5,9,8,7,6,0,4,3,2,1},
			{6,5,9,8,7,1,0,4,3,2},
			{7,6,5,9,8,2,1,0,4,3},
			{8,7,6,5,9,3,2,1,0,4},
			{9,8,7,6,5,4,3,2,1,0}
		};
        static int[,] _tableP = new int[,]{
			{0,1,2,3,4,5,6,7,8,9},
			{1,5,7,6,2,8,3,0,9,4},
			{5,8,0,3,7,9,6,1,4,2},
			{8,9,1,6,0,4,3,5,2,7},
			{9,4,5,3,1,2,6,8,7,0},
			{4,2,8,6,5,7,3,9,0,1},
			{2,7,9,3,8,0,6,4,1,5},
			{7,0,4,6,9,1,3,2,5,8}
			};
        static int[] _tableInv = new int[] { 0, 4, 3, 2, 1, 5, 6, 7, 8, 9 };
        #endregion

        private static int Sum(string number)
        {
            int c = 0;
            string n = Common.Reverse(number);

            int len = n.Length;
            char[] nchar = n.ToCharArray();
            for (int i = 0; i < len; i++)
            {
                c = _tableD[c, _tableP[(i + 1) % 8, int.Parse(nchar[i].ToString())]];
            }

            return _tableInv[c];
        }
        public string AddRecursive(string number, int digits)
        {
            string temp = number;
            while (digits > 0)
            {
                temp += Sum(temp);
                digits--;
            }
            return temp;
        }
    }
}
