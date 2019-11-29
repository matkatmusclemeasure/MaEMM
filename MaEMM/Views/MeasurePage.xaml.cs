using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;


using MaEMM.Core.Models;
using MaEMM.Core.Services;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using MaeMMBusinessLogic;

namespace MaEMM.Views
{
    public sealed partial class MeasurePage : Page, INotifyPropertyChanged
    {
        private IDataPresenter datapresenter_;
        private IDataCalculator datacalculator_;
        private IDataProcessor dataprocessor_;

         
        public ObservableCollection<DataPoint> Source { get; } = new ObservableCollection<DataPoint>();
        private InformationDTO informationDTO; 

        // TODO WTS: Change the chart as appropriate to your app.
        // For help see http://docs.telerik.com/windows-universal/controls/radchart/getting-started
        public MeasurePage()
        {
            InitializeComponent();
            dataprocessor_ = new DataProcessor();
            datacalculator_ = new DataCalculator(dataprocessor_);
            datapresenter_ = new DataPresenter(datacalculator_);
            datapresenter_.sendCoordinate += updateGraph;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            informationDTO = (InformationDTO)e.Parameter;

            Source.Clear();

            // TODO WTS: Replace this with your actual data
            var data = await SampleDataService.GetChartDataAsync();
            foreach (var item in data)
            {
                Source.Add(item);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void backB_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Inform));
        }

        private void saveMeasurementB_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Save), informationDTO);
        }

        private void startMeasurementB_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            startMeasurementB.IsEnabled = false;
            DataPCParameterDTO DTO = new DataPCParameterDTO(/*Convert.ToDouble(armlengthTB.Text)*/ 1, informationDTO.strengthLevel);
            datapresenter_.setParameter(DTO);

            for (int i = 0; i < 100; i++)
            {
                datapresenter_.meassure();
            }
            startMeasurementB.IsEnabled = false;
        }

        private void stopMeasurementB_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            MaxExpDTOP maxDTO = datapresenter_.showResult();
            muscleForceTB.Text = Convert.ToString(maxDTO.maxMuscle);
            rateOfForceDevTB.Text = Convert.ToString(maxDTO.expMuscle);
        }

        private void updateGraph(object sender, SendCoordinateEvent e)
        { 
            List<XYDTO> graphCoordinates = new List<XYDTO>(); //kat
            XYDTO xyCoordinates = new XYDTO(e.x, e.y); //kat

            graphCoordinates.Add(xyCoordinates); //kat

            //Opdatering af graph
            this.MuscleForceChart.DataContext = graphCoordinates; //kat

        }
    }
}
