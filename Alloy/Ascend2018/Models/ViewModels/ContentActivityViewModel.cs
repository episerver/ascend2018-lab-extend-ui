using System.Collections.Generic;
using EPiServer.DataAbstraction.Activities;

namespace Ascend2018.Models.ViewModels
{
    /// <summary>
    /// Holds a list of <see cref="ContentApprovalActivity"/> for the Approval Log view.
    /// </summary>
    public class ContentActivityViewModel
    {
        public IEnumerable<ContentApprovalActivity> Activities { get; set; }
    }
}