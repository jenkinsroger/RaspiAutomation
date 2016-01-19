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
    public sealed partial class MainLightPage : Page
    {
        public static SPIclass spi = new SPIclass();
        public List<NamedColor> ColorsNamed = new List<NamedColor>();
        public Apa102 LedStrip = new Apa102();

        public MainLightPage()
        {
            this.InitializeComponent();

            spi.StartSPI();
        }

        private void btnRainbow_Click(object sender, RoutedEventArgs e)
        {
            LedStrip.Rainbow_Click();
        }

        private void btnTwinkle_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCylon_Click(object sender, RoutedEventArgs e)
        {
            LedStrip.cyclon();
        }

        private void btnColorShift_Click(object sender, RoutedEventArgs e)
        {
            LedStrip.ColorShift();
        }

        private void btnStopAnimation_Click(object sender, RoutedEventArgs e)
        {
            Apa102.ColorFadeRun = false;
            Apa102.CyclonRun = false;
            Apa102.fadeRun = false;
            Apa102.TwinkleRun = false;
            Apa102.ColorShiftRun = false;
        }

        private void btnCFader_Click(object sender, RoutedEventArgs e)
        {
            LedStrip.ColorsFade();
        }

        private void btnFader_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnColorSelect(object sender, RoutedEventArgs e)
        {
            Button selectedcolorbutton = (Button)sender;
            SolidColorBrush selectedcolor = selectedcolorbutton.Background as SolidColorBrush;
            LedStrip.AllOneColor(selectedcolor.Color);
        }
    }
}
