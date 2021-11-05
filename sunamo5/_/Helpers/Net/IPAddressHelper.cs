using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace sunamo.Helpers
{
    public class IPAddressHelper
    {
        /// <summary>
        /// Vrátí null pokud cokoliv nebude sedět
        /// </summary>
        /// <param name="ip2"></param>
        public static byte[] GetIPAddressInArray(string ip2)
        {
            byte[] ip = null;
            var ips = SH.Split(ip2, ".");
            if (ips.Length() == 4)
            {
                ip = new byte[4];
                for (int i = 0; i < 4; i++)
                {
                    byte b = 0;
                    if (!byte.TryParse(ips[i], out b))
                    {
                        return null;
                    }
                    ip[i] = b;
                }
            }
            return ip;
        }

        /// <summary>
        /// Find with everyline and insert
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <returns></returns>
        public static byte[] GetIPAddressInArray(object httpRequest)
        {
            return null;
        }

        /// <summary>
        /// Find with everyline and insert
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool? IsIpAddress(string ip)
        {
            return false;
        }
    }
}

// Must import System.Web, not creating this class
//public class HttpRequest
//{

//}