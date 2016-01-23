using Sensors.Dht;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.Gpio;
using Windows.Devices.SerialCommunication;
using Windows.Storage.Streams;

namespace HomeAutomation
{
    class StaticPropertiescs
    {
        public static IDht _dht = null;
        public static DhtReading reading;
        public static GpioPin _pin;

        public static void SetupDTH11()
        {
            _pin = GpioController.GetDefault().OpenPin(4, GpioSharingMode.Exclusive);
            _dht = new Dht11(_pin, GpioPinDriveMode.Input);
        }


        public static SerialDevice serialPort;
        public static DataWriter dataWriteObject = null;
        public static DataReader dataReaderObject = null;
        public static ObservableCollection<DeviceInformation> listOfDevices;
        public static CancellationTokenSource ReadCancellationTokenSource;

        

    }

    static class ConvertTemp
    {
        public static double ConvertCelsiusToFahrenheit(double c)
        {
            return ((9.0 / 5.0) * c) + 32;
        }

        public static double ConvertFahrenheitToCelsius(double f)
        {
            return (5.0 / 9.0) * (f - 32);
        }
    }
}
