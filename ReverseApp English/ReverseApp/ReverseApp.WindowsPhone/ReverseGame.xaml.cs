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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace ReverseApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ReverseGame : Page
    {

        public static int iWord;
        public static string sWords, sReverseWord;
        private int countTicks;

        DispatcherTimer myTimer = new DispatcherTimer();
        DispatcherTimer CountDownTimer = new DispatcherTimer();

        //user input
        public String s;

        //input from file
        public String p;

        public ReverseGame()
        {
            this.InitializeComponent();

            App.load();

            //Waits till all words from the file are put into the array
            while (App.check == false)
            {
            }
            randWordFromFile();

            //show user the score            
            outbox.Text = Convert.ToString(App.iCountScore);
        }

        private void txtUserInput_GotFocus(object sender, RoutedEventArgs e)
        {
            txtUserInput.SelectAll();
        }


        private void checkBtn_Click(object sender, RoutedEventArgs e)
        {

            //user input
            s = txtUserInput.Text;

            //ignore case of input
            if (s.Equals(p, StringComparison.OrdinalIgnoreCase))
            {
                App.iCountPlays++;
                App.iCountScore++;

                myTimer.Stop();
                CountDownTimer.Stop();

                Frame.Navigate(typeof(ReverseGame));
            }

            else
            {
                CountDownTimer.Stop();
                myTimer.Stop();

                Frame.Navigate(typeof(ScorePage));
            }
        }//end

        public void randWordFromFile()
        {
            var r = new Random();

            if (App.iCountPlays <= 1)
            {
                //Picks a number between 0 and 694 (EASY)
                iWord = r.Next(App.iNumOfWords);

            }
            else if (App.iCountPlays <= 2)
            {
                //Picks a number between 694 and 1271 (MEDIUM)
                iWord = r.Next(App.iNumOfWords, App.iTotal_Easy_Med_Words);
            }
            else
            {
                //Picks a number between 1271 and 1460 (HARD)
                iWord = r.Next(App.iTotal_Easy_Med_Words, App.iTotal_Words);
            }

            sWords = (App.sListWords[iWord]);

            char[] cArray = sWords.ToCharArray();
            sReverseWord = String.Empty;

            for (int i = cArray.Length - 1; i > -1; i--)
            {
                //storing the reversed string in sReverseWord
                sReverseWord += cArray[i];
            }

            //output the word
            reversed_textBlock.Text = (sReverseWord);

            p = sWords;

            TimeLimit();
        }//end

        //Start a timer 
        public void TimeLimit()
        {
            myTimer.Tick += myTimer_Tick;

            if (App.iCountPlays <= 1)
            {
                //timers time left
                countTicks = 15;
                //timer limit 15+ 2 sec from animation at start
                myTimer.Interval = new TimeSpan(0, 0, 17);
            }
            else
            {
                //timers time left
                countTicks = 10;
                //timer limit 10 + 2 sec from animation at start
                myTimer.Interval = new TimeSpan(0, 0, 12);
            }

            CountDownTimer.Tick += countDown_Timer;
            CountDownTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);

            myTimer.Start();
            CountDownTimer.Start();
        }

        private void countDown_Timer(object sender, object e)
        {
            //display to user the COUNTDOWN timer
            time_Box.Text = countTicks--.ToString();
            DispatcherTimer CountDownTimer = (DispatcherTimer)sender;

            if (countTicks == -1)
            {
                CountDownTimer.Stop();
                myTimer.Stop();
                //WHEN TIMES UP GO TO THIS PAGE
                Frame.Navigate(typeof(ScorePage));
            }
        }//End

        //EVENT 
        public void myTimer_Tick(object sender, object e)
        {
            DispatcherTimer myTimer = (DispatcherTimer)sender;
            CountDownTimer.Stop();
            myTimer.Stop();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }
    }
}
