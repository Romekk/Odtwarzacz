using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.IO;

namespace KopiujMuzyke
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string source = @"B:\works\old_music\4";
        string target = @"B:\works\old_music\poczekalnia\";
        string dokd;
        int ilskopiowanych = 0;
        long dlugoscplk = 0;
        int nrfolderu = 36;
        bool newfolder = true;

        public MainWindow()
        {
            InitializeComponent();
            dokd = target +nrfolderu;
            LabSource.Content = "Żródło: " + source;
            PrzypiszDebug("Program nie rozpoczał działania. Naciśnij przycisk Uruchom");
        }

        private void Createnewfolder(string dkd)
        {
            if (!Directory.Exists(dkd))
            {
                Directory.CreateDirectory(dkd);
            }

        }
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            StartButton.Content = "Kopiuje";
            Createnewfolder(dokd);
            WykonajKopie(source,"");
            PrzypiszDebug("Program zakończył działanie");
        }

        private void WykonajKopie(string skad, string folder)
        {
            DirectoryInfo directory = new DirectoryInfo(skad);
            DirectoryInfo[] directories = directory.GetDirectories();
            
            FileInfo[] files = directory.GetFiles();
            if (newfolder)
            {
                newfolder = false;
                dlugoscplk = 0;
                nrfolderu++;
                dokd = target + nrfolderu;
            }

            foreach (FileInfo file in files)
            {
                if (file.Extension ==".mp3") {
                    string tempath = System.IO.Path.Combine(dokd, folder, file.Name);
                    PrzypiszDebug("Kopiuje plik " + tempath);
                    Createnewfolder(System.IO.Path.Combine(dokd, folder));
                    file.CopyTo(tempath, true);
                    ilskopiowanych++;
                    dlugoscplk += file.Length;
                    // zakładam że 2godziny to 200 MB, może wprzyszłości uda mi się dokładniej policzyć długośc utworów
                    if (dlugoscplk > 200000000) newfolder = true; 
                    ilplLab.Content = "Ilość skopiowanych utworów: " + ilskopiowanych;
                }
                
            }

            foreach (DirectoryInfo subdirectory in directories)
            {
                WykonajKopie(subdirectory.FullName, subdirectory.Name);
            }

        }


        private void PrzypiszDebug(string Dstring)
        {
            DebugTextBox.Text = Dstring;
        }

    }
}
