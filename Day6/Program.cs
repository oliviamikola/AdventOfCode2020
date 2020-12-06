using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day6
{
    class Program
    {
        static void Main(string[] args)
        {
            GroupHolder holder = new GroupHolder();
            Console.WriteLine("Part 1: " + holder.GetAnsweredYesSum());  // Answer 6430
            Console.WriteLine("Part 2: " + holder.GetAllAnsweredYesSum());  // Answer: 3125
        }
    }

    class Group
    {
        private HashSet<char> questionsAnsweredYes = new HashSet<char>();
        public int numQuestionsAnsweredYes { get; }
        
        private HashSet<char> questionsAllAnsweredYes = new HashSet<char>()
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i',
            'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r',
            's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
        };
        public int numAllQuestionsAnsweredYes { get; }

        public Group(List<string> data)
        {
            foreach (string line in data)
            {
                HashSet<char> answeredYes = new HashSet<char>();
                foreach (char question in line)
                {
                    answeredYes.Add(question);
                }
                questionsAnsweredYes.UnionWith(answeredYes);
                questionsAllAnsweredYes.IntersectWith(answeredYes);
            }
            
            numQuestionsAnsweredYes = questionsAnsweredYes.Count;
            numAllQuestionsAnsweredYes = questionsAllAnsweredYes.Count;
        }
    }

    class GroupHolder
    {
        List<Group> groups = new List<Group>();

        public GroupHolder()
        { 
            string[] data = File.ReadAllLines(@"C:\projects\AdventOfCode2020\Day6\day6data.txt");

            List<string> groupData = new List<string>(); 
            foreach (string line in data)
            {
                if (line.Trim() == "")
                {
                    groups.Add(new Group(groupData));
                    groupData.Clear();
                    continue;
                }
                groupData.Add(line);
            }
            
            groups.Add(new Group(groupData));
        }

        public long GetAnsweredYesSum()
        {
            long sum = 0;
            foreach (Group group in groups)
            {
                sum += group.numQuestionsAnsweredYes;
            }

            return sum;
        }

        public long GetAllAnsweredYesSum()
        {
            long sum = 0;
            foreach (Group group in groups)
            {
                sum += group.numAllQuestionsAnsweredYes;
            }

            return sum;
        }
    }
}
