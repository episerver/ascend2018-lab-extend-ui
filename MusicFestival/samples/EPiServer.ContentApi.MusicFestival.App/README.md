Music Festival Xamarin App
===========

The Music Festival Xamarin App project is an application designed to render the [Vue Music Festival application](../EPiServer.ContentApi.MusicFestival.Frontend/README.md) as a native app on Android and iOS devices. It contains three project:

* EPiServerContentApi.App
* EPiServerContentApi.Android
* EPiServerContentApi.iOS

The EPiServerContentApi.App project contains the main code for the application, while the other projects contain device specific implementations and settings. Within EPiServerContentApi.App, there is a "Site" folder that contains a dist.zip file. That zip file contains the Vue site. On startup, the app unzips the file, stores its contents in a local folder, and runs a webview that points to that app folder.

## Quick Start ##

First go to the [EPiServer.ContentApi.MusicFestival.Frontend](../EPiServer.ContentApi.MusicFestival.Frontend/README.md) and follow the build steps. This will generate the dist.zip file that you will need to add to your Site folder in the EPiServerContentApi.App project.

Once the dist.zip file is in the project, you can run your application. For information on running your Xamarin application, you can check out the Xamarin documentation for deploying to both [Android](https://developer.xamarin.com/guides/android/deployment,_testing,_and_metrics/) and [iOS](https://developer.xamarin.com/guides/ios/deployment,_testing,_and_metrics/)
