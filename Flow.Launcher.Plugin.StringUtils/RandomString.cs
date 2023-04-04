using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flow.Launcher.Plugin.StringUtils
{
    internal static class RandomString
    {
        const string allowedChars_1 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz#@$^*()-+!";
        const string allowedChars_2 = "0123456789abcdefghijklmnopqrstuvwxyz";
        const string allowedChars_3 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        private static string Generate(string allowedChars, int length)
        {
            var random = new Random();
            var chars = new char[length];

            for (int i = 0; i < length; i++)
            {
                chars[i] = allowedChars[random.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }

        public static string GenerateType1(int length)
        {
            return Generate(allowedChars_1, length);
        }

        public static string GenerateType2(int length)
        {
            return Generate(allowedChars_2, length);
        }

        public static string GenerateType3(int length)
        {
            return Generate(allowedChars_3, length);
        }
    }
}
