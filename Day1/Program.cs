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
            
            // Console.WriteLine("Part 1: " + Day1.PartOne(data));  // Answer: 731731
            // Console.WriteLine("Part 2: " + Day1.PartTwo(data)); // Answer: 116115990

            List<int> int_list = converter(data);
            int_list.Sort();

            List<int> a = new List<int>(){1, 2, 3, 4, 7, 8, 9, 10};
            foreach (int i in a)
            {
                if (binary_search(a, i, 0, a.Count - 1) != true)
                {
                    Console.WriteLine(i + " is False.");
                }
            }
            
            
            
            Console.WriteLine(p1attempt(int_list));
        }

        static List<int> converter(string[] data)
        {
            List<int> int_list = new List<int>();
            foreach (string s in data)
            {
                int i = Convert.ToInt32(s);
                int_list.Add(i);
            }
            return int_list;
        }

        static bool binary_search(List<int> data, int value, int start, int end)
        {
            if (start == end)
            {
                return value == data[start];
            }

            if (start > end)
            {
                return false;
            }

            int mid = (start + end) / 2;

            if (data[mid] == value)
            {
                return true;
            }
            else if (data[mid] < value)
            {
                return binary_search(data, value, mid, end);
            }
            else  // data[mid] > value
            {
                return binary_search(data, value, start, mid);
            }
        }

        static long p1attempt(List<int> data)
        {
            // Assume data is sorted
            
            foreach (int d in data)
            {
                int missing_sum = 2020 - d;
                bool in_list = binary_search(data, missing_sum, 0, data.Count - 1);

                if (in_list)
                {
                    return d * missing_sum;
                }
            }

            return 0;
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