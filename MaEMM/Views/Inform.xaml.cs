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
using MaeMMBusinessLogic;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MaEMM
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Inform : Page
    {
        public Inform()
        {
            this.InitializeComponent();
            //testTitleTB.Text = title;
        }

        private void nextB_Click(object sender, RoutedEventArgs e)
        {
            InformationDTO informationDTO = new InformationDTO(testTitleTB.Text, nameTB.Text, pIDTB.Text, testIDTB.Text, genderCB.Text, Convert.ToString(dateTimeDT), strengthNiveauCB.Text);
            this.Frame.Navigate(typeof(Views.MeasurePage), informationDTO);
            
        }

        private void backB_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Views.MainPage));
        }

        
        private void strengthNiveauCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            testTitleTB.IsEnabled = true;
            nameTB.IsEnabled = true;
            pIDTB.IsEnabled = true;
            testIDTB.IsEnabled = true;
            genderCB.IsEnabled = true;
            dateTimeDT.IsEnabled = true;
            commentsTB.IsEnabled = true;
            nextB.IsEnabled = true;
        }
    }
}
