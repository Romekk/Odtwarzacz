using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TworzListaO
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" Start ");
            Console.WriteLine("Tworzenie list odtwarzania");
            TListy();
            Console.WriteLine("Nacisnij Enter");
            Console.ReadLine();
        }

        static void TListy()
        {
            DirectoryInfo directory = new DirectoryInfo(@"B:\works\old_music\poczekalnia\");
            DirectoryInfo[] directories = directory.GetDirectories();
                                   
            foreach (DirectoryInfo directoryInfo in directories)
            {
                using (FileStream fs = File.Create(directoryInfo.FullName+".wpl"))
                {
                    DirectoryInfo aktdir = new DirectoryInfo(directory + directoryInfo.Name);
                    AddText(fs, "<?wpl version = \"1.0\" ?> \r\n");
                    AddText(fs,"<smil>\r\n");
                    AddText(fs, "   <head>\r\n");
                    AddText(fs, "   </head>\r\n");
                    AddText(fs, "   <body>\r\n");
                    AddText(fs, "       <seq>\r\n");
                    string tempath = System.IO.Path.Combine(directoryInfo.FullName);
                    FileInfo[] nplik = aktdir.GetFiles("*.mp3",SearchOption.AllDirectories);
                        foreach (FileInfo file in nplik)
                        {
                        AddText(fs, "          <media src=\"" + file.FullName+"\"/>\r\n"); 
                        }
                    AddText(fs, "       </seq>\r\n");
                    AddText(fs, "   </body>\r\n");
                    AddText(fs, "</smil>\r\n");
                    
                }
                Console.WriteLine(directoryInfo.FullName);
            }
        }

        private static void AddText(FileStream file, string value) {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            file.Write(info, 0, info.Length);
        }


    }
}
