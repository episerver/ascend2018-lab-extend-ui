using EPiServer;
using EPiServer.Personalization;
using System;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Ascend2018.Business.Criteria
{
    public static class WeatherBroker
    {
        private const string CurrentWeatherMetricPos = "http://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&mode=xml&units=metric&APPID=278927630072613245ff2dda493268af";
        private const string CurrentWeatherMetricQuery = "http://api.openweathermap.org/data/2.5/weather?q={0}&mode=xml&units=metric&APPID=278927630072613245ff2dda493268af";
        
        private static XDocument LoadWeather(Uri url)
        {
            //caching
            var xdoc = CacheManager.Get(url.ToString()) as XDocument;
            if (xdoc == null)
            {
                xdoc = XDocument.Load(url.ToString());
                CacheManager.Insert(url.ToString(), xdoc);
            }
            return xdoc;
        }

        public static WeatherTypes GetWeatherType(Uri url)
        {
            var xdoc = LoadWeather(url);
            var weathernode = xdoc.Descendants("weather").FirstOrDefault();
            if (weathernode == null) return WeatherTypes.Unknown;
            var val = int.Parse(weathernode.Attribute("number").Value);
            if (val >= 200 && val < 300) return WeatherTypes.Thunderstorms;
            if (val >= 300 && val < 400) return WeatherTypes.Drizzle;
            if (val >= 500 && val < 600) return WeatherTypes.Rain;
            if (val >= 600 && val < 700) return WeatherTypes.Snow;
            if (val >= 800 && val <= 802) return WeatherTypes.Clear;
            if (val >= 803 && val <= 900) return WeatherTypes.Clouds;
            if (val >= 900 && val <= 950) return WeatherTypes.Extreme;
            if (val >= 954 && val < 100) return WeatherTypes.Windy;

            return WeatherTypes.Unknown;
        }

        public static WeatherTypes GetWeatherType(GeoCoordinate loc)
        {
            return GetWeatherType(new Uri(string.Format(CurrentWeatherMetricPos, loc.Latitude, loc.Longitude), UriKind.Absolute));
        }

        public static WeatherTypes GetWeatherType(string query)
        {
            return GetWeatherType(new Uri(string.Format(CurrentWeatherMetricQuery, HttpContext.Current.Server.UrlEncode(query)), UriKind.Absolute));
        }
    }
}