
namespace FacturacionBolivia.Crypto
{
    public static class BaseConvert
    {
        static char[] dic = new char[]{
				'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 
				'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 
				'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 
				'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 
				'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 
				'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 
				'y', 'z', '+', '/' };
        public static string Convert(long number, long baseNumber)
        {
            string result = string.Empty;

            long intDiv = 1;
            long module = 0;
            while (intDiv > 0)
            {
                intDiv = number / baseNumber;
                module = number % baseNumber;
                result = dic[module] + result;
                number = intDiv;
            }
            return result;
        }
    }
}
