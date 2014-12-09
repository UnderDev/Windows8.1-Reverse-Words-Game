using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ReverseApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        DispatcherTimer imgTimer = new DispatcherTimer();

        public MainPage()
        {
            this.InitializeComponent();
            imgTimer.Stop();
        }


        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void animateButton_Click(object sender, RoutedEventArgs e)
        {
            animatedImage_ImageOpened();

            imgTimer.Tick += imgTimer_Tick;
            imgTimer.Interval = new TimeSpan(0, 0, 2);
            imgTimer.Start();
            animateButton.Visibility = Visibility.Collapsed;
        }

        void imgTimer_Tick(object sender, object e)
        {
            Frame.Navigate(typeof(ReverseGame));
            DispatcherTimer imgTimer = (DispatcherTimer)sender;
            imgTimer.Stop();
        }

        public void animatedImage_ImageOpened()
        {
            //Image img = sender as Image;
            var image = animatedImage;
            ShowStoryboard.Begin();
        }
    }
}
