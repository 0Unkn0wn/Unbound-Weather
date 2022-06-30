namespace UnboundWeather.core
{
    public class DisplayData
    {
        public WeatherData data = new();
        public WeatherApiHandler handler = new();

        public void InitialiseView(System.Windows.Controls.DataGrid ForecastGrid, System.Windows.Controls.DataGrid TodayGrid, System.Windows.Controls.TextBox CurrentCity)
        {
            handler.Connection(data);
            ForecastGrid.ItemsSource = data.ForecastWeatherData;
            TodayGrid.ItemsSource = data.TodayViewData;
            CurrentCity.Text = handler.Location();
        }

        public void UpdateView(System.Windows.Controls.DataGrid ForecastGrid, System.Windows.Controls.DataGrid TodayGrid, System.Windows.Controls.TextBox CurrentCity, System.Windows.Controls.TextBox CitySearch)
        {
            data.ForecastWeatherData.Clear();
            data.TodayViewData.Clear();
            handler.Connection(data, CitySearch.Text, CurrentCity);
            ForecastGrid.ItemsSource = data.ForecastWeatherData;
            TodayGrid.ItemsSource = data.TodayViewData;
        }

    }
}
