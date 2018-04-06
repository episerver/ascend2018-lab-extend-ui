using EPiServer.Core;
using EPiServer.PlugIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ascend2018.Models
{
    [PropertyDefinitionTypePlugIn]
    public class AddressesListProperty : PropertyList<Address>
    {
    }
}