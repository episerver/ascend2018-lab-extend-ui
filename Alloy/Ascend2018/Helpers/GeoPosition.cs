using EPiServer;
using EPiServer.Personalization;
using EPiServer.Personalization.Providers.MaxMind;
using EPiServer.ServiceLocation;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace Ascend2018.Helpers
{
    public static class GeoPosition
    {
        

        public static GeoCoordinate GetUsersPositionOrNull()
        {
            try
            {
                var requestIp = GetRequestIp();
                var ip = IPAddress.Parse(requestIp);
                var provider = ServiceLocator.Current.GetInstance<GeolocationProviderBase>();
                IGeolocationResult result = provider.Lookup(ip);
                return result != null ? result.Location : null;
            }
            catch
            {
                return null;
            }
        }

        public static GeoCoordinate GetUsersPosition()
        {
            var requestIp = GetRequestIp();
            //requestIp = "146.185.31.213";//Temp, provoke error
            var ip = IPAddress.Parse(requestIp);
            var provider = ServiceLocator.Current.GetInstance<GeolocationProviderBase>();
            IGeolocationResult result = provider.Lookup(ip);
            return result != null ? result.Location : provider.Lookup(IPAddress.Parse("8.8.8.8")).Location;

            //return new GeoCoordinate(59.33, 18.07);
        }

        //TODO: Add try-catch.
        public static IGeolocationResult GetUsersLocation()
        {
            try
            {
                var requestIp = GetRequestIp();
                //requestIp = "146.185.31.213";//Temp, provoke error
                var ip = IPAddress.Parse(requestIp);
                var provider = ServiceLocator.Current.GetInstance<GeolocationProviderBase>();
                IGeolocationResult result = provider.Lookup(ip);
                return result ?? provider.Lookup(IPAddress.Parse("8.8.8.8"));
            }
            catch
            {
                var provider = ServiceLocator.Current.GetInstance<GeolocationProviderBase>();
                return provider.Lookup(IPAddress.Parse("8.8.8.8"));
            }

        }

        private static string GetRequestIp()
        {
            var requestIp = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            
            if (string.IsNullOrWhiteSpace(requestIp))
            {
                requestIp = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            if (requestIp.Contains(":"))
            {
                //Port number is included, disregard it
                requestIp = requestIp.Substring(0, requestIp.IndexOf(':'));
            }
            if (!requestIp.Contains(".") || requestIp == "127.0.0.1")
            {
                requestIp = GetLocalRequestIp();
            }
            return requestIp;
        }

        private static string GetLocalRequestIp()
        {
            string requestIp = CacheManager.Get("local_ip") as string;
            if (!string.IsNullOrWhiteSpace(requestIp))
            {
                return requestIp;
            }
            var lookupRequest = WebRequest.Create("http://ipinfo.io/ip/");
            var webResponse = lookupRequest.GetResponse();
            using (var responseStream = webResponse.GetResponseStream())
            {
                var streamReader = new StreamReader(responseStream, Encoding.UTF8);
                requestIp = streamReader.ReadToEnd().Trim();
            }
            webResponse.Close();
            CacheManager.Insert("local_ip", requestIp);
            return requestIp;
        }

    }
}