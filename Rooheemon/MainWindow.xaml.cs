using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;


namespace Rooheemon
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary> 
    public partial class MainWindow : Window
    {
        public SeriesCollection SeriesCollection { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public string[] Labels { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            SeriesCollection = new SeriesCollection()
            {
                new LineSeries
                {
                    Title = "Serie Blue",
                    Values = new ChartValues<double>{4, 6, 5, 2, 4}
                }
            };

            Labels = new[] {"Jan", "Feb", "Mar", "Apr", "May"};
            //YFormatter = value => value.ToString("C");

            DataContext = this;

            Console.WriteLine("\n\n============ CONSTRUCTOR DONE ============= \n\n");
        }

        protected override void OnContentRendered(EventArgs e)
        {
            ThreadStart childRef = new ThreadStart(DoSomeComputation);

            Thread theThread = new Thread(childRef);
            theThread.Start();

            
        }

              
        

        private void DoSomeComputation() 
        {
            var rdn = new Random();
            for (int i = 0; i < 90; i++)
            {
                Thread.Sleep(1500);
                this.Dispatcher.Invoke(() =>
                {
                    SeriesCollection[0].Values = new ChartValues<double> {
                        rdn.Next(1, 10),
                        rdn.Next(1, 10),
                        rdn.Next(1, 10),
                        rdn.Next(1, 10),
                        rdn.Next(1, 10)};
                }); 
            }
        }

    }
}
