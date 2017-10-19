using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace AntiVirus
{
    class Internet
    {
        public static string CheckPing()
        {
            using (Ping p = new Ping())
            {
                return p.Send("www.google.com").RoundtripTime.ToString() + "ms";
            }
        }

        public static string CheckInternetSpeed()
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            DateTime dt1 = DateTime.Now;
            byte[] data = wc.DownloadData("http://google.com");
            DateTime dt2 = DateTime.Now;
            var Result = Math.Round((data.Length / 1024) / (dt2 - dt1).TotalSeconds, 2);
            return Convert.ToString(Result + "Kb");
        }
    }
}
