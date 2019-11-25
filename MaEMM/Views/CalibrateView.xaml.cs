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
        private double moment;
        private double voltage; 
        private List<double> momentPointsY; 
        private List<double> voltagePointsX;
        private double aSlope;
        private double bIntercept; 

        public Calibrate()
        {
            this.InitializeComponent();
            strengthCB.SelectedIndex = 0;
            momentPointsY = new List<double>();
            voltagePointsX = new List<double>(); 
            
        }

        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Views.MainPage));
        }

        private void weight1B_Click(object sender, RoutedEventArgs e)
        {
            moment = Convert.ToDouble(weightOneTB.Text) * Convert.ToDouble(armLengthTB.Text);
            momentPointsY.Add(moment);

            //voltage = IncomingVoltage.somehow;
            voltagePointsX.Add(voltage); 
        }

        private void weight2B_Click(object sender, RoutedEventArgs e)
        {
            moment = Convert.ToDouble(weightOneTB.Text) * Convert.ToDouble(armLengthTB.Text);
            momentPointsY.Add(moment);

            //voltage = IncomingVoltage.somehow;
            voltagePointsX.Add(voltage);
        }

        private void weight3B_Click(object sender, RoutedEventArgs e)
        {
            moment = Convert.ToDouble(weightOneTB.Text) * Convert.ToDouble(armLengthTB.Text);
            momentPointsY.Add(moment);

            //voltage = IncomingVoltage.somehow;
            voltagePointsX.Add(voltage);
        }

        private void weight4B_Click(object sender, RoutedEventArgs e)
        {
            moment = Convert.ToDouble(weightOneTB.Text) * Convert.ToDouble(armLengthTB.Text);
            momentPointsY.Add(moment);

            //voltage = IncomingVoltage.somehow;
            voltagePointsX.Add(voltage);
        }

        private void calibrateB_Click(object sender, RoutedEventArgs e)
        {
            int n = momentPointsY.Count;
            double sumXY;
            double sumX;
            double sumXpower2; 
            double sumY; 
            int count = 0; 

            foreach (var point in voltagePointsX)
            {
                sumXY = +(point*momentPointsY[count]);
                count++; 
            }

            foreach (var point in voltagePointsX)
            {
                sumX = +point; 
            }

            foreach (var point in momentPointsY)
            {
                sumY = +point; 
            }

            foreach (var point in voltagePointsX)
            {
                sumXpower2 = +(point*point); 
            }

            aSlope = (n * sumXY - (sumX * sumY)) / (sumXpower2 - (sumX * sumX));
            bIntercept = (1 / n) * (sumY - (aSlope * sumX));

            calibrationMade = true;

            // Til sidst gemmes a og b, kalibreringsværdierne på sd kortet så disse kan læses ind hver gang
            // programmet åbnes.
        }

        private void strengthCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

    }
}
