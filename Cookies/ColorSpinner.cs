using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cookies
{
    class ColorSpinner
    {
        private static IEnumerable<string> _colors;
        private static int _lastColor = -1;

        static ColorSpinner()
        {
            _colors = Enum.GetNames(typeof(ConsoleColor)).Where(c => c != "Black");
        }

        public static ConsoleColor GetNext()
        {
            ++_lastColor;
            if (_lastColor >= _colors.Count())
            {
                _lastColor = 0;
            }
            return (ConsoleColor)Enum.Parse(typeof(ConsoleColor), _colors.ElementAt(_lastColor));
        }
    }
}
