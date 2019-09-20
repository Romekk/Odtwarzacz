using System;
using System.IO;
using System.Text;

namespace zapisutf
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 0;
            string lan;
            Console.WriteLine("Hello World!");
            using (FileStream fs = File.Create("test.txt"))
            {
                while (i < 5)
                {
                    Console.Write(i + " Podaj tekst ");
                    i++;
                    lan = Console.ReadLine();
                    WriteText(fs, lan+"\r\n");
                }
            }

        }

        private static void WriteText(FileStream file, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            file.Write(info, 0, info.Length);
        }

        private static void ReadText(FileStream file, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            file.Read(info, 0, info.Length);
        }


    }
}
