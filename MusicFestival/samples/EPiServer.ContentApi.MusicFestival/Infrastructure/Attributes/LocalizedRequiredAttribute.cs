using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EPiServer.ContentApi.MusicFestival.Infrastructure.Attributes
{
    public class LocalizedRequiredAttribute : RequiredAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            ErrorMessage = "Error";
            return base.FormatErrorMessage(name);
        }
    }
}