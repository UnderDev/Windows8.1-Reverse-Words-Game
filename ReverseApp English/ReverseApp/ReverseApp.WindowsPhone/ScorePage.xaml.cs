using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace ReverseApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ScorePage : Page
    {
        StorageFolder folderLocal;
        StorageFolder folderRoaming;
        StorageFile fileLocal;

        string fileContents = "0";
        string fileContents2;

        public ScorePage()
        {
            this.InitializeComponent();
            folderLocal = ApplicationData.Current.LocalFolder;
            folderRoaming = ApplicationData.Current.RoamingFolder;

            AddFile();
            if (App.iCountScore > App.iHighScore)
            {
                App.iHighScore = App.iCountScore;
                fileContents = Convert.ToString(App.iCountScore);
                AddFile();
            }
        }//ScorePage

        private async void AddFile()
        {
            Boolean check = false;

            /*
             * Try to create a file from (App.fileName)
             * Write to that file whats stored in var fileContents
            */
            try
            {
                fileLocal = await folderLocal.CreateFileAsync(App.fileName);
                check = false;
                await FileIO.WriteTextAsync(fileLocal, fileContents);
            }
            //If file is there throw an exception
            catch (Exception)
            {
                check = true;
            }


            if (check)
            {
                //get the file and read in its contents, and store in var textLocal
                fileLocal = await folderLocal.GetFileAsync(App.fileName);
                string textLocal = await FileIO.ReadTextAsync(fileLocal);

                //convert the contents of the 2 strings into Ints
                fileContents2 = textLocal;
                int num1, num2;
                num1 = Convert.ToInt32(fileContents2);
                num2 = Convert.ToInt32(fileContents);

                //if the number pulled from file is < the current score, Write to the file the Current score
                if (num1 < num2)
                {
                    await FileIO.WriteTextAsync(fileLocal, fileContents);
                }
            }
            ReadFile();
        }

        private async void ReadFile()
        {
            fileLocal = await folderLocal.GetFileAsync(App.fileName);

            string textLocal = await FileIO.ReadTextAsync(fileLocal);
            fileContents = textLocal;

            //output to TextBlock the Highscore
            resultsBox.Text = Convert.ToString(fileContents);
            fileLocal = null;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void checkBtn_Click(object sender, RoutedEventArgs e)
        {
            Array.Clear(App.sListWords, 0, App.sListWords.Length);

            App.iCountPlays = 0;
            App.iCountScore = 0;
            App.ReadFromFile();

            Frame.Navigate(typeof(MainPage));
        }
    }//end partial class 
}//end name space
