//Challenge 7 - Encrypted lines
using System.Collections.Generic;
using System.IO;

class Challenge7
{
    static string InputFileName = @"/Inputs/Challenge 7/submitInput.txt";
    static string OutputFileName = @"/Outputs/Challenge 7/submitOutput.txt";

    static void Main()
    {
        string[] input = File.ReadAllLines(InputFileName);

        int casesTotal = int.Parse(input[0]);
        string[] output = new string[casesTotal];

        for (int i = 0; i < casesTotal; i++)
        {
            string line = input[i + 1];
            string lineDecrypt = Decrypt(line);

            output[i] = string.Format("Case #{0}: {1}", i + 1, lineDecrypt); 
        }

        File.WriteAllLines(OutputFileName, output);
    }

    static string Decrypt(string line)
    {
        string lineDecrypt = string.Empty;

        for (int i = 0; i < line.Length; i++)
        {
            char letter = line[i];

            if (letter != ' ' && Dictionary.ContainsKey(letter))
            {
                letter = Dictionary[letter];
            }

            lineDecrypt += letter;
        }
        
        return lineDecrypt;
    }

    static Dictionary<char, char> Dictionary = new Dictionary<char, char>
    {
        {'a','a'},
        {'.','e'},
        {'c','i'},
        {'r','o'},
        {'g','u'},
        {'x','b'},
        {'j','c'},
        {'e','d'},
        {'u','f'},
        {'i','g'},
        {'d','h'},
        {'h','j'},
        {'t','k'},
        {'n','l'},
        {'m','m'},
        {'b','n'},
        {'l','p'},
        {'\'','q'},
        {'p','r'},
        {'o','s'},
        {'y','t'},
        {'k','v'},
        {',','w'},
        {'q','x'},
        {'f','y'},
        {';','z'},
        {'w',','},
        {'v','.' },
        {'-','\'' },
        {'z','/' },
        {'s',';' }
    };
}