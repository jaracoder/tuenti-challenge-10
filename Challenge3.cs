// Challenge 3 - Fortunata and Jacinta

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

class Challenge3
{
    static string InputBookName = @"/Inputs/Challenge 3/pg17013.txt";
    static string InputFileName  = @"/Inputs/Challenge 3/submitInput.txt";
    static string OutputFileName = @"/Outputs/Challenge 3/submitOutput.txt";

    static Dictionary<string, int> Dictionary = new Dictionary<string, int>();
    static IOrderedEnumerable<KeyValuePair<string, int>> DictionarySort;
    static List<string> UnicodeOrder = new List<string>() { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "á", "é", "í", "ñ", "ó", "ú", "ü" };

  
    static void Main()
    {
        GenerateDictionary();

        string[] input = File.ReadAllLines(InputFileName, Encoding.UTF8);

        int casesTotal = int.Parse(input[0]);
        string[] output = new string[casesTotal];

        for (int i = 0; i < casesTotal; i++)
        {
            string wordOrDigits = input[i + 1];

            if (IsDigitsOnly(wordOrDigits))
            {
                int number = int.Parse(wordOrDigits);
                var item = DictionarySort.ElementAt(number - 1);

                output[i] = string.Format("Case #{0}: {1} {2}", i + 1, item.Key, item.Value);
            }
            else
            {
                var value = DictionarySort.First(x => x.Key == wordOrDigits).Value;
                var key = wordOrDigits;
                var index = DictionarySort.ToList().IndexOf(
                    new KeyValuePair<string, int>(key, value)) + 1;

                output[i] = string.Format("Case #{0}: {1} #{2}", i + 1, value, index);
            }
        }

        var utf8WithoutBOM = new UTF8Encoding(false); // Thanks to the Tuenti engineers :)
        File.WriteAllLines(OutputFileName, output, utf8WithoutBOM);
    }

    static void GenerateDictionary()
    {
        string line;

        using (StreamReader book = new StreamReader(InputBookName, Encoding.Default))
        {
            while ((line = book.ReadLine()) != null)
            {
                var words = GetWords(line);

                // Save Dictionary
                var wordsReplace = ReplaceCharsWords(words);
                for (int i = 0; i < wordsReplace.Length; i++)
                {
                    var word = wordsReplace[i];

                    if (Dictionary.ContainsKey(word))
                    {
                        int total = Dictionary[word] + 1;
                        Dictionary[word] = total;
                    }
                    else
                    {
                        Dictionary.Add(word, 1);
                    }
                } 
            }
        }

        // Sort dictionary frecuency and order unicode
        DictionarySort = Dictionary.OrderByDescending(item => item.Value)
            .ThenBy(item => item, new UnicodeOrderCompare());
    }

    static string[] GetWords(string line)
    {
        line = line.ToLower();

        return line.ToLower()
            .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .Where(x => x.Length > 2)
            .ToArray();
    }

    static string[] ReplaceCharsWords(string[] words)
    {
        var wordsInLine = string.Join(" ", words);
        var reg = new Regex(@"[^" + string.Join("", UnicodeOrder) + "]");

        return reg.Replace(wordsInLine, " ")
             .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
             .Where(x => x.Length > 2)
             .ToArray();
    }

    static bool IsDigitsOnly(string str)
    {
        foreach (char c in str)
        {
            if (c < '0' || c > '9')
                return false;
        }

        return true;
    }

    public class UnicodeOrderCompare : IComparer<KeyValuePair<string, int>>
    {    
        public int Compare(KeyValuePair<string, int> x, KeyValuePair<string, int> y)
        {
            var i = 0;
            var j = 0;

            while (i < x.Key.Length && j < y.Key.Length)
            {
                var aVal = UnicodeOrder.IndexOf(x.Key[i].ToString());
                var bVal = UnicodeOrder.IndexOf(y.Key[j].ToString());
                if (aVal < bVal)
                {
                    return -1;
                }
                else if (aVal > bVal)
                {
                    return 1;
                }

                i++;
                j++;
            }

            if (x.Key.Length > i)
                return 1;
            else if (y.Key.Length > j)
                return -1;
            else
                return 0;

         //   return string.Compare(x.Key, y.Key, StringComparison.CurrentCulture);
        }
    }
}