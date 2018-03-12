using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(WebView), typeof(EPiServerContentApi.App.iOS.Renderers.WebViewRenderer))]

namespace EPiServerContentApi.App.iOS.Renderers
{
    internal class WebViewRenderer : Xamarin.Forms.Platform.iOS.WebViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            var view = NativeView as UIKit.UIWebView;

            if (view != null)
            {
                view.ScrollView.ScrollEnabled = true;
                view.ScrollView.Bounces = false;
            }
        }
    }
}
