using System;
using System.Collections.Generic;
using System.IO;

namespace Day2
{
    class Day2
    {
        static void Main(string[] args)
        {
            PasswordHolder manager = new PasswordHolder();
            
            Console.WriteLine("Part 1: " + manager.Part1CountValidity());  // Answer: 582
            Console.WriteLine("Part 2: " + manager.Part2CountValidity());  // Answer: 729
        }
    }

    /// <summary>
    /// Stores information about each password
    /// </summary>
    class Password
    {
        private string CorruptedPassword { get; }
        private int First { get; }
        private int Second { get; }
        private char Character { get; }

        public Password(string line)
        {
            string[] string_list = line.Split(':');
            CorruptedPassword = string_list[1].Trim();

            string[] times_and_char_list = string_list[0].Split(' ');
            Character = times_and_char_list[1][0];

            string[] times_list = times_and_char_list[0].Split('-');
            First = Convert.ToInt32(times_list[0].Trim());
            Second = Convert.ToInt32(times_list[1].Trim());
        }

        public bool Part1IsValid()
        {
            int character_count = 0;
            foreach (char p in CorruptedPassword)
            {
                if (p == Character)
                {
                    character_count++;
                }
            }

            return First <= character_count && character_count <= Second;
        }

        public bool Part2IsValid()
        {
            return CorruptedPassword[First - 1] == Character ^ CorruptedPassword[Second - 1] == Character;
        }
    }

    /// <summary>
    /// Class to hold all the passwords provided
    /// </summary>
    class PasswordHolder
    {
        private List<Password> Holder = new List<Password>();

        public PasswordHolder()
        {
            string[] data = File.ReadAllLines(@"C:\projects\AdventOfCode2020\Day2\day2data.txt");
            foreach (string line in data)
            {
                Password password = new Password(line);
                Holder.Add(password);
            }
        }

        public long Part1CountValidity()
        {
            long numValidPart1 = 0;
            foreach (Password p in Holder)
            {
                if (p.Part1IsValid())
                {
                    numValidPart1++;
                }
            }

            return numValidPart1;
        }

        public long Part2CountValidity()
        {
            long numValidPart2 = 0;
            foreach (Password p in Holder)
            {
                if (p.Part2IsValid())
                {
                    numValidPart2++;
                }
            }

            return numValidPart2;
        }
    }
}