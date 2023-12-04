using System.Text.RegularExpressions;

namespace _04;

public class App
{
    private string FormatString(string input)
    {
        var regex = new Regex("[\\s\\s+]");
        return regex.Replace(input, " ");
    }
    
    private double CountWinningPoints(string input)
    {
        var lines = input.Split("\n");
        double res = 0;
        foreach (var line in lines)
        {
            var tmpLine = line.Substring(line.IndexOf(':') + 1).Trim();
            tmpLine = tmpLine.Replace("  ", " ");
            var winning = tmpLine.Split("|")[0].Trim();
            var myCards = tmpLine.Split("|")[1].Trim();

            var winningArray = winning.Split(" ");
            var myCardsArray = myCards.Split(" ");

            int howMany = 0;
            foreach (var s in winningArray)
            {
                if (myCardsArray.Contains(s))
                    howMany++;
            }

            if (howMany > 0)
                res += Math.Pow(2, howMany - 1);

        }

        return res;
    }
    
    private int CountScratchcards(string input)
    {
        var lines = input.Split("\n");
        int[] res = new int[lines.Length];
        for (var i = 0; i < res.Length; i++)
        {
            res[i] = 1;
        }
        
        for (int i = 0;i < lines.Length;i++)
        {
            var tmpLine = lines[i].Substring(lines[i].IndexOf(':') + 1).Trim();
            tmpLine = tmpLine.Replace("  ", " ");
            var winning = tmpLine.Split("|")[0].Trim();
            var myCards = tmpLine.Split("|")[1].Trim();

            var winningArray = winning.Split(" ");
            var myCardsArray = myCards.Split(" ");

            int howMany = 0;
            foreach (var s in winningArray)
            {
                if (myCardsArray.Contains(s))
                    howMany++;
            }

            if (howMany > 0)
            {
                for (int j = 1; j < lines.Length && j <= howMany; j++)
                {
                    res[i + j] += res[i];
                }
            }
            
            

        }

        int val = 0;
        foreach (var re in res)
        {
            val += re;
        }

        return val;
    }
    
    private string ReadFile()
    {
        string fileName = "/home/gregusio/Documents/advent-of-code/04/source.txt";
        return File.ReadAllText(fileName);
    }
    
    public void Run()
    {
        string input = ReadFile();
        //Console.Write(CountWinningPoints(input));
        Console.Write(CountScratchcards(input));
    }
}