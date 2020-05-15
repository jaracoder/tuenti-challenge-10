// Challenge 2 - The Lucky One

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Challenge2
{
    static string InputFileName  = @"/Inputs/Challenge 2/submitInput.txt";
    static string OutputFileName = @"/Outputs/Challenge 2/submitOutput2.txt";

    static void Main()
    {
        string[] input = File.ReadAllLines(InputFileName);

        int casesTotal = int.Parse(input[0]);
        string[] output = new string[casesTotal];

        var indexInput = 1;
        for (int i = 0; i < casesTotal; i++)
        {
            int resultsTotal = int.Parse(input[indexInput]);
            indexInput++;
            
            List<Result> results = GetResults(input, indexInput, resultsTotal);
            indexInput += resultsTotal;

            var bestPlayer = GetBestPlayer(results);
            output[i] = string.Format("Case #{0}: {1}", i + 1, bestPlayer.Num);
        }

        File.WriteAllLines(OutputFileName, output);
    }

    static List<Result> GetResults(string[] input, int index, int total)
    {
        List<Result> results = new List<Result>();

        string line = string.Empty;
        for (int i = 0; i < total; i++)
        {
            line = input[index];
            results.Add(GetResult(line));

            index++;
        }

        return results;
    }

    static Player GetBestPlayer(List<Result> results)
    {
        Player betterPlayer = null,
               aux = null;

        var grouByWins = results
            .GroupBy(x => x.Winner)
            .Select(x => new Player(){ Num = x.Key.Num, Wins = x.Count(), Loses = 0 })
            .OrderByDescending(x => x.Wins)
            .ToList();

        var grouByLoses = results
            .GroupBy(x => x.Losser)
            .Select(x => new Player(){ Num = x.Key.Num, Wins = x.Key.Wins, Loses = x.Count() })
            .OrderByDescending(x => x.Wins)
            .ToList();
 
        foreach (var group in grouByWins)
        {
            group.Wins -= grouByLoses
                .Where(x => x.Num == group.Num)
                .Count();

            if (aux == null)
            {
                aux = new Player() { Num = group.Num, Wins = group.Wins };
            }
            else
            {
                if (aux.Wins != group.Wins)
                {
                    betterPlayer = aux.Wins > group.Wins ? aux :
                        new Player() { Num = group.Num, Wins = group.Wins };

                    break;
                }
            }
        }

        return betterPlayer;
    }

    static Result GetResult(string line)
    {
        string[] aux = line.Split(' ');

        if (aux[2] == "0")
        {
            return new Result
            {
                Winner = new Player() { Num = int.Parse(aux[1]) },
                Losser = new Player() { Num = int.Parse(aux[0]) },
            };
        }
        else
        {
            return new Result
            {
                Winner = new Player() { Num = int.Parse(aux[0]) },
                Losser = new Player() { Num = int.Parse(aux[1]) },
            };
        }
    }

    public class Player : IEquatable<Player>
    {
        public int? Num { get; set; }
        public int? Wins { get; set; }
        public int Loses { get; set; }
        
        public bool Equals(Player other)
        {
            return Num == other.Num;
        }

        public override int GetHashCode()
        {
            return Num == null ? 0 : Num.GetHashCode();
        }
    }

    public class Result
    {
        public Player Winner { get; set; }
        public Player Losser { get; set; }
    }
}