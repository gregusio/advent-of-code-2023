using System.Text.RegularExpressions;

namespace _01;

public class App
{
    private int StringToInt(string str)
    {
        switch (str)
        {
            case "one": return 1;
            case "two": return 2;
            case "three": return 3;
            case "four": return 4;
            case "five": return 5;
            case "six": return 6;
            case "seven": return 7;
            case "eight": return 8;
            case "nine": return 9;
            default: return Int32.Parse(str);
        }
    }
    
    private int AdvancedCountNum(string input)
    {
        int res = 0;
        string[] lines = input.Split("\n");
        foreach (var line in lines)
        {
            var regex = new Regex("one|two|three|four|five|six|seven|eight|nine|[0-9]");
            var matches = regex.Matches(line);
            
            var tmp = matches.Count > 0 ? line.LastIndexOf(matches[^1].Value, StringComparison.Ordinal) : -1;
            var end = line.Substring(tmp + 1);
            var matches1 = regex.Matches(end);
            
            var numbers = new string[2] {"0", "0"};
            if (matches.Count == 1)
            {
                numbers[0] = matches[0].Value;
                if (matches1.Count > 0)
                    numbers[1] = matches1[0].Value;
                else 
                    numbers[1] = numbers[0];
            }
            else if(matches.Count > 1)
            {
                numbers[0] = matches[0].Value;
                if (matches1.Count > 0)
                    numbers[1] = matches1[0].Value;
                else 
                    numbers[1] = matches[^1].Value;
                
            }

            res += 10 * StringToInt(numbers[0]) + StringToInt(numbers[1]);
        }

        return res;
    }
    
    private int CountNum(string input)
    {
        int res = 0;
        var regex = new Regex("[^0-9]");
        string[] lines = input.Split("\n");
        foreach (var line in lines)
        {
            string numInLine = regex.Replace(line, "");
            if (numInLine.Length == 1)
                numInLine += numInLine;
            else if (numInLine.Length > 1)
                numInLine = string.Concat(numInLine[0], numInLine[^1])
                    ;
            if (Int32.TryParse(numInLine, out int num))
                res += num;
        }

        return res;
    } 
    
    private string ReadFile()
    {
        string fileName = "/home/gregusio/Documents/advent-of-code/01/source.txt";
        return File.ReadAllText(fileName);
    }
    
    public void Run()
    {
        string input = ReadFile();
        //Console.WriteLine(CountNum(input));
        Console.WriteLine(AdvancedCountNum(input));
    }
}