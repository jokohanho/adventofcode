using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Day7
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> listA = new List<string>();
            using (var reader = new StreamReader(@"input.in"))
            {

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();


                    listA.Add(line);

                }
            }



            foreach (var item in listA)
            {
                if (item.StartsWith("$ cd .."))
                {
                    Console.WriteLine("Going back one dir");
                }
                else if (item.StartsWith("$ cd "))
                {
                    Console.WriteLine("CD to " + item.Substring(5));
                }
                else if (item.StartsWith("$ ls"))
                {
                    Console.WriteLine("Listing content");
                }
                else if (item.StartsWith("dir"))
                {
                    Console.WriteLine(item);
                }
                else if (Regex.IsMatch(item.Substring(0, 1), @"^\d+$"))
                {
                    Console.WriteLine(item);
                }
                // Console.WriteLine(item);
            }


        }




    }
}

