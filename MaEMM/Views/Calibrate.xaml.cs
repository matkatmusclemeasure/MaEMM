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

        public Calibrate()
        {
            this.InitializeComponent();
            strengthCB.SelectedIndex = 0;
        }

        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Views.MainPage));
        }

        private void weight1B_Click(object sender, RoutedEventArgs e)
        {
            // Skal have omregnet den indtastede vægt og den indtastede armlængde til et moment, som kan
            // kædes sammen med den spænding som måles når der trykkes på knappen gem.
            // Disse værdier skal benyttes til at regne kalibreringsværdierne a og b ved regression, 
            // for sammenhængen mellem uafhængig variabel spænding, x, og afhængig variabel moment, y. 
        }

        private void weight2B_Click(object sender, RoutedEventArgs e)
        {
            // Skal have omregnet den indtastede vægt og den indtastede armlængde til et moment, som kan
            // kædes sammen med den spænding som måles når der trykkes på knappen gem.
            // Disse værdier skal benyttes til at regne kalibreringsværdierne a og b ved regression, 
            // for sammenhængen mellem uafhængig variabel spænding, x, og afhængig variabel moment, y.
        }

        private void weight3B_Click(object sender, RoutedEventArgs e)
        {
            // Skal have omregnet den indtastede vægt og den indtastede armlængde til et moment, som kan
            // kædes sammen med den spænding som måles når der trykkes på knappen gem.
            // Disse værdier skal benyttes til at regne kalibreringsværdierne a og b ved regression, 
            // for sammenhængen mellem uafhængig variabel spænding, x, og afhængig variabel moment, y.
        }

        private void weight4B_Click(object sender, RoutedEventArgs e)
        {
            // Skal have omregnet den indtastede vægt og den indtastede armlængde til et moment, som kan
            // kædes sammen med den spænding som måles når der trykkes på knappen gem.
            // Disse værdier skal benyttes til at regne kalibreringsværdierne a og b ved regression, 
            // for sammenhængen mellem uafhængig variabel spænding, x, og afhængig variabel moment, y.
        }

        private void calibrateB_Click(object sender, RoutedEventArgs e)
        {
            //Her sker selve udregningen af kalibreringsværdierne a og b ved lineær regression
            // hvor de 4 indtastede/målte datasæt benyttes i beregningerne.
            // Til sidst gemmes a og b, kalibreringsværdierne på sd kortet så disse kan læses ind hver gang
            // programmet åbnes.

            calibrationMade = true;
        }

        private async void strengthCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (firstTimeChanged == true)
            {
                firstTimeChanged = false;
            }

            else
            {
                if (calibrationMade == true)
                {
                    armLengthTB.Text = "";
                    weightOneTB.Text = "";
                    weightTwoTB.Text = "";
                    weightThreeTB.Text = "";
                    weightFourTB.Text = "";
                    calibrationMade = false;
                }
                else
                {
                    var messageDialog = new MessageDialog("Do you wanna continue without making the calibration for the present strength level?");

                    messageDialog.Commands.Add(new UICommand("Yes", new UICommandInvokedHandler(this.CommandInvokedHandler)));
                    messageDialog.Commands.Add(new UICommand("No", new UICommandInvokedHandler(this.CommandInvokedHandler)));

                    await messageDialog.ShowAsync();
                }
            }
            
        }

        private void CommandInvokedHandler(IUICommand command)
        {
            if(command.Label == "Yes")
            {
                armLengthTB.Text = "";
                weightOneTB.Text = "";
                weightTwoTB.Text = "";
                weightThreeTB.Text = "";
                weightFourTB.Text = "";
                calibrationMade = false;
            }
            else if(command.Label=="No")
            {
                //Det skal gøres så den ikke skifter i combobox, måske ikke muligt
            }
        }
    }
}
