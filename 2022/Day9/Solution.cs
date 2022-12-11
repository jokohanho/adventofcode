using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day9
{
    class Program
    {
        static void Main()
        {
            List<string> list = new List<string>();
            using (var reader = new StreamReader(@"C:\Dev\Adventofcode\day9.csv"))
            {

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    list.Add(line);
                }
            }
            Console.WriteLine("SolvePartOne: " + SolvePartOne(list, 2));
            Console.WriteLine("SolvePartTwo: " + SolvePartTwo(list,10));
        }

        static int SolvePartOne(List<string> list, int knots)
        {
            List<string> taildVisitedLocaitons = new List<string>();
            List<int[]> knotsList = new List<int[]>();
            bool isHead = false;
            for (int k = 0; k < knots; k++)
            {
                knotsList.Add(new int[] { 1, 1 });
            }

            taildVisitedLocaitons.Add(knotsList[knotsList.Count - 1][0] + "," + knotsList[knotsList.Count - 1][1]);

            foreach (var item in list)
            {
                string[] motion = item.Split(" ");
                for (int m = 0; m < int.Parse(motion[1]); m++)
                {
                    for (int k = 0; k < knotsList.Count() - 1; k++)
                    {
                        int[] headCurrentLocation = knotsList[k];
                        int[] tailCurrentLocation = knotsList[k + 1];
                        if (k == 0)
                        {
                            isHead = true;
                        }
                        else
                        {
                            isHead = false;
                        }
                        (int[] firstKnotLocation, int[] secondKnotLocation) = getMotionPattern(motion[0], headCurrentLocation, tailCurrentLocation, isHead);
                        knotsList[k] = firstKnotLocation;
                        knotsList[k + 1] = secondKnotLocation;
                        if (!taildVisitedLocaitons.Contains(knotsList[knotsList.Count() - 1][0] + "," + knotsList[knotsList.Count() - 1][1]))
                        {
                            taildVisitedLocaitons.Add(knotsList[knotsList.Count() - 1][0] + "," + knotsList[knotsList.Count() - 1][1]);
                        }
                    }
                }
            }

            return taildVisitedLocaitons.Count;
        }
        static int SolvePartTwo(List<string> list, int knots)
        {
            List<string> taildVisitedLocaitons = new List<string>();
            List<int[]> knotsList = new List<int[]>();
            bool isHead = false;
            for (int k = 0; k < knots; k++)
            {
                knotsList.Add(new int[] { 1, 1 });
            }
            taildVisitedLocaitons.Add(knotsList[knotsList.Count - 1][0] + "," + knotsList[knotsList.Count - 1][1]);
            foreach (var item in list)
            {
                string[] motion = item.Split(" ");
                for (int m = 0; m < int.Parse(motion[1]); m++)
                {
                    for (int k = 0; k < knotsList.Count() - 1; k++)
                    {
                        int[] headCurrentLocation = knotsList[k];
                        int[] tailCurrentLocation = knotsList[k + 1];
                        if (k == 0)
                        {
                            isHead = true;
                        }
                        else
                        {
                            isHead = false;
                        }
                        (int[] firstKnotLocation, int[] secondKnotLocation) = getMotionPattern(motion[0], headCurrentLocation, tailCurrentLocation, isHead);
                        knotsList[k] = firstKnotLocation;
                        knotsList[k + 1] = secondKnotLocation;
                        if (!taildVisitedLocaitons.Contains(knotsList[knotsList.Count() - 1][0] + "," + knotsList[knotsList.Count() - 1][1]))
                        {
                            taildVisitedLocaitons.Add(knotsList[knotsList.Count() - 1][0] + "," + knotsList[knotsList.Count() - 1][1]);
                        }
                    }
                }
            }
            return taildVisitedLocaitons.Count;
        }

        static int SolvePartTwo(List<string> list)
        {
            return 1;
        }

        static (int[] firstKnotLocation, int[] secondKnotLocation) getMotionPattern(string direction, int[] firstKnotLocation, int[] secondKnotLocation, bool isHead)
        {
            int[] headCurrentLocation = firstKnotLocation;
            int[] tailCurrentLocation = secondKnotLocation;
            if (isHead)
            { 
                switch (direction)
                {
                    case "R":
                        headCurrentLocation[0] = headCurrentLocation[0] + 1;
                        break;
                    case "U":
                        headCurrentLocation[1] = headCurrentLocation[1] + 1;
                        break;
                    case "L":
                        headCurrentLocation[0] = headCurrentLocation[0] - 1;
                        break;
                    case "D":
                        headCurrentLocation[1] = headCurrentLocation[1] - 1;
                        break;
                }
            }
            //right-up
            if ((tailCurrentLocation[0] < headCurrentLocation[0] && tailCurrentLocation[1] < headCurrentLocation[1]) && ((headCurrentLocation[0] - tailCurrentLocation[0]) > 1 || (headCurrentLocation[1] - tailCurrentLocation[1]) > 1))
            {
                tailCurrentLocation[0] = tailCurrentLocation[0] + 1;
                tailCurrentLocation[1] = tailCurrentLocation[1] + 1;
            }
            //right-down
            else if((tailCurrentLocation[0] < headCurrentLocation[0] && tailCurrentLocation[1] > headCurrentLocation[1]) && ((headCurrentLocation[0] - tailCurrentLocation[0]) > 1 || (tailCurrentLocation[1] - headCurrentLocation[1]) > 1))
            {
                tailCurrentLocation[0] = tailCurrentLocation[0] + 1;
                tailCurrentLocation[1] = tailCurrentLocation[1] - 1;
            }
            //left-up 
            else if ((tailCurrentLocation[0] > headCurrentLocation[0] && tailCurrentLocation[1] < headCurrentLocation[1]) && ((tailCurrentLocation[0] - headCurrentLocation[0]) > 1 || (headCurrentLocation[1] - tailCurrentLocation[1]) > 1))
            {
                tailCurrentLocation[0] = tailCurrentLocation[0] - 1;
                tailCurrentLocation[1] = tailCurrentLocation[1] + 1;
            }
            //left-down 
            else if ((tailCurrentLocation[0] > headCurrentLocation[0] && tailCurrentLocation[1] > headCurrentLocation[1]) && ((tailCurrentLocation[0] - headCurrentLocation[0]) > 1 || (tailCurrentLocation[1] - headCurrentLocation[1]) > 1))
            {
                tailCurrentLocation[0] = tailCurrentLocation[0] - 1;
                tailCurrentLocation[1] = tailCurrentLocation[1] - 1;
            }
            //right
            else if (tailCurrentLocation[1] == headCurrentLocation[1] && (headCurrentLocation[0] - tailCurrentLocation[0]) > 1)
            {
                tailCurrentLocation[0] = tailCurrentLocation[0] + 1;
            }
            //left
            else if (tailCurrentLocation[1] == headCurrentLocation[1] && (tailCurrentLocation[0] - headCurrentLocation[0]) > 1)
            {
                tailCurrentLocation[0] = tailCurrentLocation[0] - 1;
            }
            //up
            else if (tailCurrentLocation[0] == headCurrentLocation[0] && (headCurrentLocation[1] - tailCurrentLocation[1]) > 1)
            {
                tailCurrentLocation[1] = tailCurrentLocation[1] + 1;
            }
            //down
            else if (tailCurrentLocation[0] == headCurrentLocation[0] && (tailCurrentLocation[1] - headCurrentLocation[1]) > 1)
            {
                tailCurrentLocation[1] = tailCurrentLocation[1] - 1;
            }

            return (headCurrentLocation, tailCurrentLocation);
        }
    }
}
