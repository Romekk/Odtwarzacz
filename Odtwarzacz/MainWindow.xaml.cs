using System;
using System.Windows;
using System.Windows.Threading;
using System.IO;
using Microsoft.Win32;
// TODO: dodać guzik szybszego przewijania ( np. 4x)
// TODO: obsłużyć  błąd gdy plik na zerową długość 
// TODO zapisywanie aktulnego pliku i pozycji odtwarzania co np. 5 s
namespace Odtwarzacz
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;
        string plikkonf = "odtwarzacz.cfg";
        bool KlipIsPlaying;
        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            KlipIsPlaying = false;
            if (File.Exists(plikkonf))
            {
                using (StreamReader sw = File.OpenText(plikkonf))
                {
                    string nazwapl = sw.ReadLine();
                    Uri uri;
                    uri = new Uri(nazwapl);
                    Klip.Source = uri;
                    sw.Close();
                }

            }
           
        }

        private void KlipPlay(object sender, RoutedEventArgs e)
        {
            Klip.Play();
            if (timer != null) timer.Start();
            KlipIsPlaying = true;
        }

        private void KlipPause(object sender, RoutedEventArgs e)
        {
            // if Klip.IsMuted  Klip.Pause()
            if (KlipIsPlaying)
            {
                Klip.Pause();
                KlipIsPlaying = false;
                PlayButton.Content = " Wznów ";
            }
            else {
                KlipPlay(sender, e);
                PlayButton.Content = " Pauza    ";
            }

        }

        private void KlipStop(object sender, RoutedEventArgs e)
        {
            Klip.Stop();
            if (timer != null) timer.Stop();
        }

        private void Zaladuj_na_starcie(object sender, RoutedEventArgs e)
        {
            Klip.ScrubbingEnabled = true;
      //      Klip_MediaOpened(sender, e);
            Klip.Stop();
            if (Klip.Source != null) KlipPlay(sender,e);
     
    }

        private void Klip_MediaOpened(object sender, RoutedEventArgs e)
        {
            CzasO.Maximum = Klip.NaturalDuration.TimeSpan.TotalSeconds;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            if (timer != null) timer.Start();
            CalkowityCzas.Content = Klip.NaturalDuration.TimeSpan.ToString(@"hh\:mm\:ss");
        }
        
        private void Timer_Tick(object sender, EventArgs e)
        {
            CzasO.Value = Klip.Position.TotalSeconds;
        }

        private void CzasO_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Klip.Position = TimeSpan.FromSeconds(CzasO.Value);
        }

        private void CzasO_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            Klip.Pause();
            if (timer != null) timer.Stop();
        }

        private void CzasO_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            Klip.Play();
            timer.Start();

                
        }

        public void Wczytajplik(object sender, RoutedEventArgs e)
        {
            Uri uri;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true) {
                uri = new Uri(openFileDialog.FileName);
                Klip.Source = uri;
            }
        }

        private void Klip_MediaEnded(object sender, RoutedEventArgs e)
        {
            using (StreamWriter sw = File.CreateText(plikkonf))
            {
                string name;
                name = Klip.Source.OriginalString;
                sw.WriteLine(name);
                sw.WriteLine(Klip.Position.TotalSeconds);
                sw.Close();
            }
        }

        private void KlipdoPrzodu(object sender, RoutedEventArgs e)
        {
            int pozx;
            pozx = 60 * Klip.Position.Minutes + Klip.Position.Seconds;
            Klip.Position = TimeSpan.FromSeconds(CzasO.Value +60);
            //x = x + 15;
            //Klip.Position = x;
        }

        private void Klip_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show(e.ErrorException.Message+" "+Klip.Name);
        }
    }
}