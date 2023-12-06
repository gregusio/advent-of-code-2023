using System.Text.RegularExpressions;

namespace _06;

public class App
{
    private string FormatString(string input)
    {
        var regex = new Regex("\\s\\s+");
        return regex.Replace(input, " ");
    }
    
    private string FormatOtherString(string input)
    {
        var regex = new Regex("\\s");
        return regex.Replace(input, "");
    }
    
    private int FirstTask(string input)
    {
        input = FormatString(input);
        var times = input.Split("\n")[0].Substring(6, 13).Split(" ");
        var distance = input.Split("\n")[0].Substring(22, 36).Split(" ");
        var res = 1;
        
        for (var i = 0; i < times.Length; i++)
        {
            var curr = 0;
            for (int j = 1; j < Int32.Parse(times[i]); j++)
            {
                if (Int32.Parse(distance[i]) / j < Int32.Parse(times[i]) - j)
                    curr++;
            }

            res *= curr;
        }

        return res;
    }
    
    private long SecondTask(string input)
    {
        input = FormatOtherString(input);
        var times = input.Substring(5, 8);
        var distance = input.Substring(22, 15);
       
        long curr = 0;
        for (long j = 1; j < long.Parse(times); j++)
        {
            if (long.Parse(distance) / j < long.Parse(times) - j)
                curr++;
        }
        

        return curr;
    }
    
    private string ReadFile()
    {
        string fileName = "/home/gregusio/Documents/advent-of-code/06/source.txt";
        return File.ReadAllText(fileName);
    }
    
    public void Run()
    {
        string input = ReadFile();
        //Console.WriteLine(FirstTask(input));
        Console.WriteLine(SecondTask(input));
    }
}