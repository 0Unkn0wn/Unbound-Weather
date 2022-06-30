using System.Windows;
using System.Windows.Input;

namespace UnboundWeather.core
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DisplayData display = new();
        public MainWindow()
        {
            InitializeComponent();
            display.InitialiseView(ForecastGrid, TodayGrid, CurentCity);

        }
        private void CitySearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                display.UpdateView(ForecastGrid, TodayGrid, CurentCity, CitySearch);
            }
        }
    }
}
