using Newtonsoft.Json;
using RestSharp;
using System;
using System.Linq;
using System.Windows;



namespace UnboundWeather.core
{
    public class WeatherApiHandler
    {
        public string Location()
        {
            var client = new RestClient($"https://api.ip2location.io/?key=c34d00da3f54f6f62b90f2369cbb9d64&format=json");
            var request = new RestRequest("");
            var response = client.Execute(request);
            var body = response.Content;
            dynamic location_data = JsonConvert.DeserializeObject(body);
            return $"{location_data.city_name},{location_data.country_code}";
        }
        public void CurrentData(WeatherData weatherdata)
        {
            weatherdata.Real_time_temp = weatherdata.ForecastWeatherData.ElementAt(0).TemperatureValue;
            weatherdata.Real_time_hum = weatherdata.ForecastWeatherData.ElementAt(0).Humidity;
            weatherdata.RealTimeDate = weatherdata.ForecastWeatherData.ElementAt(0).Date;
            weatherdata.RealTimeIcon = weatherdata.ForecastWeatherData.ElementAt(0).IconId;
            weatherdata.TodayViewData.Add(new WeatherData { TemperatureValue = weatherdata.Real_time_temp, Humidity = weatherdata.Real_time_hum, Date = weatherdata.RealTimeDate, IconId = weatherdata.RealTimeIcon });
        }
        public async void Connection(WeatherData weatherdata)
        {
            var location = Location();
            var client = new RestClient("http://api.openweathermap.org/data/2.5/");
            var request = new RestRequest($"forecast?q={location}&units=metric&appid=d2b7177f8dbaa3f53c3035d9211711a7");
            var response = await client.ExecuteAsync(request);
            var body = response.Content;
            dynamic weather = JsonConvert.DeserializeObject(body);
            foreach (var day in weather.list)
            {
                var temp = new WeatherData();
                temp.TemperatureValue = day.main.feels_like;
                temp.Humidity = day.main.humidity;
                temp.Date = day.dt_txt;
                foreach (var icon in day.weather)
                {
                    temp.IconId = icon.icon;
                }
                DateTime dateTime = DateTime.Parse(temp.Date);
                temp.Date = dateTime.ToString("ddd HH:mm");
                weatherdata.ForecastWeatherData.Add(temp);
            }
            CurrentData(weatherdata);
        }

        public async void Connection(WeatherData weatherdata, string city, System.Windows.Controls.TextBox actualCity)
        {
            ///fb6706c2fa464ac6b637558b3430074e
            var client = new RestClient("http://api.openweathermap.org/data/2.5/");
            var request = new RestRequest($"forecast?q={city}&units=metric&appid=d2b7177f8dbaa3f53c3035d9211711a7");
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show($"Weather for {city}");
                actualCity.Text = city;
                var body = response.Content;
                dynamic weather = JsonConvert.DeserializeObject(body);
                foreach (var day in weather.list)
                {
                    var temp = new WeatherData
                    {
                        TemperatureValue = day.main.temp_max,
                        Humidity = day.main.humidity,
                        Date = day.dt_txt
                    };
                    foreach (var icon in day.weather)
                    {
                        temp.IconId = icon.icon;
                    }
                    DateTime dateTime = DateTime.Parse(temp.Date);
                    temp.Date = dateTime.ToString("ddd HH:mm");
                    weatherdata.ForecastWeatherData.Add(temp);
                }

                CurrentData(weatherdata);
            }
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                MessageBox.Show($"Invalid weather for {city}");
                actualCity.Text = "Error unknown location";
            }

        }
    }
}
