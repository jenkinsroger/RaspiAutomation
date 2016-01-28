using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CommunicationScreen : Page
    {
        private DispatcherTimer _Datectimer = new DispatcherTimer();
        public static string Message;
        public CommunicationScreen()
        {
            this.InitializeComponent();


            _Datectimer.Interval = TimeSpan.FromSeconds(1);
            _Datectimer.Tick += _Datectimer_Tick;
            _Datectimer.Start();
        }

        private async void _Datectimer_Tick(object sender, object e)
        {
            textBlock.Text = Message;
        }

    }
}
