using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer.ContentApi.Core;
using EPiServer.Core;
using EPiServer.ServiceLocation;

namespace EPiServer.ContentApi.MusicFestival.Features.ContentApi
{
    public class LowercaseLongStringPropertyModelHandler : DefaultPropertyModelHandler
    {
        public LowercaseLongStringPropertyModelHandler()
        {
            ModelTypes = new List<TypeModel>
            {
                new TypeModel { ModelType = typeof(LowercaseLongStringPropertyModel), ModelTypeString = nameof(LowercaseLongStringPropertyModel), PropertyType = typeof(PropertyLongString) },
            };
        }

        public override int SortOrder { get; } = 100;
    }
}