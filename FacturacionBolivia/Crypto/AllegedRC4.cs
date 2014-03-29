using FacturacionBolivia.Utils;

namespace FacturacionBolivia.Crypto
{
    public static class AllegedRC4
    {
        private static Common _utils = new Common();

        private const int buffer = 256;
        
        public static string Apply(string message, string keyRC4)
        {
            int[] state = new int[buffer];
            int x = 0;
            int y = 0;
            int index1 = 0;
            int index2 = 0;
            int nmen = 0;
            int i = 0;
            string cyfered = string.Empty;

            for (i = 0; i < buffer; i++)
            {
                state[i] = i;
            }

            int keyLength = keyRC4.Length;
            int messageLength = message.Length;

            for (i = 0; i < buffer; i++)
            {
                index2 = (((int)keyRC4[index1]) + state[i] + index2) % buffer;
                _utils.Swap(ref state[index2], ref state[i]);
                index1 = (index1 + 1) % keyLength;
            }

            string cadtemp = string.Empty;

            for (i = 0; i < messageLength; i++)
            {
                x = (x + 1) % buffer;
                y = (state[x] + y) % buffer;
                _utils.Swap(ref state[y], ref state[x]);

                nmen = (int)message[i] ^ state[(state[x] + state[y]) % buffer];
                cadtemp = "0" + BaseConvert.Convert(nmen, 16);
                cyfered += cadtemp.Substring(cadtemp.Length - 2, 2);
            }

            return cyfered;
        }
    }
}
