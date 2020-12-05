using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day5
{
    class Day5
    {
        static void Main(string[] args)
        {
            BoardingPassHolder holder = new BoardingPassHolder();
            
            Console.WriteLine("Part 1: " + holder.FindMaxSeatId());  // Answer: 822
            Console.WriteLine("Part 2: " + holder.FindMySeat());  // Answer: 705
        }
    }

    class BoardingPass
    {
        private int rowNumber;
        private int seatNumber;

        public int seatId { get; }

        public BoardingPass(string info)
        {
            rowNumber = FindNumber(0, 127, info.Substring(0, 7), 'F', 'B');
            seatNumber = FindNumber(0, 7, info.Substring(7, 3), 'L', 'R');

            seatId = rowNumber * 8 + seatNumber;
        }


        private int FindNumber(int start, int end, string data, char lesser, char greater)
        {
            int num = 0;
            
            foreach (char direction in data)
            {
                if (direction == lesser)
                {
                    end = (start + end) / 2;
                    num = end;
                }

                if (direction == greater)
                {
                    start = (start + end) / 2 + 1;
                    num = start;
                }
            }

            return num;
        }
    }

    class BoardingPassHolder
    {
        List<BoardingPass> passes = new List<BoardingPass>();
        List<int> ids = new List<int>();

        public BoardingPassHolder()
        {
            string[] data = File.ReadAllLines(@"C:\projects\AdventOfCode2020\Day5\day5data.txt");

            foreach (string line in data)
            {
                BoardingPass pass = new BoardingPass(line);
                passes.Add(pass);
                ids.Add(pass.seatId);
            }
            
            ids.Sort();
        }

        public int FindMaxSeatId()
        {
            int maxId = -1;
            
            foreach (BoardingPass pass in passes)
            {
                if (pass.seatId > maxId)
                {
                    maxId = pass.seatId;
                }
            }

            return maxId;
        }

        public int FindMySeat()
        {
            foreach (int index in Enumerable.Range(0, ids.Count - 2))
            {
                int firstSeatId = ids[index];
                int secondSeatId = ids[index + 1];

                if (secondSeatId - firstSeatId == 2)
                {
                    return secondSeatId - 1;
                }
            }

            return -1;
        }
    }
}