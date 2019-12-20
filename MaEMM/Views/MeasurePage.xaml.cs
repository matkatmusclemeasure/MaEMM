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
using System.Threading;
using Windows.UI.Xaml;
using Windows.UI.Core;
using Windows.UI.Xaml.Media;
using Windows.UI;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Windows.UI.Popups;

namespace MaEMM.Views
{
    public sealed partial class MeasurePage : Page, INotifyPropertyChanged
    {
        private IDataPresenter datapresenter_;
        private IDataCalculator datacalculator_;
        private IDataProcessor dataprocessor_;
        //private IZeroPointAdjustment zeroPointAdjustment_;
        private IProducer producer_;
        private List<XYDTO> graphCoordinates; 
        private bool firsttime = true;
        private double zeroPointValue;
        private bool measureRunning = false;
        //private Thread measureThread;
        private int testcount = 0;
        private List<double> coordinateDownSampling;
        public ObservableCollection<XYDTO> downSampledCoordinate = new ObservableCollection<XYDTO>();
        private double DSCoordinateSum = 0;
        private double DSCoordinate;
        private BlockingCollection<int> BC_;
        //private XYDTO downsampledXY;
        private double timeCount;
        private bool manualAdjust = false; 

        public ObservableCollection<DataPoint> Source { get; } = new ObservableCollection<DataPoint>();
        private InformationDTO informationDTO;

        // TODO WTS: Change the chart as appropriate to your app.
        // For help see http://docs.telerik.com/windows-universal/controls/radchart/getting-started
        public MeasurePage()
        {
            InitializeComponent();
            graphCoordinates = new List<XYDTO>(); //kat
            BC_ = new BlockingCollection<int>();
            producer_ = new producer(BC_);
            dataprocessor_ = new DataProcessor(BC_);
            datacalculator_ = new DataCalculator(dataprocessor_);
            datapresenter_ = new DataPresenter(datacalculator_);
            //zeroPointAdjustment_ = new ZeroPointAdjustment();
            //datapresenter_.sendCoordinate += updateGraph;
            coordinateDownSampling = new List<double>();
            datapresenter_.sendCoordinate += sampleDown;
            MuscleForceChart.DataContext = downSampledCoordinate;

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

        private async void startMeasurementB_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
                DataPCParameterDTO DTO = new DataPCParameterDTO(Convert.ToDouble(armlengthTB.Text) / 100, informationDTO.strengthLevel);
                datapresenter_.setParameter(DTO);

                downSampledCoordinate.Clear();

                BC_ = new BlockingCollection<int>();

                producer_.startMeasure(BC_);
                datapresenter_.meassure(BC_);
            }
            catch(Exception exc)
            {
                var dialog = new MessageDialog("Enter real length");
                await dialog.ShowAsync();
            }

            
            //measureThread = new Thread(this.measure);
            //measureThread.IsBackground = true;
            ////testcount = 0;
            //startMeasurementB.IsEnabled = false;
            //measureThread.Start();
            
        }

        //private void measure()
        //{
        //    timeCount = 0;
        //    datapresenter_.resetList();
           
        //    measureRunning = true;

        //    while (measureRunning == true)
        //    {
        //        datapresenter_.meassure();
        //        //testcount++;
        //        System.Threading.Thread.Sleep(1);
        //    }

            
        //}

        private void stopMeasurementB_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //this.MuscleForceChart.DataContext = graphCoordinates; //kat 
            //measureRunning = false;
            //muscleForceTB.Text = Convert.ToString(testcount);
            //startMeasurementB.IsEnabled = true;

            producer_.stopMeasure();

            MaxExpDTOP maxDTO = datapresenter_.showResult();
            muscleForceTB.Text = Convert.ToString(Math.Round(maxDTO.maxMuscle,2));
            rateOfForceDevTB.Text = Convert.ToString(Math.Round(maxDTO.expMuscle,2));

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

        //private void resetMeasurementB_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        //{
        //    List<XYDTO> graphCoordinates = new List<XYDTO>();
        //    graphCoordinates.Add(new XYDTO(1, 2));
        //    graphCoordinates.Add(new XYDTO(2, 3));
        //    graphCoordinates.Add(new XYDTO(3, 4));
        //    graphCoordinates.Add(new XYDTO(4, 3));
        //    graphCoordinates.Add(new XYDTO(5, 1));

        //    this.MuscleForceChart.DataContext = graphCoordinates;

        //    timeCount = 0; 

        //}

        private async void zeroPointAdjustmentB_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

            try
            {
                DataPCParameterDTO DTO = new DataPCParameterDTO(Convert.ToDouble(armlengthTB.Text) / 100, informationDTO.strengthLevel);

                BC_ = new BlockingCollection<int>();

                datapresenter_.setParameter(DTO);

                producer_.zeroPointAdjust(BC_);

                datapresenter_.zeroPointAdjust(BC_);

                Color color = new Color();
                color = Colors.Green;
                zeroPointAdjustmentB.Background = new SolidColorBrush(color);
            }
            catch(Exception exc)
            {
                var dialog = new MessageDialog("Enter real length");
                await dialog.ShowAsync();
            }
            
        }

        private void setZeroPointValue(object sender, SendDoubleEvent e)
        {
            zeroPointValue = e.forceInput;
        }

        void AddData(XYDTO data)
        {
            // Updates to the dataDoubles collection must be done on the UI thread.
            // Otherwise the application will throw an exception, because only the UI
            // thread is allowed to modify UI components and the modification of the
            // collection will result in a UI update.
            Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                downSampledCoordinate.Add(data);
            });
        }

        private void sampleDown(object sender, SendCoordinateEvent e)
        {
            if (manualAdjust == true)
            {
                if (coordinateDownSampling.Count < 17)
                {
                    coordinateDownSampling.Add(e.y);
                }
                else if (coordinateDownSampling.Count >= 17)
                {
                    timeCount += 0.017;

                    for (int i = 0; i < 17; i++)
                    {
                        DSCoordinateSum += coordinateDownSampling[i];
                    }

                    DSCoordinate = DSCoordinateSum / 17;

                    //downsampledXY.X = timeCount; 
                    //downsampledXY.Y = DSCoordinate;

                    Task updateTask = new Task(async () =>
                    {

                        AddData(new XYDTO(timeCount, DSCoordinate));

                    });
                    updateTask.Start();

                    //downSampledCoordinate.Add(new XYDTO(timeCount, DSCoordinate));

                    coordinateDownSampling.Clear();
                    DSCoordinateSum = 0;
                }
            }
        }

        private async void manualAdjustmentB_Click(object sender, RoutedEventArgs e)
        {
            manualAdjust = true;

            try
            {
                DataPCParameterDTO DTO = new DataPCParameterDTO(Convert.ToDouble(armlengthTB.Text) / 100, informationDTO.strengthLevel);
                datapresenter_.setParameter(DTO);

                downSampledCoordinate.Clear();

                BC_ = new BlockingCollection<int>();

                producer_.startMeasure(BC_);
                datapresenter_.meassure(BC_);
            }
            catch (Exception exc)
            {
                var dialog = new MessageDialog("Enter real length");
                await dialog.ShowAsync();
            }
        }

        private void manualAdjustmentStopB_Click(object sender, RoutedEventArgs e)
        {
            producer_.stopMeasure(); 
            timeCount = 0;
            manualAdjust = false; 
        }
    }
}
