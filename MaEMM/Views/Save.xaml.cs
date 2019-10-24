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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MaEMM
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Save : Page
    {
        private InformationDTO informationDTO; 

        public Save()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            informationDTO = (InformationDTO)e.Parameter;

            TestTitleTB.Text = informationDTO.testTitle;
            nameTB.Text = informationDTO.patientName;
            PersonalIDTB.Text = informationDTO.personalID;
            TestIDTB.Text = informationDTO.testID;
            genderCB.Text = informationDTO.patientGender;
            dateTimeTB.Text = informationDTO.dateOfMeasurement; 
        }

        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Views.MeasurePage));
        }
    }
}
