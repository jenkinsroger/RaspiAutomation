using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sensors.Dht;
//using Sensors.OneWire.Common;
using Windows.Devices.Gpio;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace HomeAutomation
{
    class DHT11Class
    {
        public static DispatcherTimer _timer = new DispatcherTimer();

        public static GpioPin _pin = null;
        public static IDht _dht = null;
        public static List<int> _retryCount = new List<int>();
        public static DateTimeOffset _startedAt = DateTime.MinValue;

        public static void Setup()
        {

            _pin = GpioController.GetDefault().OpenPin(4, GpioSharingMode.Exclusive);
            _dht = new Dht11(_pin, GpioPinDriveMode.Input);

            _timer.Start();

            _startedAt = DateTimeOffset.Now;

            
        }

    }
}
