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
using MaeMMBusinessLogic; 

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MaEMM
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Calibrate : Page
    {
        private bool calibrationMade = false;
        private bool firstTimeChanged = true;
 
        private List<double> momentPointsY; 
        private List<double> voltagePointsX;
        private double aSlope;
        private double bIntercept;

        private ICalibrate calibrate; 

        public Calibrate()
        {
            this.InitializeComponent();
            strengthCB.SelectedIndex = 0;
            momentPointsY = new List<double>();
            voltagePointsX = new List<double>();
            calibrate = new CalibrateLogic(); 
            
        }

        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Views.MainPage));
        }

        private void weight1B_Click(object sender, RoutedEventArgs e)
        {
            calibrate.enterCalibrationValues(Convert.ToDouble(weightOneTB.Text), Convert.ToDouble(weightOneTB.Text)); 
        }

        private void weight2B_Click(object sender, RoutedEventArgs e)
        {
            calibrate.enterCalibrationValues(Convert.ToDouble(weightTwoTB.Text), Convert.ToDouble(weightTwoTB.Text));
        }

        private void weight3B_Click(object sender, RoutedEventArgs e)
        {
            calibrate.enterCalibrationValues(Convert.ToDouble(weightThreeTB.Text), Convert.ToDouble(weightThreeTB.Text));
        }

        private void weight4B_Click(object sender, RoutedEventArgs e)
        {
            calibrate.enterCalibrationValues(Convert.ToDouble(weightFourTB.Text), Convert.ToDouble(weightFourTB.Text));
        }

        private void calibrateB_Click(object sender, RoutedEventArgs e)
        {
            calibrate.startCalibration(); 
            calibrationMade = true;
        }

        private void strengthCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

    }
}
