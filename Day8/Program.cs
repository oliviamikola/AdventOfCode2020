using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day8
{
    class Day8
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            
            Console.WriteLine("Part 1: " + Part1(program).Item2);  // Answer: 1446
            Console.WriteLine("Part 2: " + Part2(program));  // Answer 1403
        }

        static Tuple<bool, int> Part1(Program program)
        {
            int lineNumber = 0;
            int accumulator = 0;
            
            HashSet<int> visitedCommands = new HashSet<int>();

            while (lineNumber < program.commands.Count)
            {
                if (visitedCommands.Contains(lineNumber))
                {
                    return new Tuple<bool, int>(false, accumulator);
                }

                visitedCommands.Add(lineNumber);
                Command command = program.commands[lineNumber];

                switch (command.operation)
                {
                    case Operation.acc:
                        accumulator += command.value;
                        lineNumber++;
                        break;
                    case Operation.jump:
                        lineNumber += command.value;
                        break;
                    case Operation.nop:
                        lineNumber++;
                        break;
                    case Operation.invalid:
                        Console.WriteLine("Something went wrong.");
                        return new Tuple<bool, int>(false, -1);
                }
            }
            
            return new Tuple<bool, int>(true, accumulator);
        }

        static int Part2(Program program)
        {
            foreach (int indexToSwap in Enumerable.Range(0, program.commands.Count))
            {
                Operation previousOperation = program.commands[indexToSwap].operation;
                if (previousOperation == Operation.jump)
                {
                    program.commands[indexToSwap].operation = Operation.nop;
                }
                else if (previousOperation == Operation.nop)
                {
                    program.commands[indexToSwap].operation = Operation.jump;
                }
                else
                {
                    continue;
                }
                
                Tuple<bool, int> info = Part1(program);
                program.commands[indexToSwap].operation = previousOperation;

                if (info.Item1)
                {
                    return info.Item2;
                }

            }

            return -1;
        }
    }

    class Program
    {
        public Dictionary<int, Command> commands = new Dictionary<int, Command>();
        
        public Program()
        {
            string[] data = File.ReadAllLines(@"C:\projects\AdventOfCode2020\Day8\day8data.txt");

            int lineNumber = 0;
            foreach (string line in data)
            {
                commands.Add(lineNumber, new Command(line));
                lineNumber++;
            }
        }
    }

    class Command
    {
        public Operation operation;
        public int value;

        public Command(string line)
        {
            string[] splitLine = line.Split(" ");

            switch (splitLine[0].Trim())
            {
                case "acc":
                    operation = Operation.acc;
                    break;
                case "jmp":
                    operation = Operation.jump;
                    break;
                case "nop":
                    operation = Operation.nop;
                    break;
                default:
                    operation = Operation.invalid;
                    break;
            }

            string entireValue = splitLine[1];
            char sign = entireValue[0];
            string number = entireValue.Substring(1);
            
            value = Int32.Parse(number);
            if (sign == '-')
            {
                value *= -1;
            }
        }
    }
    
    enum Operation
    {
        invalid,
        acc,
        jump,
        nop
    }
}