using System;
using System.Collections.Generic;
using System.IO;

namespace Day7
{
    class Day7
    {
        static void Main(string[] args)
        {
            BagHolder holder = new BagHolder();
            
            Console.WriteLine("Part 1: " + holder.GetBagsContaining("shiny gold"));  // Answer: 208
            Console.WriteLine("Part 2: " + holder.GetBagsInside("shiny gold"));  // Answer: 1664
        }
    }

    class InnerBag
    {
        private string bag { get; }
        public HashSet<string> possibleOuterBags = new HashSet<string>();

        public InnerBag(string name, string outerBag)
        {
            this.bag = name;
            this.possibleOuterBags.Add(outerBag);
        }

        public void AddOuterBag(string outerBag)
        {
            possibleOuterBags.Add(outerBag);
        }
    }
    
    class OuterBag
    {
        public string bag { get; }
        public Dictionary<string, long> containedInnerBags = new Dictionary<string, long>();
        
        public OuterBag(string line, Dictionary<string, InnerBag> innerBags)
        {
            string[] splitLine = line.Split(" contain ");
            
            this.bag = splitLine[0].Replace("bags", "").Trim();
            
            if (splitLine[1] != "no other bags.")
            {
                string[] containedBags = splitLine[1].Split(",");
                SetInnerBags(containedBags, innerBags);
            }
        }

        private void SetInnerBags(string[] containedBags, Dictionary<string, InnerBag> innerBags)
        {
            foreach (string bag in containedBags)
            {
                long numBags = Convert.ToInt64(bag.Trim()[0].ToString());
                string innerBagName = bag.Trim().Substring(2).Replace("bags", "").Replace("bag", "").Trim('.').Trim();

                if (!innerBags.ContainsKey(innerBagName))
                {
                    InnerBag innerBag = new InnerBag(innerBagName, this.bag);
                    innerBags.Add(innerBagName, innerBag);
                }

                innerBags[innerBagName].AddOuterBag(this.bag);
                containedInnerBags.Add(innerBagName, numBags);
            }
        }
    }

    class BagHolder
    {
        Dictionary<string, InnerBag> innerBags = new Dictionary<string, InnerBag>();
        Dictionary<string, OuterBag> outerBags = new Dictionary<string, OuterBag>();

        public BagHolder()
        {
            string[] data = File.ReadAllLines(@"C:\projects\AdventOfCode2020\Day7\day7data.txt");

            foreach (string line in data)
            {
                OuterBag outerBag = new OuterBag(line, innerBags);
                outerBags.Add(outerBag.bag, outerBag);
            }
        }

        public int GetBagsContaining(string bagName)  // BFS
        {
            Queue<string> containersToVisit = new Queue<string>();
            HashSet<string> visitedContainers = new HashSet<string>();
            
            containersToVisit.Enqueue(bagName);
            while (containersToVisit.Count != 0)
            {
                string currentContainer = containersToVisit.Dequeue();

                if (!innerBags.ContainsKey(currentContainer))
                {
                    continue;
                }
                HashSet<string> outerContainers = innerBags[currentContainer].possibleOuterBags;

                foreach (string outerContainer in outerContainers)
                {
                    if (visitedContainers.Contains(outerContainer))
                    {
                        continue;
                    }
                    containersToVisit.Enqueue(outerContainer);
                    visitedContainers.Add(outerContainer);
                }
            }
            return visitedContainers.Count;
        }

        public long GetBagsInside(string bagName)  // DFS
        {
            long GetBagsInner(string currentBagName)
            {
                if (!outerBags.ContainsKey(currentBagName))
                {
                    return 0;
                }

                long numInnerBags = 1;
                Dictionary<string, long> innerBags = outerBags[currentBagName].containedInnerBags;
                
                foreach (string innerBag in innerBags.Keys)
                {
                    numInnerBags += innerBags[innerBag] * GetBagsInner(innerBag);
                }

                return numInnerBags;
            }

            return GetBagsInner(bagName) - 1;
        }
    }
}