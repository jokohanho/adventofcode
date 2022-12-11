using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;


namespace Day8
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Continues processing items in a collection until the end condition is true.
        /// </summary>
        /// <typeparam name="T">The type of the collection.</typeparam>
        /// <param name="collection">The collection to iterate.</param>
        /// <param name="endCondition">The condition that returns true if iteration should stop.</param>
        /// <returns>Iterator of sub-list.</returns>
        public static IEnumerable<T> TakeUntil<T>(this IEnumerable<T> collection, Predicate<T> endCondition)
        {
            return collection.TakeWhile(item => !endCondition(item));
        }
    }
    class Program
    {
        
        static void Main()
        {
            List<string> list = new List<string>();
            using (var reader = new StreamReader(@"C:\Dev\Adventofcode\day8.csv"))
            {

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    list.Add(line);
                }
            }

            
            Console.WriteLine("SolvePartOne: " +SolvePartOne(list));
            Console.WriteLine("SolvePartTwo: " + SolvePartTwo(list));
        }

        static int SolvePartOne(List<string> list)
        { 
            (List<List<int>> RowList, List<List<int>> ColumnList) = genRowAndColumnLists(list);
            int x = 1;
            int y = 1;
            int visableTrees = (RowList.Count() * 2) + (ColumnList.Take(ColumnList.Count() - 1).Skip(1).Count()*2);
            foreach (var treeList in RowList.Take(RowList.Count() - 1).Skip(1))
            {
                int xa = 1;
                int ya = 1;
                foreach (var tree in treeList.Take(RowList.Count() - 1).Skip(1))
                {
                    List<int> leftList = RowList[x].GetRange(0, xa);
                    List<int> topList = ColumnList[ya].GetRange(0, y);
                    List<int> rightList = RowList[x].GetRange(xa+1, RowList[x].Count-1-xa);
                    List<int> downList = ColumnList[ya].GetRange(y+1, RowList[ya].Count - 1 - y);
                    var LeftResult = leftList.Where(x => x >= tree).ToList();
                    var RightResult = rightList.Where(x => x >= tree).ToList();
                    var TopResult = topList.Where(x => x >= tree).ToList();
                    var DownResult = downList.Where(x => x >= tree).ToList();
                    if (LeftResult.Count == 0 || RightResult.Count == 0 || TopResult.Count == 0 || DownResult.Count == 0  )
                    {
                        visableTrees++;
                    }
                    ya++;
                    xa++;
                }
                y++;
                x++;
            }
            return visableTrees;
        }

        static int SolvePartTwo(List<string> list)
        {
            
            (List<List<int>> RowList, List<List<int>> ColumnList) = genRowAndColumnLists(list);
            List<int> scenicScoreList = new List<int>();
            int x = 1;
            int y = 1;
            //int visableTrees = (RowList.Count() * 2) + (ColumnList.Take(ColumnList.Count() - 1).Skip(1).Count() * 2);
            foreach (var treeList in RowList.Take(RowList.Count() - 1).Skip(1))
            {
                int xa = 1;
                int ya = 1;
                foreach (var tree in treeList.Take(RowList.Count() - 1).Skip(1))
                {
                    List<int> leftList = RowList[x].GetRange(0, xa);
                    Stack<int> stack = new Stack<int>();
                    foreach (var treeX in leftList)
                    {
                        stack.Push(treeX);
                    }
                    int LeftResult = getScenicView(stack, tree);
                    stack.Clear();
                    List<int> topList = ColumnList[ya].GetRange(0, y);
                    foreach (var treeX in topList)
                    {
                        stack.Push(treeX);
                    }
                    int TopResult = getScenicView(stack, tree);
                    stack.Clear();
                    List<int> rightList = RowList[x].GetRange(xa + 1, RowList[x].Count - 1 - xa);
                    rightList.Reverse();
                    foreach (var treeX in rightList)
                    {
                        stack.Push(treeX);
                    }
                    int RightResult = getScenicView(stack, tree);
                    stack.Clear();
                    List<int> downList = ColumnList[ya].GetRange(y + 1, RowList[ya].Count - 1 - y);
                    downList.Reverse();
                    foreach (var treeX in downList)
                    {
                        stack.Push(treeX);
                    }
                    int DownResult = getScenicView(stack, tree);

                    int scenicScore = LeftResult * TopResult * RightResult * DownResult;
                    scenicScoreList.Add(scenicScore);
                    ya++;
                    xa++;
                }
                y++;
                x++;
            }

          
       
            return scenicScoreList.Max();

        }
       
        static int getScenicView(Stack<int> stack, int tree)
        {
            List<int> listResult = new List<int>();
            foreach (var x in stack)
            {
                if (x >= tree)
                {
                    listResult.Add(x);
                    break;
                }
                else if (x < tree)
                {
                    listResult.Add(x);
                }
                else
                {
                    break;
                }
            }
            return listResult.Count;
        }

        static (List<List<int>>,List<List<int>>) genRowAndColumnLists(List<string> list)
        {
            List<List<int>> RowList = new List<List<int>>();
            List<List<int>> ColumnList = new List<List<int>>();

            char[] charArray = list[0].ToArray();
            List<int> columns = new List<int>();
            foreach (var x in charArray)
            {
                columns.Add(int.Parse(x.ToString()));
            }
            List<int> column2 = new List<int>();
            
            foreach (var col in columns)
            {
                column2.Add(col);
                ColumnList.Add(new List<int>(column2));
                column2.Clear();
            }
            foreach (var line in list)
            {
                charArray = line.ToArray();
                List<int> row = new List<int>();
                foreach (var x in charArray)
                {
                    row.Add(int.Parse(x.ToString()));
                }
                RowList.Add(new List<int>(row));
                if(list[0] != line)
                {
                    List<int> column = new List<int>();
                    for (int i = 0; i < line.Length - 1; i++)
                    {
                        column = ColumnList[i];
                        column.Add(int.Parse(line[i].ToString()));
                        ColumnList[i] = new List<int>(column);
                    }
                }
            }
            return (RowList, ColumnList);
        }

    }
}
