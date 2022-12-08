using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;


namespace Day6
{
    class Program
    {
        static void Main()
        {
            List<string> messageList = new List<string>();
            using (var reader = new StreamReader(@"input.in"))
            {

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    messageList.Add(line);
                }
            }
            var input = "";
            foreach (var mess in messageList)
            {
                input = mess;
            }

            int markerSize = 14;
            for (int i = 0; i < input.Length; i++)
            {
                bool isDistinct = input.Substring(i, markerSize).Distinct().Count() == input.Substring(i, markerSize).Length;
                if (isDistinct)
                {
                    Console.WriteLine(i+ markerSize);
                    break;
                }
            }
        }
    }
}
