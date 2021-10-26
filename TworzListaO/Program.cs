using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
// Program tworzy listy *.wpl z n mojejmuzyki z folderu B:\works\old_music\kopia
namespace TworzListaO
{
    class Program
    {
        
          
        
        static void Main(string[] args)
        {
            string nameoffolderstartowy = @"B:\works\old_music\kopia";
            DirectoryInfo sfolder= new DirectoryInfo(nameoffolderstartowy);
            Console.WriteLine(" Start ");
            Console.WriteLine("Tworzenie list odtwarzania");
            TListy(sfolder);
            Console.WriteLine("Nacisnij Enter");
            Console.ReadLine();
        }


        static void TListy(DirectoryInfo folder ) {
            string dest = @"B:\works\old_music\listy\";
            foreach (DirectoryInfo Subdir in folder.EnumerateDirectories())     {
                int i = 0;
                Console.WriteLine("Full name: "+Subdir.FullName+"; Name"+Subdir.Name+" ;"+Subdir.Parent);
                var filei = Subdir.EnumerateFiles("*.mp3");
                foreach (FileInfo fi in filei) {
                    Console.WriteLine(fi.Name);
                    i++;
                }
                if (i > 0) {
                    using (FileStream fs = File.Create(dest + Subdir.Parent+"_"+ Subdir.Name + ".wpl"))
                    {
                        AddText(fs, "<?wpl version = \"1.0\" ?> \r\n");
                        AddText(fs, "<smil>\r\n");
                        AddText(fs, "   <head>\r\n");
                        AddText(fs, "   </head>\r\n");
                        AddText(fs, "   <body>\r\n");
                        AddText(fs, "       <seq>\r\n");
                        foreach (FileInfo fi in filei)
                        {
                            AddText(fs, "<media src=\""+fi.FullName + "\"/>\r\n");
                        }
                        AddText(fs, "       </seq>\r\n");
                        AddText(fs, "   </body>\r\n");
                        AddText(fs, "</smil>\r\n");
                    }
                }
                TListy(Subdir);
            }
        }

        private static void AddText(FileStream file, string value) {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            file.Write(info, 0, info.Length);
        }

        private void Utworzwpl(DirectoryInfo folder)
        {

            
        }

    }
}
