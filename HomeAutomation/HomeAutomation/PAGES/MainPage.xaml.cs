using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Enumeration;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HomeAutomation
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private DispatcherTimer _Datectimer = new DispatcherTimer();
        public static SPIclass spi = new SPIclass();
        public static DateTime newdatetime = new DateTime();


        public MainPage()
        {
            

            this.InitializeComponent();
            contentFrame.Navigate(typeof(CommunicationScreen));

            UpdateTimeClass.getdata();

            //StaticPropertiescs.SetupDTH11();
            //spi.StartSPI();


            _Datectimer.Interval = TimeSpan.FromSeconds(1);
            _Datectimer.Tick += _Datectimer_Tick;
            _Datectimer.Start();

        }

        private async void _Datectimer_Tick(object sender, object e)
        {
            txtDate.Text = string.Format("{0:dddd, MMMM d, yyyy}", DateTime.Now);
            txtTime.Text = string.Format("{0:hh:mm:ss tt}", DateTime.Now);
        }

        private void btnLightTxt_Tapped(object sender, TappedRoutedEventArgs e)
        {
            contentFrame.Navigate(typeof(MainLightPage));
        }

        private void HomeText_Tapped_1(object sender, TappedRoutedEventArgs e)
        {
            contentFrame.Navigate(typeof(HomePage));
        }
    }
}
