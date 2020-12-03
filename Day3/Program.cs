using System;
using System.IO;
using System.Linq;

namespace Day3
{
    class Day3
    {
        static void Main(string[] args)
        {
            string[] data = File.ReadAllLines(@"C:\projects\AdventOfCode2020\Day3\day3data.txt");
            
            Console.WriteLine("Part 1: " + Day3.TreeCounter(data, 3, 1));  // Answer: 232
            Console.WriteLine("Part 2: " + Day3.PartTwo(data));  // Answer: 3952291680
        }

        static long TreeCounter(string[] data, int to_right, int down)
        {
            long tree_count = 0;

            int currentRight = 0;
            foreach (int column_index in Enumerable.Range(0, data.Length).Select(x => x * down))
            {
                if (column_index >= data.Length)
                {
                    break;
                }

                string row = data[column_index];
                char square = row[currentRight % row.Length];
                if (square == '#')
                {
                    tree_count++;
                }

                currentRight += to_right;
                
            }

            return tree_count;
        }

        static long PartTwo(string[] data)
        {
            long slope1 = TreeCounter(data, 1, 1);
            long slope2 = TreeCounter(data, 3, 1);
            long slope3 = TreeCounter(data, 5, 1);
            long slope4 = TreeCounter(data, 7, 1);
            long slope5 = TreeCounter(data, 1, 2);

            return slope1 * slope2 * slope3 * slope4 * slope5;
        }
    }
}