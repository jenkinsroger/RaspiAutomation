using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace HomeAutomation
{
    public class Apa102
    {
        List<Color> pixelColors = new List<Color>();
        public static SPIclass spi = MainPage.spi;

        public static bool CyclonRun = false;
        public static bool ColorFadeRun = false;
        public static bool TwinkleRun = false;
        public static bool fadeRun = false;
        public static bool ColorShiftRun = false;


        public void AllOneColor(Color Color)
        {
            pixelColors = new List<Color>();

            for (int i = 0; i < spi.PixelCount; i++) 
            {
                pixelColors.Add(Color.FromArgb(255, Color.R,Color.G,Color.B));
            }

            SPIclass.SendPixels(pixelColors);
        }

        public void FadeLedString(Color color)
        {
            Task.Run(() =>
            {
                int Brightness = 1;

                fadeRun = true;

                while (fadeRun)
                {

                    while (Brightness < 126)
                    {
                        pixelColors = new List<Color>();

                        for (int i = 0; i < spi.PixelCount; i++)
                        {
                            pixelColors.Add(Color.FromArgb((byte)Brightness, color.R, color.G, color.B));
                        }

                        SPIclass.SendPixels(pixelColors);
                        Brightness += 5;
                        Task.Delay(30).Wait();
                    }

                    while (Brightness > 1)
                    {
                        pixelColors = new List<Color>();

                        for (int i = 0; i < spi.PixelCount; i++)
                        {
                            pixelColors.Add(Color.FromArgb((byte)Brightness, color.R, color.G, color.B));
                        }

                        SPIclass.SendPixels(pixelColors);
                        Brightness -= 5;
                        Task.Delay(30).Wait();
                    }

                   
                }
            });
        }

        public void Rainbow_Click()
        {
            List<Color> pixelColors = new List<Color>();

            for (int i = 0; i < spi.PixelCount / 6; i++)
            {
                pixelColors.Add(Color.FromArgb(60, 255, 0, 0));     // Red
                pixelColors.Add(Color.FromArgb(60, 0, 255, 0));     // Green
                pixelColors.Add(Color.FromArgb(60, 0, 0, 255));     // Blue
                pixelColors.Add(Color.FromArgb(60, 255, 255, 0));     // Yellow
                pixelColors.Add(Color.FromArgb(60, 255, 0, 255));     // pink
                pixelColors.Add(Color.FromArgb(60, 0, 255, 255));     // cyan

            }

            SPIclass.SendPixels(pixelColors);
        }

        public void Twinkle(Color color)
        {
            TwinkleRun = true;
            Task.Run(() =>
            {
                while(TwinkleRun)
                {
                    Random Brightness = new Random();
                    pixelColors = new List<Color>();

                    for (int i = 0; i < spi.PixelCount; i++)
                    {
                        pixelColors.Add(Color.FromArgb((byte)Brightness.Next(0,255), color.R, color.G, color.B));
                    }

                    SPIclass.SendPixels(pixelColors);
                    Task.Delay(50).Wait();
                }

            });
        }

        public void ColorsFade()
        {
            int Brightness = 60;
            int CurrentCycle = 0;
            int NumberOfCycles = 10;

            int red = 255;
            int green = 0;
            int blue = 0;

            int changeamount = 5;

            Color StartColor = Color.FromArgb((byte)Brightness, (byte)red, (byte)green, (byte)blue);

            ColorFadeRun = true;

            Task.Run(() =>
            {
                while (ColorFadeRun)
                {
                    pixelColors = new List<Color>();

                    for (int i = 0; i < spi.PixelCount; i++)
                    {
                        pixelColors.Add(Color.FromArgb((byte)Brightness, (byte)red, (byte)green, (byte)blue));
                    }
                    SPIclass.SendPixels(pixelColors);

                    if (red == 255 && green < 255)
                    {
                        if (green == 255) break;
                        green = green + changeamount;
                    }
                    else if(red >0 && green ==255)
                    {
                        if (red == 0) break;
                        red = red - changeamount;
                    }
                    else if (green == 255 && blue <255)
                    {
                        if (blue == 255) break;
                        blue = blue + changeamount;
                    }
                    else if(green > 0 && blue == 255)
                    {
                        if (green == 0) break;
                        green = green - changeamount;
                    }
                    else if (red < 255 && blue == 255)
                    {
                        if (red == 255) break;
                        red = red + changeamount;
                    }
                    else if (red == 255 && blue > 0)
                    {
                        if (blue == 0) break;
                        blue = blue - changeamount;
                    }

                    Task.Delay(30).Wait();
                }
            });


        }

        public void cyclon()
        {
            int Brightness = 255;
            int red = 0;
            int green = 0;
            int blue = 0;

            int top = 3;
            int center = 2;
            int botton = 1;

            int location = 0;

            CyclonRun = true;
            Task.Run(() =>
            {
                while (CyclonRun)
                {
                    while ((location + top) < spi.PixelCount)
                    {
                        if ((location + top) == (spi.PixelCount - 1)) break;

                        pixelColors = new List<Color>();

                        for (int i = 0; i < spi.PixelCount; i++)
                        {
                            pixelColors.Add(Color.FromArgb((byte)Brightness, (byte)red, (byte)green, (byte)blue));
                        }

                        pixelColors[top + location] = Color.FromArgb((byte)Brightness, 255, (byte)green, (byte)blue);
                        pixelColors[center + location] = Color.FromArgb((byte)Brightness, 255, (byte)green, (byte)blue);
                        pixelColors[botton + location] = Color.FromArgb((byte)Brightness, 255, (byte)green, (byte)blue);

                        SPIclass.SendPixels(pixelColors);
                        Task.Delay(30).Wait();
                        location++;
                    }

                    while ((location + botton) < spi.PixelCount)
                    {
                        if ((location + botton) == 0) break;

                        pixelColors = new List<Color>();

                        for (int i = 0; i < spi.PixelCount; i++)
                        {
                            pixelColors.Add(Color.FromArgb((byte)Brightness, (byte)red, (byte)green, (byte)blue));
                        }

                        pixelColors[top + location] = Color.FromArgb((byte)Brightness, 255, (byte)green, (byte)blue);
                        pixelColors[center + location] = Color.FromArgb((byte)Brightness, 255, (byte)green, (byte)blue);
                        pixelColors[botton + location] = Color.FromArgb((byte)Brightness, 255, (byte)green, (byte)blue);

                        SPIclass.SendPixels(pixelColors);
                        Task.Delay(25).Wait();
                        location--;
                    }
                }
            });
        }

        public void ColorShift()
        {
            int location = 0;
            int pixelcount = spi.PixelCount;

            int Brightness = 255;
            int red = 0;
            int green = 0;
            int blue = 0;

            int changeamount = 10;

            pixelColors = new List<Color>();

            for (int i = 0; i < spi.PixelCount; i++)
            {
                pixelColors.Add(Color.FromArgb((byte)Brightness, (byte)red, (byte)green, (byte)blue));
            }
            SPIclass.SendPixels(pixelColors);

            ColorShiftRun = true;

            Task.Run(() =>
            {
                red = 250;
                while(ColorShiftRun)
                {
                    if (location == 36) location = 0;
                    pixelColors[location] = Color.FromArgb((byte)Brightness, (byte)red, (byte)green, (byte)blue);
                    SPIclass.SendPixels(pixelColors);
                    Task.Delay(50).Wait();
                    location++;

                    while (red == 250 && green < 250)
                    {
                        if (green == 250) break;
                        green = green + changeamount;

                        if (location == 36) location = 0;
                        pixelColors[location] = Color.FromArgb((byte)Brightness, (byte)red, (byte)green, (byte)blue);
                        SPIclass.SendPixels(pixelColors);
                        Task.Delay(50).Wait();
                        location++;
                    }
                    while (red > 0 && green == 250)
                    {
                        if (red == 0) break;
                        red = red - changeamount;

                        if (location == 36) location = 0;
                        pixelColors[location] = Color.FromArgb((byte)Brightness, (byte)red, (byte)green, (byte)blue);
                        SPIclass.SendPixels(pixelColors);
                        Task.Delay(50).Wait();
                        location++;
                    }
                    while (green == 250 && blue < 250)
                    {
                        if (blue == 250) break;
                        blue = blue + changeamount;

                        if (location == 36) location = 0;
                        pixelColors[location] = Color.FromArgb((byte)Brightness, (byte)red, (byte)green, (byte)blue);
                        SPIclass.SendPixels(pixelColors);
                        Task.Delay(50).Wait();
                        location++;
                    }
                    while (green > 0 && blue == 250)
                    {
                        if (green == 0) break;
                        green = green - changeamount;

                        if (location == 36) location = 0;
                        pixelColors[location] = Color.FromArgb((byte)Brightness, (byte)red, (byte)green, (byte)blue);
                        SPIclass.SendPixels(pixelColors);
                        Task.Delay(50).Wait();
                        location++;
                    }
                    while (red < 250 && blue == 250)
                    {
                        if (red == 250) break;
                        red = red + changeamount;

                        if (location == 36) location = 0;
                        pixelColors[location] = Color.FromArgb((byte)Brightness, (byte)red, (byte)green, (byte)blue);
                        SPIclass.SendPixels(pixelColors);
                        Task.Delay(50).Wait();
                        location++;
                    }
                    while (red == 250 && blue > 0)
                    {
                        if (blue == 0) break;
                        blue = blue - changeamount;

                        if (location == 36) location = 0;
                        pixelColors[location] = Color.FromArgb((byte)Brightness, (byte)red, (byte)green, (byte)blue);
                        SPIclass.SendPixels(pixelColors);
                        Task.Delay(50).Wait();
                        location++;
                    }
                }
            });
        }

        public void NewColorFader()
        {
            int Brightness = 120;
            int CurrentCycle = 0;
            int NumberOfCycles = 10;

            double hue = 0;
            double sat = 1;
            double value = 1;

            Task.Run(() =>
            {
                //while (true)
                //{
                    pixelColors = new List<Color>();

                    for (int i = 0; i < spi.PixelCount; i++)
                    {

                        Color NewColor = LightColors.ColorFromHSV(hue, sat, value);
                        pixelColors.Add(Color.FromArgb((byte)Brightness, (byte)NewColor.R, (byte)NewColor.G, (byte)NewColor.B));

                        hue += 5;
                    }
                    SPIclass.SendPixels(pixelColors);
                    //Task.Delay(30).Wait();
                //}
            });
        }
    }
}
