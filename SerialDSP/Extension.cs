using System.IO.Ports;
using System.Threading.Tasks;

namespace SerialDSP
{
    public static class StringExtension
    {
        /// <summary>
        /// In-place split to avoid heap allocate
        /// </summary>
        public static void InPlaceSingleSplit(this string s, char separator, string[] buffer)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == separator)
                {
                    buffer[0] = s.Substring(0, i);
                    buffer[1] = s.Substring(i + 1, s.Length - i - 1);
                }
            }
        }
    }
    public static class SerialPortExtension
    {
        public static async Task<string> ReadLineAsync(this SerialPort port)
        {
            return await Task.Run(() => 
            {
                lock (port)
                    return port.ReadLine();
            });
        } 
    }
}
