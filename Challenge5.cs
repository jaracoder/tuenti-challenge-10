//Challenge 5 - Tuentistic Numbers

using System;
using System.IO;

class Challenge5
{
    static string InputFileName  = @"/Inputs/Challenge 5/submitInput.txt";
    static string OutputFileName = @"/Outputs/Challenge 5/submitOutput.txt";

    static void Main()
    {
        string[] input = File.ReadAllLines(InputFileName);

        int casesTotal = int.Parse(input[0]);
        string[] output = new string[casesTotal];

        for (int i = 0; i < casesTotal; i++)
        {
            decimal number = decimal.Parse(input[i + 1]);

            Console.Write(number + " - ");

            if (IsPossibleTuentisticNumber(number))
            {
                decimal max = GetMaxNumTuentisticNumber(number);
               
                if (max > 0)
                {
                    Console.WriteLine(max + ".");
                    output[i] = string.Format("Case #{0}: {1}", i + 1, max);
                }
                else
                {
                    Console.WriteLine("IMPOSSIBLE.");
                    output[i] = string.Format("Case #{0}: IMPOSSIBLE", i + 1);
                }
            }
            else
            {
                Console.WriteLine("IMPOSSIBLE.");
                output[i] = string.Format("Case #{0}: IMPOSSIBLE", i + 1);
            }
        }

        File.WriteAllLines(OutputFileName, output);
    }

    static bool IsPossibleTuentisticNumber(decimal number)
    {
        if (number >= 20 && number <= 29)
        {
            return true;
        }

        if (number < 20)
        {
            return false;
        }

        return true;
    }

    static decimal GetMaxNumTuentisticNumber(decimal number)
    {
        if (number >= 20 && number <= 29) return 1;
    
        decimal result = number / 20 - 1;
        if (result < 2 && result > 0) result = 2;

        if (result > 0)
        {
            for (decimal i = result; i >= 1; i--)
            {
                for (decimal z = 20; z <= 29; z++)
                {
                    decimal total = z * i;

                    if (total > number) break;

                    for (long c = 20; c <= 29; c++)
                    {
                        if (total + 29 < number) break;

                        if (total + c == number)
                        {
                            return decimal.Truncate(i + 1);
                        }
                    }
                }
            }
        }

        return 0;
    }
}