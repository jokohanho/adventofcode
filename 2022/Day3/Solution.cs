using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Day3
{
    class Program
    {
        static int getPriorityNumber(string item)
        {
            int priorityNumber = 0;
            int priorityNumberCounter = 1;
            char[] az = Enumerable.Range('a', 'z' - 'a' + 1).Select(i => (Char)i).ToArray();
            foreach (var c in az)
            {
                if ((c).ToString() == item)
                {
                    priorityNumber = priorityNumberCounter;
                    break;
                }
                priorityNumberCounter++;
            }
            if (priorityNumber == 0)
            {
                char[] AZ = Enumerable.Range('A', 'Z' - 'A' + 1).Select(i => (Char)i).ToArray();
                foreach (var c in AZ)
                {
                    if ((c).ToString() == item)
                    {
                        priorityNumber = priorityNumberCounter;
                        break;
                    }
                    priorityNumberCounter++;
                }
            }
            return priorityNumber;
        }
        static int getPriorityNumberTest(string item)
        {
            int index = 0; 
            if (item == item.ToUpper())
            {
                index = char.Parse(item) - 38;
            }
            else
            {
                index = char.Parse(item) - 96;
            }
            var priorityNumber = index;
            return priorityNumber;
        }
        static string findDuplicatedItem(string inventory)
        {
            string duplicatedItem = "";
            string firsthalf = inventory.Substring(0, inventory.Length / 2);
            string secondhalf = inventory.Substring(firsthalf.Length, firsthalf.Length);
            foreach (var Char in firsthalf.ToCharArray().Distinct())
            {
                if (secondhalf.Contains(Char))
                {
                    duplicatedItem = (Char).ToString();
                    break;
                }
            }
            return duplicatedItem;
        }
        static string findDuplicatedItemTest(string inventory)
        {
            string duplicatedItem = "";
            string firsthalf = inventory.Substring(0, inventory.Length / 2);
            string secondhalf = inventory.Substring(firsthalf.Length, firsthalf.Length);

            IEnumerable<char> duplicated = firsthalf.ToCharArray().Distinct().Intersect(secondhalf.ToCharArray().Distinct());
            foreach (char C in duplicated)
            {
                duplicatedItem = C.ToString();
            }
            return duplicatedItem;
        }
        static string findBadge(List<string> inventories)
        {
            string badge = "";
            foreach (var Char in inventories[0].ToCharArray().Distinct())
            {
                if (inventories[1].Contains(Char) && inventories[2].Contains(Char))
                {
                    badge = (Char).ToString();
                    break;
                }
            }
            return badge;
        }

        static string findBadgeV2(List<string> inventories)
        {
            string badge = "";
            List<List<char>> inventoryLists = new List<List<char>>();
            List<char> CharList = new List<char>();
            foreach (string item in inventories)
            {
                foreach (char C in item.ToCharArray().Distinct())
                {
                    CharList.Add(C);
                }
                inventoryLists.Add(new List<char> (CharList));
                CharList.Clear();
            }
            List<char> res = inventoryLists.Aggregate<IEnumerable<char>>((a, b) => a.Intersect(b)).ToList();
            foreach (var item in res)
            {
                badge = item.ToString();
            }
            return badge;
        }
        static int solvePart1(List<string> list)
        {
            int sumPriority = 0;
            foreach (var item in list)
            {
                var duplicatedItem = findDuplicatedItemTest(item);
                
                var priorityNumber = getPriorityNumberTest(duplicatedItem);
                sumPriority = sumPriority + priorityNumber;
            }
            return sumPriority;
        }
        static int solvePart2(List<string> list)
        {
            int sumPriority = 0;
            List<string> inventories = new List<string>();
            foreach (var item in list)
            {
                inventories.Add(item);
                if (inventories.Count == 3)
                {
                    var badge = findBadge(inventories);
                    inventories.Clear();
                    var priorityNumber = getPriorityNumber(badge);
                    sumPriority = sumPriority + priorityNumber;
                }
            }
            return sumPriority;
        }

        static int solvePart2V2(List<string> list)
        {
            int sumPriority = 0;
            List<string> inventories = new List<string>();
            foreach (var item in list)
            {
                inventories.Add(item);
                if (inventories.Count == 3)
                {
                    var badge = findBadgeV2(inventories);
                    inventories.Clear();
                    var priorityNumber = getPriorityNumberTest(badge);
                    sumPriority = sumPriority + priorityNumber;
                }
            }
            return sumPriority;
        }
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
            var part1 = solvePart1(listA);
            Console.WriteLine("Sum of the priorities from part 1: " + part1);
            var part2 = solvePart2(listA);
            Console.WriteLine("Sum of the priorities from part 2: " + part2);
            var part2V2 = solvePart2V2(listA);
            Console.WriteLine("Sum of the priorities from part 2 v2: " + part2V2);
        }
    }
}
