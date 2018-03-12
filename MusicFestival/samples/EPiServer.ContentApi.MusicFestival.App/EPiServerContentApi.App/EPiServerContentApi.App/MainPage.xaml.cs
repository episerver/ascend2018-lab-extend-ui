using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EPiServerContentApi.App
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            
            InitializeComponent();
            try
            {
                InitializeComponent();
                try
                {
                    var htmlFileLocation = SiteManager.GetSitePath().GetAwaiter().GetResult();

                    Browser.Navigating += (s, e) =>
                    {
                        //open external links that arent youtube in new window
                        if (e.Url.StartsWith("http") && !e.Url.Contains("youtube"))
                        {
                            try
                            {
                                var uri = new Uri(e.Url);
                                Device.OpenUri(uri);
                            }
                            catch (Exception)
                            {
                            }

                            e.Cancel = true;
                        }
                    };

                    Browser.Source = new UrlWebViewSource { Url = $"file://{htmlFileLocation}" };

                }
                catch (Exception e)
                {

                }

            }
            catch (Exception e)
            {

            }
        }
    }
}
