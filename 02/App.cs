namespace _02;

public class App
{
    private bool CheckIfGoodRange(string[] tuple)
    {
        int numOfColor = Int32.Parse(tuple[0]);
        switch (tuple[1])
        {
            case "red": return numOfColor <= 12;
            case "green": return numOfColor <= 13;
            case "blue": return numOfColor <= 14;
            default: return false;
        }
    }
    
    private int CountIds(string input)
    {
        string[] lines = input.Split("\n");
        int res = 0;
        foreach (var line in lines)
        {
            int id = Int32.Parse(line[new Range(5, line.IndexOf(':'))]);
            var line1 = line.Replace(":", ";");
            var tmpSets = line1.Split(";");
            var sets = tmpSets[new Range(1, tmpSets.Length)];
            bool condition = true;
            foreach (var s in sets)
            {
                var colors = s.Split(",");
                foreach (var color in colors)
                {
                    var color1 = color.Trim();
                    var tuple = color1.Split(" ");
                    if (!CheckIfGoodRange(tuple))
                        condition = false;
                }
            }

            if (condition)
                res += id;

        }

        return res;
    }
    
    private int MinimalCountIds(string input)
    {
        string[] lines = input.Split("\n");
        int res = 0;
        foreach (var line in lines)
        {
            int id = Int32.Parse(line[new Range(5, line.IndexOf(':'))]);
            var line1 = line.Replace(":", ";");
            var tmpSets = line1.Split(";");
            var sets = tmpSets[new Range(1, tmpSets.Length)];
            
            int red = 0;
            int green = 0;
            int blue = 0;
            
            foreach (var s in sets)
            {
                var colors = s.Split(",");
                foreach (var color in colors)
                {
                    var color1 = color.Trim();
                    var tuple = color1.Split(" ");
                    var count = Int32.Parse(tuple[0]);
                    switch (tuple[1])
                    {
                        case "red":
                            if (count > red)
                                red = count;
                            break;
                        case "green":
                            if (count > green)
                                green = count;
                            break;
                        case "blue":
                            if (count > blue)
                                blue = count;
                            break;
                        default:
                            break;
                    }

                }
            }

            res += (red * green * blue);

        }

        return res;
    }
    
    private string ReadFile()
    {
        string fileName = "/home/gregusio/Documents/advent-of-code/02/source.txt";
        return File.ReadAllText(fileName);
    }
    
    public void Run()
    {
        string input = ReadFile();
        //Console.WriteLine(CountIds(input));
        Console.WriteLine(MinimalCountIds(input));
    }
}