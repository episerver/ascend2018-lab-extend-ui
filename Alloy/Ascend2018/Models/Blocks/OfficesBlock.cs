using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System.Collections;
using System.Collections.Generic;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Cms.Shell.UI.ObjectEditing.EditorDescriptors;
using Ascend2018.Models;

namespace Ascend2018.Models.Blocks
{
    [ContentType(DisplayName = "Offices", GUID = "7972d25f-921b-4e3e-a56a-921fe56a26a2", Description = "")]
    public class OfficesBlock : BlockData
    {
        [EditorDescriptor(EditorDescriptorType = typeof(CollectionEditorDescriptor<Address>))]
        [Display(Name ="Our Addresses")]
        public virtual IList<Address> OurAddresses { get; set; }
    }
}