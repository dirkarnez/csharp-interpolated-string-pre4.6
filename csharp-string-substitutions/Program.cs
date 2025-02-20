﻿using System;
using System.Text.RegularExpressions;

namespace csharp_interpolated_string_pre4._6
{
    //class Job
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public string Description { get; set; }

    //    public Customer[] Customers { get; set; }
    //}

    //class Customer
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}

    class Program
    {
        static void Main(string[] args)
        {
            var a = "{{   b      }}";
            var b = "world";
            var template = "this is a string with variable {{    a   }} and {{b}}, again {{a}} {{b}}, an unmatched {{c  }}, an maybe-illegal one {{ b c }}";

            MatchEvaluator matcher = match =>
            {
                // DisplayMatchResults(match);

                var value = match.Groups[1].Value.Trim();
                switch (value) {
                    case "a":
                        Console.WriteLine($"Matched--\"{value}\"");
                        return a;
                    case "b":
                        Console.WriteLine($"Matched--\"{value}\"");
                        return b;
                    default:
                        Console.WriteLine($"Unmatched--\"{value}\"");
                        return match.Value; // do nothing
                }
            };

            // YES, leaving left and rights spaces for trimming,
            // because otherwise the capture will not work well
            // the capture should not care spaces in the middle (e.g. {{ a b }})
            Console.WriteLine(Regex.Replace(template, "{{([^{}]+)}}", matcher));
            Console.ReadLine();
        }

        public static void DisplayMatchResults(Match match)
        {
            Console.WriteLine("Match has {0} captures", match.Captures.Count);

            int groupNo = 0;
            foreach (Group mm in match.Groups)
            {
                Console.WriteLine("  Group {0,2} has {1,2} captures '{2}'", groupNo, mm.Captures.Count, mm.Value);

                int captureNo = 0;
                foreach (Capture cc in mm.Captures)
                {
                    Console.WriteLine("       Capture {0,2} '{1}'", captureNo, cc);
                    captureNo++;
                }
                groupNo++;
            }

            groupNo = 0;
            foreach (Group mm in match.Groups)
            {
                Console.WriteLine("    match.Groups[{0}].Value == \"{1}\"", groupNo, match.Groups[groupNo].Value); //**
                groupNo++;
            }

            groupNo = 0;
            foreach (Group mm in match.Groups)
            {
                int captureNo = 0;
                foreach (Capture cc in mm.Captures)
                {
                    Console.WriteLine("    match.Groups[{0}].Captures[{1}].Value == \"{2}\"", groupNo, captureNo, match.Groups[groupNo].Captures[captureNo].Value); //**
                    captureNo++;
                }
                groupNo++;
            }
        }
    }
}


