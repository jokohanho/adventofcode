using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;


namespace Day5
{
    class Program
    {
        static void Main()
        {
            List<string> movesList = new List<string>();
            using (var reader = new StreamReader(@"input-moves.in"))
            {

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    movesList.Add(line);
                }
            }
            List<string> stacksList = new List<string>();
            using (var reader = new StreamReader(@"input-stacks.in"))
            {

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    stacksList.Add(line + " ");
                }
            }
            var cratesOnTopOne = solvePartOne(stacksList, movesList);
            Console.WriteLine(cratesOnTopOne);
            var cratesOnTopTwo = solvePartTwo(stacksList, movesList);
            Console.WriteLine(cratesOnTopTwo);
        }

        static string solvePartOne(List<string> stacksList, List<string> movesList)
        {
            List<List<string>> stacks = getStacksList(stacksList);
            List<int> actions = new List<int>();
            List<string> crates = new List<string>();
            foreach (var action in movesList)
            {
                string x = action;
                int pFrom = x.IndexOf("move ") + "move ".Length;
                int pTo = x.LastIndexOf(" from");
                int move = int.Parse(x.Substring(pFrom, pTo - pFrom));
                actions.Add(move);
                pFrom = x.IndexOf("from ") + "from ".Length;
                pTo = x.LastIndexOf(" to");
                int from = int.Parse(x.Substring(pFrom, pTo - pFrom));
                actions.Add(from);
                pFrom = x.IndexOf("to ") + "to ".Length;
                pTo = x.Length;
                int to = int.Parse(x.Substring(pFrom, pTo - pFrom));
                actions.Add(to);
                foreach (var item in stacks[actions[2] - 1])
                {
                    crates.Add(item);
                }
                for (int i = 0; i < actions[0]; i++)
                {
                    crates.Add(stacks[actions[1] - 1][stacks[actions[1] - 1].Count - 1 - i]);
                }
                stacks[actions[2]-1] = new List<string>(crates);
                for (int i = 0; i < actions[0]; i++)
                {
                    stacks[actions[1] - 1].RemoveAt(stacks[actions[1] - 1].Count - 1);
                }
                crates.Clear();
                actions.Clear();
            }
            string lastCrates = "";
            char[] charsToTrim = { '[', ' ', ']' };
            foreach (var c in stacks)
            {
                lastCrates += c.Last().Trim(charsToTrim);
            }
                return lastCrates;
        }

        static string solvePartTwo(List<string> stacksList, List<string> movesList)
        {
            List<List<string>> stacks = getStacksList(stacksList);
            List<int> actions = new List<int>();
            List<string> crates = new List<string>();
            foreach (var action in movesList)
            {
                string x = action;
                int pFrom = x.IndexOf("move ") + "move ".Length;
                int pTo = x.LastIndexOf(" from");
                int move = int.Parse(x.Substring(pFrom, pTo - pFrom));
                actions.Add(move);
                pFrom = x.IndexOf("from ") + "from ".Length;
                pTo = x.LastIndexOf(" to");
                int from = int.Parse(x.Substring(pFrom, pTo - pFrom));
                actions.Add(from);
                pFrom = x.IndexOf("to ") + "to ".Length;
                pTo = x.Length;
                int to = int.Parse(x.Substring(pFrom, pTo - pFrom));
                actions.Add(to);
                foreach (var item in stacks[actions[2] - 1])
                {
                    crates.Add(item);
                }
                for (int i = actions[0]; i > 0; i--)
                {
                    crates.Add(stacks[actions[1] - 1][stacks[actions[1] - 1].Count - i]);
                }
                stacks[actions[2] - 1] = new List<string>(crates);
                for (int i = 0; i < actions[0]; i++)
                {
                    stacks[actions[1] - 1].RemoveAt(stacks[actions[1] - 1].Count - 1);
                }
                crates.Clear();
                actions.Clear();
            }
            string lastCrates = "";
            char[] charsToTrim = { '[', ' ', ']' };
            foreach (var c in stacks)
            {
                lastCrates += c.Last().Trim(charsToTrim);
            }
            return lastCrates;
        }

        static List<List<string>> getStacksList(List<string> stacksList)
        {
            List<List<string>> stacks = new List<List<string>>();
            List<string> crates = new List<string>();
            for (var i = stacksList.Count - 1; i >= 0; i--)
            {
                int crateLength = 4;
                for (int y = 0; y < stacksList[i].Length / crateLength; y++)
                {
                    var crate = stacksList[i].Substring(y * crateLength, crateLength);
                    if (crate.StartsWith("["))
                    {
                        if (stacks.ElementAtOrDefault(y) == null)
                        {
                            crates.Add(crate);
                            stacks.Add(new List<string>(crates));
                        }
                        else
                        {
                            foreach (var item in stacks[y])
                            {
                                crates.Add(item);
                            }
                            crates.Add(crate);
                            stacks[y] = new List<string>(crates);
                        }
                    }
                    crates.Clear();
                }
            }
            return stacks;
        }
    }
}
