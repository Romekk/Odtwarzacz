using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace readwrite
{
    class Program
    {
        static void Main(string[] args)
        {

            int counter = 0;
            string line;

            // Read the filereadread and display it line by line.  
            System.IO.StreamReader fileread = new System.IO.StreamReader(@"B:\works\old_music\poczekalnia\03.wpl");
            System.IO.StreamWriter filewrite = new System.IO.StreamWriter("test.txt");
            while ((line = fileread.ReadLine()) != null)
            {
                System.Console.WriteLine(line);
                filewrite.WriteLine(line);
                counter++;
            }

            fileread.Close();
            filewrite.Close();
            System.Console.WriteLine("There were {0} lines.", counter);

        }
    }

}
