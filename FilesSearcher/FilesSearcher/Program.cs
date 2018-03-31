﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FilesSearcher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter directory");
            string directory = Console.ReadLine();
            Console.WriteLine("Pleas enter file extension");
            string extension = Console.ReadLine();
            string path, ext;

            //need to replace "C:/" and "sys" by readline
            path = @directory;
            ext = "*." + extension;

            //searching all files info in selected directory with selected extension
            try
            {
                FileSystemInfo[] files = new DirectoryInfo(path).GetFileSystemInfos(ext);
                DateTime time = new DateTime(1900, 1, 1, 1, 1, 1, 1);
                DateTime x = time;
                FileSystemInfo LastCreatedFile = files[0];

                //selecting last created file
                foreach (FileSystemInfo file in files)
                {
                    if (x < file.CreationTime)
                    {
                        x = file.CreationTime;
                        LastCreatedFile = file;
                    }
                }

                Console.WriteLine("Last created file:");
                Console.WriteLine("{0} was created - {1}", LastCreatedFile.Name, LastCreatedFile.CreationTime);
                Console.WriteLine();
                Console.WriteLine("Recently created files:");

                int count = 1;
                for (int i = 0; i < files.Length; i++)
                {
                    //exclude last created file
                    if (files[i].Name != LastCreatedFile.Name)
                    {
                        //decrease creation date of the last created file by 10 seconds 
                        //and compare it to all files in massive
                        if (LastCreatedFile.CreationTime.AddSeconds(-10) <= files[i].CreationTime)
                        {
                            Console.WriteLine("{0}. {1} was created - {2}", count, files[i].Name, files[i].CreationTime);
                            count++;
                        }
                    }
                }
            }

            catch (System.IO.DirectoryNotFoundException)
            {
                Console.WriteLine("*********************************");
                Console.WriteLine("Entered directory doesn't exist");
                Console.WriteLine("*********************************");
            }
        }
    } 
}
