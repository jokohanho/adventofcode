using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Http;

namespace Day4
{
    class Program
    {
        static void Main()
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

            var partOne = solvePartOne(listA);
            Console.WriteLine("Total fullyContainSections: " + partOne);
            var partOneV2 = solvePartOneV2(listA);
            Console.WriteLine("Total fullyContainSectionsV2: " + partOneV2);
            var partTwo = solvePartTwo(listA);
            Console.WriteLine("Total ovelappingSections: " + partTwo);
            var partTwoV2 = solvePartTwoV2(listA);
            Console.WriteLine("Total ovelappingSectionsV2: " + partTwoV2);


        }

        static int solvePartOne(List<string> list)
        {
            int fullyContainsCounter = 0;
            foreach (var item in list)
            {
                var PairOne = combindedSections(getSectionsList(splitHyphen(splitComma(item)[0])));
                var PairTwo = combindedSections(getSectionsList(splitHyphen(splitComma(item)[1])));
                if (getFullyContainsSections(PairOne, PairTwo))
                {
                    fullyContainsCounter++;
                }
            }
            return fullyContainsCounter;
        }

        static int solvePartOneV2(List<string> list)
        {
            int fullyContainsCounter = 0;
            foreach (var item in list)
            {
                var pair = item.Split(",");
                var a = pair[0].Split("-");
                var b = pair[1].Split("-");
                int ax = int.Parse(a[0]);
                int ay = int.Parse(a[1]);
                int bx = int.Parse(b[0]);
                int by = int.Parse(b[1]);

                if(ax <= bx && ay >= by)
                {
                    fullyContainsCounter++;
                }
                else if (bx <= ax && by >= ay)
                {
                    fullyContainsCounter++;
                }
              
            }
            return fullyContainsCounter;
        }
        static int solvePartTwoV2(List<string> list)
        {
            int fullyContainsCounter = 0;
            foreach (var item in list)
            {
                var pair = item.Split(",");
                var a = pair[0].Split("-");
                var b = pair[1].Split("-");
                int ax = int.Parse(a[0]);
                int ay = int.Parse(a[1]);
                int bx = int.Parse(b[0]);
                int by = int.Parse(b[1]);

                if ((ax <= bx && ay >= by) || (ax <= by && ay >= by))
                {
                    fullyContainsCounter++;
                }
                else if ((bx <= ax && by >= ax) || (bx <= ay && by >= ax))
                {
                    fullyContainsCounter++;
                }

            }
            return fullyContainsCounter;
        }

        static int solvePartTwo(List<string> list)
        {
            int fullyContainsCounter = 0;
            foreach (var item in list)
            {
                var PairOneList = getSectionsList(splitHyphen(splitComma(item)[0]));
                var PairTwoList = getSectionsList(splitHyphen(splitComma(item)[1]));
                if (getOverlappingSections(PairOneList, PairTwoList))
                {
                    fullyContainsCounter++;
                }
            }
            return fullyContainsCounter;
        }

        static string[] splitComma(string item)
        {
            return item.Split(",");
        }
        static string[] splitHyphen(string item)
        {
            return item.Split("-");
        }
        static List<string> getSectionsList(string[] items)
        {
            List<string> assignmentsList = new List<string>();
            IEnumerable<int> assignments = Enumerable.Range(int.Parse(items[0]), (int.Parse(items[1]) - int.Parse(items[0]) + 1)).Select(x => x);
            foreach (var item in assignments)
            {
                if(item < 10)
                {
                    assignmentsList.Add("0"+item.ToString());
                }
                else
                {
                    assignmentsList.Add(item.ToString());
                }
            }
            return assignmentsList;
        }
        static string combindedSections(List<string> sectionsList)
        {
            return string.Join("-", sectionsList);
        }
        static Boolean getFullyContainsSections(string sectionOne, string sectionTwo)
        {
            if(sectionOne.Contains(sectionTwo) || sectionTwo.Contains(sectionOne))
            {
                return true;
            }
            return false;
        }
        static Boolean getOverlappingSections(List<string> sectionOne, List<string> sectionTwo)
        {
           
            IEnumerable<string> overlapping = sectionOne.Intersect(sectionTwo);

            if(overlapping.ToList().Count() > 0)
            {
                return true;
            }
            
            return false;
        }
    }
}
