using System.Security.Principal;
using EPiServer.Personalization.VisitorGroups;
using System;
using System.Web;
using Ascend2018.Helpers;

namespace Ascend2018.Business.Criteria
{
    [VisitorGroupCriterion(
        DisplayName = "Weather Type", 
        Category = "Weather")]
    public class WeatherCriterion : CriterionBase<WeatherModel>
    {
        public override bool IsMatch(IPrincipal principal, HttpContextBase httpContext)
        {
            try
            {
                var pos = GeoPosition.GetUsersPositionOrNull();
                if (Model.UseVisitorsLocation && (pos != null))
                {
                    return (WeatherBroker.GetWeatherType(pos) == Model.Weather);
                }
                return (WeatherBroker.GetWeatherType(Model.DefaultLocation) == Model.Weather);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
