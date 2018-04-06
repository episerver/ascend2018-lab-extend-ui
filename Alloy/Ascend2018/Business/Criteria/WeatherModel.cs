using EPiServer.Personalization.VisitorGroups;
using EPiServer.Web.Mvc.VisitorGroups;
using System.ComponentModel.DataAnnotations;

namespace Ascend2018.Business.Criteria
{
    public class WeatherModel : CriterionModelBase
    {
        #region Editable Properties
        [DojoWidget(LabelTranslationKey ="/weathermodel/defaultlocation")]
        public string DefaultLocation { get; set; }

        [DojoWidget(LabelTranslationKey = "/weathermodel/usevisitorslocation")]
        public bool UseVisitorsLocation { get; set; }

        [DojoWidget(
            SelectionFactoryType = typeof(EnumSelectionFactory), 
            AdditionalOptions = "{ selectOnClick:true}")]
        public WeatherTypes Weather { get; set; }

        #endregion

        public override ICriterionModel Copy()
        {
            return ShallowCopy();
        }
    }

  
}
