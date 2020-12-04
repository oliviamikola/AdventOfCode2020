using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Day4
{
    class Day4
    {
        static void Main(string[] args)
        {
            PassportHolder holder = new PassportHolder();

            Tuple<long, long> validities = holder.ValidityCheck();
                
            Console.WriteLine("Part 1: " + validities.Item1);  // Answer: 245
            Console.WriteLine("Part 2: " + validities.Item2);  // Answer: 133
        }
    }

    class Passport
    {
        private Dictionary<string, string> dataDict =
        new Dictionary<string, string>(){
            {"byr", null},
            {"iyr", null},
            {"eyr", null},
            {"hgt", null},
            {"hcl", null},
            {"ecl", null},
            {"pid", null},
            {"cid", null}
    };
        
        public Passport(List<string> lines)
        {
            foreach (string line in lines)
            {
                string[] splitLines = line.Trim().Split();

                foreach (string partialLine in splitLines)
                {
                    string[] infoPair = partialLine.Trim().Split(":");
                    string info = infoPair[0];
                    string value = infoPair[1];

                    dataDict[info] = value;
                }
            }
        }

        public bool IsValid(List<string> allowedMissing = null)
        {
            foreach (KeyValuePair<string, string> pair in dataDict)
            {
                if (pair.Value == null)
                {
                    if (allowedMissing != null && allowedMissing.Contains(pair.Key))
                    {
                        continue;
                    }
                    
                    return false;
                }
            }

            return true;
        }

        public bool StrictIsValid()
        {
            return new Regex("^(19[2-9][0-9]|200[0-2])$").IsMatch(dataDict["byr"] ??= "") && 
                   new Regex("^(201[0-9]|2020)$").IsMatch(dataDict["iyr"] ??= "") && 
                   new Regex("^(202[0-9]|2030)$").IsMatch(dataDict["eyr"] ??= "") && 
                   new Regex("(1[5-8][0-9]|19[0-3])cm|(59|6[0-9]|7[0-6])in").IsMatch(dataDict["hgt"] ??= "") && 
                   new Regex("^#[0-9a-f]{6}$").IsMatch(dataDict["hcl"] ??= "") &&
                   new Regex("^amb|blu|brn|gry|grn|hzl|oth$").IsMatch(dataDict["ecl"] ??= "") && 
                   new Regex("^[0-9]{9}$").IsMatch(dataDict["pid"] ??= "");
        }

    }

    class PassportHolder
    {
        List<Passport> passports = new List<Passport>();

        public PassportHolder()
        {
            string[] data = File.ReadAllLines(@"C:\projects\AdventOfCode2020\Day4\day4data.txt");

            List<string> passportInfo = new List<string>();
            foreach (string line in data)
            {
                if (line.Trim() == "")  // Finished with the current passport info
                {
                    passports.Add(new Passport(passportInfo));
                    passportInfo.Clear();
                    continue;
                }
                
                passportInfo.Add(line);
            }

            if (passportInfo.Count != 0)
            {
                passports.Add(new Passport(passportInfo));
            }
        }

        public Tuple<long, long> ValidityCheck()
        {
            long part1Validity = 0;
            long part2Validity = 0;
            
            foreach (Passport passport in passports)
            {
                if (passport.IsValid(new List<string>(){"cid"}))
                {
                    part1Validity++;
                }

                if (passport.StrictIsValid())
                {
                    part2Validity++;
                }
            }
            return new Tuple<long, long>(part1Validity, part2Validity);
        }
    }
}