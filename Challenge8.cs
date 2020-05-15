//TODO############################################
//Challenge 8 - Headache
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Challenge8
{
    static string InputFileName = @"/Inputs/Challenge 8/codes.txt";
    static string OutputFileNameDown = @"/Outputs/Challenge 8/clean-codes-down.txt";
    static string OutputFileNameTop = @"/Outputs/Challenge 8/clean-codes-top.txt";
    static string OutputFileName = @"/Outputs/Challenge 8/map.txt";

    static List<KeyValuePair<List<string>, List<int>>> Pieces = new List<KeyValuePair<List<string>, List<int>>>();


    static void Main()
    {
        string[] input = File.ReadAllLines(InputFileName).Where(x => x != string.Empty).ToArray();
      
        List<string> linesToUp = GetLinesFromDirection(input, true);
        ShowLinesConsole(linesToUp);

        Console.ReadLine();
    }


    static List<string> GetLinesFromDirection(string[] lines, bool toUp)
    {
        List<string> l = new List<string>();
        int total = lines.Length;

        if (toUp)
        {
            for (int i = 0; i < total; i++)
            {
                string line = lines[i];

                if (line.Contains("P*"))
                {
                    l.Add(line);
                    for (int c = i - 1; c >= 0; c--)
                    {
                        l.Add(lines[c]);
                    }
                }
            }
        }

        return l;
    }

    static void ShowLinesConsole(List<string> lines)
    {
        foreach (var line in lines)
        {
            Console.WriteLine(line);
        }
    }
}