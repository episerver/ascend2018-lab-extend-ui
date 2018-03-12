using System;
using System.Collections.Generic;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;

namespace Ascend2018.Business.EditorDescriptors
{
    /// <summary>
    /// Adds a new editor for strings, available as a UIHint.
    /// Just add [UIHint(MarkdownEditorDescriptor.UIHint)] to model string properties that should use Markdown.
    /// </summary>
    [EditorDescriptorRegistration(
        TargetType = typeof(string),
        UIHint = UIHint,
        EditorDescriptorBehavior = EditorDescriptorBehavior.PlaceLast)]
    public class MarkdownEditorDescriptor : EditorDescriptor
    {
        public const string UIHint = "Markdown";

        public override void ModifyMetadata(ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
        {
            base.ModifyMetadata(metadata, attributes);

            metadata.ClientEditingClass = "alloy/editors/markdowneditor/Editor";
        }
    }
}