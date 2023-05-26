using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Helpers
{
    public static class GenerateSerialNo
    {

        public static string serialNo(string seriNo)
        {
            string seriNumarasi = seriNo;
            string seriHarf = seriNumarasi.Substring(0, 3);
            int yeniSeriNo = int.Parse(seriNumarasi.Substring(3)) + 1;
            string yeniSeriNumarasi = seriHarf + yeniSeriNo.ToString().PadLeft(12, '0');
            return yeniSeriNumarasi;
        }
    }
}
