using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankiOnlineCacheSorter
{
    class Utils
    {
        // <returns>
        // returns 2 if symbol is 'y', 1 if is 'n' , 0 if is unknown char
        // </returns>
        public static int YNorUnknown(char symbol)
        {
            switch (char.ToLowerInvariant(symbol))
            {
                case 'y':
                    {
                        return 2;
                    }
                case 'n':
                    {
                        return 1;
                    }
                default:
                    {
                        return 0;
                    }
            }
        }
    }
}
namespace Extensions.Console
{
    static class MConsole
    {
        public static void WriteLine(string text, ConsoleColor TextColor = ConsoleColor.White)
        {
            System.Console.ForegroundColor = TextColor;
            System.Console.WriteLine(text);
            System.Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
namespace System.Mathf
{
    static class Mathf
    {
        public static float Clamp(float value, float min, float max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }
    }
}
