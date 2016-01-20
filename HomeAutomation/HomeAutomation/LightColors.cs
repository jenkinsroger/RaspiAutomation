using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace HomeAutomation
{
    public static class LightColors
    {
        public static Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 100)) % 10;
            double f = hue / 100 - Math.Floor(hue / 100);

            value = value * 255;
            Byte v = (byte)Convert.ToInt32(value);
            Byte p = (byte)Convert.ToInt32(value * (1 - saturation));
            Byte q = (byte)Convert.ToInt32(value * (1 - f * saturation));
            Byte t = (byte)Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }

    }
}
