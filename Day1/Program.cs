using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace Day1
{
    class Day1
    {
        static void Main(string[] args)
        {
            string[] data = File.ReadAllLines(@"C:\projects\AdventOfCode2020\Day1\day1data.txt");
            
            Console.WriteLine("Part 1: " + Day1.PartOne(data));  // Answer: 731731
            Console.WriteLine("Part 2: " + Day1.PartTwo(data)); // Answer: 116115990
        }

        /// <summary>
        /// Finds pair of numbers whose sum adds to 2020
        /// </summary>
        /// <param name="data"> Lines from provided data </param>
        /// <returns> The product of two numbers whose sum equals to 2020 </returns>
        static long PartOne(string[] data)
        {
            int first_index = 0;
            while (first_index < data.Count())
            {
                int second_index = 0;
                while (second_index < data.Count())
                {
                    if (first_index == second_index)
                    {
                        second_index++;
                        continue;
                    }

                    if (Convert.ToInt32(data[first_index]) + Convert.ToInt32(data[second_index]) == 2020)
                    {
                        return Convert.ToInt32(data[first_index]) * Convert.ToInt32(data[second_index]);
                    }

                    second_index++;
                }

                first_index++;
            }

            return 0;
        }

        /// <summary>
        /// Finds three numbers whose sum adds to 2020
        /// </summary>
        /// <param name="data"> Lines from provided data </param>
        /// <returns> The product of three numbers whose sum equals to 2020 </returns>
        static long PartTwo(string[] data)
        {
            int first_index = 0;
            while (first_index < data.Count())
            {
                int second_index = 0;
                while (second_index < data.Count())
                {
                    if (first_index == second_index)
                    {
                        second_index++;
                        continue;
                    }

                    int third_index = 0;
                    while (third_index < data.Count())
                    {
                        if (first_index == third_index | second_index == third_index)
                        {
                            third_index++;
                            continue;
                        }

                        if (Convert.ToInt32(data[first_index]) + Convert.ToInt32(data[second_index]) +
                            Convert.ToInt32(data[third_index]) == 2020)
                        {
                            return Convert.ToInt32(data[first_index]) * Convert.ToInt32(data[second_index]) *
                                   Convert.ToInt32(data[third_index]);
                        }

                        third_index++;
                    }

                    second_index++;
                }

                first_index++;
            }

            return 0;
        }
    }
}