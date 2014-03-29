using FacturacionBolivia.Crypto;
using System;

namespace FacturacionBolivia.Singletons
{
    public class CodigoDeControl
    {
        const int Version7 = 2;

        #region Singleton
        private static CodigoDeControl _instancia;
        private static int _version = CodigoDeControl.Version7;
        public static CodigoDeControl Instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new CodigoDeControl();

                return _instancia;
            }
        }
        #endregion

        #region Private Properties
        private Verhoeff _verhoeff;
        #endregion

        private CodigoDeControl()
        {
            _verhoeff = new Verhoeff();
        }

        public string Generar(string autorizacion, string numero, string nitci, string fecha, string monto, string llave)
        {
            CorregirDatos(ref monto);

            string suma = PasoUno(ref numero, ref nitci, ref fecha, ref monto);

            string digitos;
            int[] sumaDigitos;
            int x;
            PasoDos(ref autorizacion, ref numero, ref nitci, ref fecha, ref monto, llave, suma, out digitos, out sumaDigitos, out x);
            
            string arc4 = PasoTres(autorizacion, numero, nitci, fecha, monto, llave, digitos);
            
            long sumaTotal;
            long[] sumas;
            x = PasoCuatro(x, arc4, out sumaTotal, out sumas);
            
            string codigoDeControl = PasoCinco(llave, digitos, sumaDigitos, sumaTotal, sumas);
            return codigoDeControl;
        }

        private static void CorregirDatos(ref string monto)
        {
            var montoTest = (int)Math.Round(double.Parse(monto));
            monto = montoTest.ToString();
        }

        #region Pasos Impuestos
        private string PasoUno(ref string numero, ref string nitci, ref string fecha, ref string monto)
        {
            numero = _verhoeff.AddRecursive(numero, _version);
            nitci = _verhoeff.AddRecursive(nitci, _version);
            fecha = _verhoeff.AddRecursive(fecha, _version);
            monto = _verhoeff.AddRecursive(monto, _version);

            long longSum = long.Parse(numero) + long.Parse(nitci) + long.Parse(fecha) + long.Parse(monto);

            string suma = longSum.ToString();
            suma = _verhoeff.AddRecursive(suma, 5);
            return suma;
        }
        private static void PasoDos(ref string autorizacion, ref string numero, ref string nitci, ref string fecha, ref string monto, string llave, string suma, out string digitos, out int[] digitossum, out int x)
        {
            digitos = "" + suma.Substring(suma.Length - 5, 5);
            digitossum = new int[] { 0, 0, 0, 0, 0 };
            string[] cadenas = new string[] { "", "", "", "", "" };
            int inicio = 0;
            x = 0;
            foreach (char d in digitos.ToCharArray())
            {
                digitossum[x] = int.Parse(d.ToString()) + 1;
                cadenas[x] = llave.Substring(inicio, int.Parse(d.ToString()) + 1);
                inicio += int.Parse(d.ToString()) + 1;
                x++;
            }
            autorizacion += cadenas[0];
            numero += cadenas[1];
            nitci += cadenas[2];
            fecha += cadenas[3];
            monto += cadenas[4];
        }

        private static string PasoTres(string autorizacion, string numero, string nitci, string fecha, string monto, string llave, string digitos)
        {
            string arc4 = AllegedRC4.Apply(autorizacion + numero + nitci + fecha + monto, llave + digitos);
            return arc4;
        }

        private static int PasoCuatro(int x, string arc4, out long sumaTotal, out long[] sumas)
        {
            sumaTotal = 0;
            sumas = new long[] { 0, 0, 0, 0, 0 };
            int strlen_arc4 = arc4.Length;
            for (int i = 0; i < strlen_arc4; i++)
            {
                x = (int)arc4[i];
                sumas[i % 5] += x;
                sumaTotal += x;
            }
            return x;
        }

        private static string PasoCinco(string llave, string digitos, int[] sumaDigitos, long suma_total, long[] sumas)
        {
            long total = 0;
            for (int i = 0; i < sumas.Length; i++)
            {
                total += suma_total * sumas[i] / sumaDigitos[i];
            }

            string mensaje = BaseConvert.Convert(total, 64);
            string codigo = AllegedRC4.Apply(mensaje, llave + digitos).Insert(2, "-").Insert(5, "-").Insert(8, "-");
            if (codigo.Length > 11)
            {
                codigo = codigo.Insert(11, "-");
            }
            return codigo;
        }
        #endregion
    }
}
