﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;


using MaEMM.Core.Models;
using MaEMM.Core.Services;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using MaeMMBusinessLogic;
using System.Threading;
using Windows.UI.Xaml;
using Windows.UI.Core;
using Windows.UI.Xaml.Media;
using Windows.UI;

namespace MaEMM.Views
{
    public sealed partial class MeasurePage : Page, INotifyPropertyChanged
    {
        private IDataPresenter datapresenter_;
        private IDataCalculator datacalculator_;
        private IDataProcessor dataprocessor_;
        private IZeroPointAdjustment zeroPointAdjustment_; 
        private List<XYDTO> graphCoordinates; 
        private bool firsttime = true;
        private double zeroPointValue;
        private bool measureRunning = false;
        private Thread measureThread;
        private int testcount = 0;
        private List<double> coordinateDownSampling;
        public ObservableCollection<XYDTO> downSampledCoordinate { get; } = new ObservableCollection<XYDTO>();
        private double DSCoordinateSum = 0;
        private double DSCoordinate;
        private XYDTO downsampledXY;
        private double timeCount; 

        public ObservableCollection<DataPoint> Source { get; } = new ObservableCollection<DataPoint>();
        private InformationDTO informationDTO;

        // TODO WTS: Change the chart as appropriate to your app.
        // For help see http://docs.telerik.com/windows-universal/controls/radchart/getting-started
        public MeasurePage()
        {
            InitializeComponent();
            graphCoordinates = new List<XYDTO>(); //kat
            dataprocessor_ = new DataProcessor();
            datacalculator_ = new DataCalculator(dataprocessor_);
            datapresenter_ = new DataPresenter(datacalculator_);
            zeroPointAdjustment_ = new ZeroPointAdjustment(); 
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
            measureThread = new Thread(this.measure);
            measureThread.IsBackground = true;
            //testcount = 0;
            startMeasurementB.IsEnabled = false;
            measureThread.Start();
            
        }

        private void measure()
        {
            timeCount = 0; 
            DataPCParameterDTO DTO = new DataPCParameterDTO(/*Convert.ToDouble(armlengthTB.Text)*/ 1, informationDTO.strengthLevel);
            DataPCParameterDTO DTO = new DataPCParameterDTO(/*Convert.ToDouble(armlengthTB.Text)*/ 20, informationDTO.strengthLevel);
            datapresenter_.resetList();
            datapresenter_.setParameter(DTO);
            measureRunning = true;

            while (measureRunning == true)
            {
                datapresenter_.meassure();
                //testcount++;
                System.Threading.Thread.Sleep(1);
            }

            
        }

        private void stopMeasurementB_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //this.MuscleForceChart.DataContext = graphCoordinates; //kat 
            measureRunning = false;
            //muscleForceTB.Text = Convert.ToString(testcount);
            startMeasurementB.IsEnabled = true;

            MaxExpDTOP maxDTO = datapresenter_.showResult();
            muscleForceTB.Text = Convert.ToString(maxDTO.maxMuscle);
            rateOfForceDevTB.Text = Convert.ToString(maxDTO.expMuscle);

            timeCount = 0; 
        }

        private void updateGraph(object sender, SendCoordinateEvent e)
        { 
            
            //XYDTO xyCoordinates = new XYDTO(e.x, e.y); //kat

            //graphCoordinates.Add(xyCoordinates); //kat

            //Opdatering af graph
            //if(firsttime == false)
            //{
            //    this.MuscleForceChart.DataContext = graphCoordinates; //kat 
            //}
            //else
            //{
            //    firsttime = false;
            //}

        }

        private void resetMeasurementB_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            List<XYDTO> graphCoordinates = new List<XYDTO>();
            graphCoordinates.Add(new XYDTO(1, 2));
            graphCoordinates.Add(new XYDTO(2, 3));
            graphCoordinates.Add(new XYDTO(3, 4));
            graphCoordinates.Add(new XYDTO(4, 3));
            graphCoordinates.Add(new XYDTO(5, 1));

            this.MuscleForceChart.DataContext = graphCoordinates;

            timeCount = 0; 

        }

        private void zeroPointAdjustmentB_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            datapresenter_.zeroPointAdjust();

            Color color = new Color();
            color = Colors.Green;
            zeroPointAdjustmentB.Background = new SolidColorBrush(color);
        }

        private void setZeroPointValue(object sender, SendDoubleEvent e)
        {
            zeroPointValue = e.forceInput;
        }

        private void sampleDown(object sender, SendCoordinateEvent e)
        {

            if (coordinateDownSampling.Count < 17)
            {
                coordinateDownSampling.Add(e.y);
            }
            else if (coordinateDownSampling.Count >= 17 )
            {
                timeCount += 0.017;

                for (int i = 0; i < 17; i++)
                {
                    DSCoordinateSum += coordinateDownSampling[i];
                }

                DSCoordinate = DSCoordinateSum / 17;

                downsampledXY.X = timeCount; 
                downsampledXY.Y = DSCoordinate;

                downSampledCoordinate.Add(downsampledXY);

                coordinateDownSampling.Clear();
                DSCoordinateSum = 0;
            }
        }
 
    }
}
