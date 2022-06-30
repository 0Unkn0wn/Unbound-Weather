using System.Collections.ObjectModel;
using ReactiveUI;

namespace UnboundWeather.core
{
    public class WeatherData : ReactiveObject
    {
        private double temperature;
        private double humidity;
        private string? day_of_the_week;
        public string IconSource
        {
            get => @$"http://openweathermap.org/img/w/{IconId}.png";
        }

        public string? IconId { get; set; }

        public string? City { get; set; }
        public double Real_time_temp { get; set; } 

        public double Real_time_hum { get; set; }

        public string? RealTimeDate { get; set; }

        public string? RealTimeIcon { get; set; }
 
        public string Temperature
        {
            get => $"{TemperatureValue} °C";
        }
        public double TemperatureValue
        {
            get => temperature;
            set => this.RaiseAndSetIfChanged(ref temperature, value);
        }
        public double Humidity
        {
            get => humidity;
            set => this.RaiseAndSetIfChanged(ref humidity, value);
        }
        public string Date
        {
            get => day_of_the_week;

            set => this.RaiseAndSetIfChanged(ref day_of_the_week, value);
        }

        public ObservableCollection<WeatherData> ForecastWeatherData = new();
        public ObservableCollection<WeatherData> TodayViewData = new();
    }
}
