using System;
using System.Collections.Generic;
using EPiServer.Core;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using EPiServer.Web;

namespace Ascend2018.Business.EditorDescriptors
{
    /// <summary>
    /// Replaces the default editor for image properties.
    /// "PlaceLast" makes sure all other EditorDescriptor, like the built in one (ImageReferenceEditorDescriptor), are
    /// applied first so we get all the data they set (such as "RepositoryKey" which is needed to get the content root).
    /// </summary>
    [EditorDescriptorRegistration(TargetType = typeof(ContentReference), UIHint = UIHint.Image, EditorDescriptorBehavior = EditorDescriptorBehavior.PlaceLast)]
    public class ImageEditorDescriptor : EditorDescriptor
    {
        public override void ModifyMetadata(ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
        {
            base.ModifyMetadata(metadata, attributes);

            metadata.ClientEditingClass = "alloy/editors/imagepreview/ImageContentSelector";
        }
    }
}