using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FacturacionBolivia.Singletons;

namespace UnitTest
{
    [TestClass]
    public class CodigoDeControlUnitTest
    {
        CodigoDeControl _control = CodigoDeControl.Instancia;

        [TestMethod]
        public void MontoConCentavosMayor()
        {
            string result = _control.Generar("7904006306693", "876814", "1665979", "20080519", "35958.60", @"zZ7Z]xssKqkEf_6K9uH(EcV+%x+u[Cca9T%+_$kiLjT8(zr3T9b5Fx2xG-D+_EBS");

            Assert.AreEqual(result, "7B-F3-48-A8", false);
        }

        [TestMethod]
        public void MontoConCentavosMenor()
        {
            string result = _control.Generar("6004002578983", "890986", "1678842", "20070331", "25089.49", @"hsKS\KhKG-YmMGA5sTUKN[CEYhEQFC8KS=4$Wi9*uQGh[L)e78eF4V{@JXrFVqeh");

            Assert.AreEqual(result, "EB-5E-52-76-57", false);
        }

        [TestMethod]
        public void NitCero()
        {
            string result = _control.Generar("1904008691195", "978256", "0", "20080201", "26006", @"pPgiFS%)v}@N4W3aQqqXCEHVS2[aDw_n%3)pFyU%bEB9)YXt%xNBub4@PZ4S9)ct");

            Assert.AreEqual(result, "62-12-AF-1B", false);
        }
    }
}
