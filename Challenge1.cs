//Challenge1: Rock, Paper, Scissors

using System.IO;

class Challenge1
{
    static string InputFileName  = @"/Inputs/Challenge 1/submitInput.txt";
    static string OutputFileName = @"/Outputs/Challenge 1/submitOutput.txt";

    static void Main()
    {
        string[] input = File.ReadAllLines(InputFileName);

        int total = int.Parse(input[0]);
        string[] output = new string[total];

        string line = string.Empty;
        for (int i = 0; i < total; i++)
        {
            line = input[i + 1];
            output[i] = string.Format("Case #{0}: {1}", i + 1, GetWinner(line));
        }

        File.WriteAllLines(OutputFileName, output);
    }

    static string GetWinner(string round)
    {
        string move1 = round.Split(' ')[0];
        string move2 = round.Split(' ')[1];

        if (move1 == move2) return "-";
      
        if (move1 == Game.Rock)
        {
            if (move2 == Game.Scissors)
                return Game.Rock;
            else
                return Game.Paper;
        }

        if (move1 == Game.Scissors)
        {
            if (move2 == Game.Paper)
                return Game.Scissors;
            else
                return Game.Rock;
        }

        if (move1 == Game.Paper)
        {
            if (move2 == Game.Rock)
                return Game.Paper;
            else
                return Game.Scissors;
        }

        return string.Empty;
    }

    class Game
    {
        public static string Rock = "R";
        public static string Paper = "P";
        public static string Scissors = "S";
    }
}