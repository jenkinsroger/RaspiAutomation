using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices;
using Windows.Devices.Enumeration;
using Windows.Devices.Spi;
using Windows.UI;

namespace HomeAutomation
{
    public class SPIclass
    {
        public const string SpiControllerName = "SPI0";
        public static SpiDevice spiDevice;
        public int PixelCount { get; set; }
        public const int SpiChipSelectLine = 0;
        public static byte[] startFrame = { 0, 0, 0, 0 };
        public static byte[] endFrame;

        public readonly SpiConnectionSettings settings = new SpiConnectionSettings(SpiChipSelectLine)
        {
            // SPI clock speed in Hz.  Super brief testing worked fine anywhere as low as 40khz (below about 200khz was noticeably slow), all the way
            // up to 16mhz (above 16mhz started to get corrupted data towards the end of the strip).  10mhz is probably a good baseline value to use
            // unless you run in to problems.  :)
            ClockFrequency = 10000000,
            // APA102/DotStar uses SPI mode 3, CPOL = 1 (clock is high when inactive), CPHA = 1 (data is valid on the clock's trailing edge)
            Mode = SpiMode.Mode0,
            DataBitLength = 8
        };

        public async Task<SpiDevice> getSpiDevice()
        {
            string spiSelector = SpiDevice.GetDeviceSelector(SpiControllerName);
            DeviceInformationCollection devicesInfo = await DeviceInformation.FindAllAsync(spiSelector);

            return await SpiDevice.FromIdAsync(devicesInfo[0].Id, this.settings);
        }

        public async Task Begin()
        {
            spiDevice = await getSpiDevice();

        }

        public async void StartSPI()
        {
            //spiDevice = await getSpiDevice();
            await Begin();

            PixelCount = 36;

            int endFrameSize = (PixelCount + 14) / 16;

            // By initializing an int array of that specific length, it gets initialized with ints of default value (0).  :)
            endFrame = new byte[endFrameSize];
        }

        public static void SendPixels(List<Color> pixels)
        {
            List<byte> spiDataBytes = new List<byte>();
            spiDataBytes.AddRange(startFrame);

            foreach (var pixel in pixels)
            {
                // Global brightness.  Not implemented currently.  0xE0 (binary 11100000) specifies the beginning of the pixel's
                // color data.  0x1F (binary 00011111) specifies the global brightness.  If you want to actually use this functionality
                // comment out this line and uncomment the next one.  Then the pixel's RGB value will get scaled based on the alpha
                // channel value from the Color.
                //spiDataBytes.Add(0xE0 | 0x1F);
                spiDataBytes.Add((byte)(0xE0 | (byte)(pixel.A >> 3)));

                // APA102/DotStar leds take the color data in Blue, Green, Red order.  Weirdly, according to the spec these are supposed
                // to take a 0-255 value for R/G/B.  However, on the ones I have they only seem to take 0-126.  Specifying 127-255 doesn't
                // break anything, but seems to show the same exact value 0-126 would have (i.e. 127 is 0 brightness, 255 is full brightness).
                // Discarding the lowest bit from each to make the value fit in 0-126.
                spiDataBytes.Add((byte)(pixel.B >> 1));
                spiDataBytes.Add((byte)(pixel.G >> 1));
                spiDataBytes.Add((byte)(pixel.R >> 1));
            }

            spiDataBytes.AddRange(endFrame);

            spiDevice.Write(spiDataBytes.ToArray());
        }
    }
}
