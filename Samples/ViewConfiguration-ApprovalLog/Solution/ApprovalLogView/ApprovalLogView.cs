using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Shell;

namespace Ascend2018.ApprovalLogView
{
    [ServiceConfiguration(typeof(ViewConfiguration))]
    public class ApprovalLogView : ViewConfiguration<IContentData>
    {
        public const string ViewKey = "approvalLog";

        public ApprovalLogView()
        {
            Key = ViewKey;
            Name = "Approval Audit Log";
            Description = "Approval Audit Log";
            IconClass = "epi-iconList";

            ControllerType = "epi-cms/widget/IFrameController";
            ViewType = "/ApprovalLog/";
        }
    }
}
