using Sensors.Dht;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HomeAutomation
{
    public sealed partial class HomePage : Page
    {
        private DispatcherTimer _timer = new DispatcherTimer();
        public Apa102 LedStrip = new Apa102();

        public HomePage()
        {
            this.InitializeComponent();

            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += _timer_Tick;

            getinitreadingtemp();
        }

        private async void getinitreadingtemp()
        {
            _timer.Start();
        }

        private async void _timer_Tick(object sender, object e)
        {

           StaticPropertiescs.reading = new DhtReading();
            StaticPropertiescs.reading = await StaticPropertiescs._dht.GetReadingAsync().AsTask();

            if (StaticPropertiescs.reading.IsValid)
            {
                txtTemp.Text = (StaticPropertiescs.reading.Temperature).ToString() + " °C" + "       " + (ConvertTemp.ConvertCelsiusToFahrenheit(StaticPropertiescs.reading.Temperature).ToString() + " °F");
            }
            else
            {

            }
        }

        private void btnLightOn_Click(object sender, RoutedEventArgs e)
        {
            LedStrip.AllOneColor(Windows.UI.Color.FromArgb(255, 255, 255, 255));
        }

        private void btnLightOff_Click(object sender, RoutedEventArgs e)
        {
            LedStrip.AllOneColor(Windows.UI.Color.FromArgb(0, 0, 0, 0));
        }
    }
}
