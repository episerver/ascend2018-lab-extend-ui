using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer.ContentApi.Core;
using EPiServer.Core;

namespace EPiServer.ContentApi.MusicFestival.Features.ContentApi
{
    public class LowercaseLongStringPropertyModel : PropertyModel<string, PropertyLongString>
    {
        public LowercaseLongStringPropertyModel(PropertyLongString propertyLongString) : base(propertyLongString)
        {
            if (propertyLongString != null)
            {
                Value = propertyLongString.ToString().ToLower();
            }
        }
    }
}