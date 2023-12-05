using System.Text.RegularExpressions;

namespace _05;

public class App
{
    private List<string[]> GetMaps(string input)
    {
        var lines = input.Split("\n");
        var res = new List<string[]>();

        var dict = new Dictionary<string, int>();
        
        for (var i = 0; i < lines.Length; i++)
        {
            if (lines[i].Contains("map"))
            {
                dict[lines[i]] = i;
            }
        }
        
        res.Add(lines[new Range(dict["seed-to-soil map:"] + 1, dict["soil-to-fertilizer map:"] - 1)]);
        res.Add(lines[new Range(dict["soil-to-fertilizer map:"] + 1, dict["fertilizer-to-water map:"] - 1)]);
        res.Add(lines[new Range(dict["fertilizer-to-water map:"] + 1, dict["water-to-light map:"] - 1)]);
        res.Add(lines[new Range(dict["water-to-light map:"] + 1, dict["light-to-temperature map:"] - 1)]);
        res.Add(lines[new Range(dict["light-to-temperature map:"] + 1, dict["temperature-to-humidity map:"] - 1)]);
        res.Add(lines[new Range(dict["temperature-to-humidity map:"] + 1, dict["humidity-to-location map:"] - 1)]);
        res.Add(lines[new Range(dict["humidity-to-location map:"] + 1, lines.Length)]);

        return res;
    }

    private void FindLow(long firstNum, long range, List<Dictionary<(long, long), long>> listOfDict, ref long lower)
    {
        bool first = true;
        for (long j = 0; j < range; j++)
        {
            var num = firstNum + j;
            foreach (var dictionary in listOfDict)
            {
                foreach (var dictionaryKey in dictionary.Keys)
                {
                    if (num >= dictionaryKey.Item1 && num <= dictionaryKey.Item2)
                    {
                        num = dictionary[dictionaryKey] + (num - dictionaryKey.Item1);
                        break;
                    }
                }
            }

            if (first)
            {
                lower = num;
                first = false;
            }
            else if (num < lower)
            {
                lower = num;
            }
        }
    }
    
    
    private long FirstTask(string input)
    {
        var maps = GetMaps(input);
        var listOfDict = new List<Dictionary<(long, long), long>>();
        
        foreach (var map in maps)
        {
            var dict = new Dictionary<(long, long), long>();
            foreach (var line in map)
            {
                var array = line.Split(" ");
                var destStart = long.Parse(array[0]);
                var sourceStart = long.Parse(array[1]);
                var range = long.Parse(array[2]);
               

                dict[(sourceStart, sourceStart + range - 1)] = destStart; 
            }
            
            listOfDict.Add(dict);
        }

        bool first = true;
        long lower = 0;
        var seeds = input.Split("\n")[0].Substring(input.IndexOf(':') + 1).Trim().Split(" ");
        foreach (var seed in seeds)
        {
            var num = long.Parse(seed);

            foreach (var dictionary in listOfDict)
            {
                foreach (var dictionaryKey in dictionary.Keys)
                {
                    if (num >= dictionaryKey.Item1 && num <= dictionaryKey.Item2)
                    {
                        num = dictionary[dictionaryKey] + (num - dictionaryKey.Item1);
                        break;
                    }
                }
            }

            if (first)
            {
                lower = num;
                first = false;
            }
            else if (num < lower)
            {
                lower = num;
            }
            
        }

        return lower;
    }

   
    
    private async Task SecondTask(string input)
    {
        var maps = GetMaps(input);
        var listOfDict = new List<Dictionary<(long, long), long>>();
        
        foreach (var map in maps)
        {
            var dict = new Dictionary<(long, long), long>();
            foreach (var line in map)
            {
                var array = line.Split(" ");
                var destStart = long.Parse(array[0]);
                var sourceStart = long.Parse(array[1]);
                var range = long.Parse(array[2]);
               

                dict[(sourceStart, sourceStart + range - 1)] = destStart; 
            }
            
            listOfDict.Add(dict);
        }

        bool first = true;
        long lower = 0;
        var seeds = input.Split("\n")[0].Substring(input.IndexOf(':') + 1).Trim().Split(" ");

        long[] low = new long[10];
        // Thread thread1 = new Thread(() => FindLow(long.Parse(seeds[0]), long.Parse(seeds[1]), listOfDict, ref low1));
        //
        // Thread thread2 = new Thread(() => FindLow(long.Parse(seeds[2]), long.Parse(seeds[3]), listOfDict, ref low2));
        //
        // thread1.Start();
        // thread2.Start();

        // var tasks = new List<Task>();
        // tasks.Add(Task.Run(() => FindLow(long.Parse(seeds[0]), long.Parse(seeds[1]), listOfDict, ref low[0])));
        // tasks.Add(Task.Run(() => FindLow(long.Parse(seeds[2]), long.Parse(seeds[3]), listOfDict, ref low[1])));
        // tasks.Add(Task.Run(() => FindLow(long.Parse(seeds[4]), long.Parse(seeds[5]), listOfDict, ref low[2])));
        // tasks.Add(Task.Run(() => FindLow(long.Parse(seeds[6]), long.Parse(seeds[7]), listOfDict, ref low[3])));
        // tasks.Add(Task.Run(() => FindLow(long.Parse(seeds[8]), long.Parse(seeds[9]), listOfDict, ref low[4])));
        // tasks.Add(Task.Run(() => FindLow(long.Parse(seeds[10]), long.Parse(seeds[11]), listOfDict, ref low[5])));
        // tasks.Add(Task.Run(() => FindLow(long.Parse(seeds[12]), long.Parse(seeds[13]), listOfDict, ref low[6])));
        // tasks.Add(Task.Run(() => FindLow(long.Parse(seeds[14]), long.Parse(seeds[15]), listOfDict, ref low[7])));
        // tasks.Add(Task.Run(() => FindLow(long.Parse(seeds[16]), long.Parse(seeds[17]), listOfDict, ref low[8])));
        // tasks.Add(Task.Run(() => FindLow(long.Parse(seeds[18]), long.Parse(seeds[19]), listOfDict, ref low[9])));
        //
        // await Task.WhenAll(tasks);
        
        Parallel.Invoke(
                () => FindLow(long.Parse(seeds[0]), long.Parse(seeds[1]), listOfDict, ref low[0]),
                () => FindLow(long.Parse(seeds[2]), long.Parse(seeds[3]), listOfDict, ref low[1]),
                () => FindLow(long.Parse(seeds[4]), long.Parse(seeds[5]), listOfDict, ref low[2]),
                () => FindLow(long.Parse(seeds[6]), long.Parse(seeds[7]), listOfDict, ref low[3]),
                () => FindLow(long.Parse(seeds[8]), long.Parse(seeds[9]), listOfDict, ref low[4]),
                () => FindLow(long.Parse(seeds[10]), long.Parse(seeds[11]), listOfDict, ref low[5]),
                () => FindLow(long.Parse(seeds[12]), long.Parse(seeds[13]), listOfDict, ref low[6]),
                () => FindLow(long.Parse(seeds[14]), long.Parse(seeds[15]), listOfDict, ref low[7]),
                () => FindLow(long.Parse(seeds[16]), long.Parse(seeds[17]), listOfDict, ref low[8]),
                () => FindLow(long.Parse(seeds[18]), long.Parse(seeds[19]), listOfDict, ref low[9])
            );
        
        // await Task.Run(() => FindLow(long.Parse(seeds[0]), long.Parse(seeds[1]), listOfDict, ref low[0]));
        // await Task.Run(() => FindLow(long.Parse(seeds[2]), long.Parse(seeds[3]), listOfDict, ref low[1]));
        // await Task.Run(() => FindLow(long.Parse(seeds[4]), long.Parse(seeds[5]), listOfDict, ref low[2]));
        // await Task.Run(() => FindLow(long.Parse(seeds[6]), long.Parse(seeds[7]), listOfDict, ref low[3]));
        // await Task.Run(() => FindLow(long.Parse(seeds[8]), long.Parse(seeds[9]), listOfDict, ref low[4]));
        // await Task.Run(() => FindLow(long.Parse(seeds[10]), long.Parse(seeds[11]), listOfDict, ref low[5]));
        // await Task.Run(() => FindLow(long.Parse(seeds[12]), long.Parse(seeds[13]), listOfDict, ref low[6]));
        // await Task.Run(() => FindLow(long.Parse(seeds[14]), long.Parse(seeds[15]), listOfDict, ref low[7]));
        // await Task.Run(() => FindLow(long.Parse(seeds[16]), long.Parse(seeds[17]), listOfDict, ref low[8]));
        // await Task.Run(() => FindLow(long.Parse(seeds[18]), long.Parse(seeds[19]), listOfDict, ref low[9]));

        // for (var i = 0; i < seeds.Length; i++)
        // {
        //     var firstNum = long.Parse(seeds[i]);
        //     i++;
        //     var range = long.Parse(seeds[i]);
        //     var nextNum = 0;
        //     for (long j = 0; j < range; j++)
        //     {
        //         var num = firstNum + j;
        //         foreach (var dictionary in listOfDict)
        //         {
        //             foreach (var dictionaryKey in dictionary.Keys)
        //             {
        //                 if (num >= dictionaryKey.Item1 && num <= dictionaryKey.Item2)
        //                 {
        //                     num = dictionary[dictionaryKey] + (num - dictionaryKey.Item1);
        //                     break;
        //                 }
        //             }
        //         }
        //
        //         if (first)
        //         {
        //             lower = num;
        //             first = false;
        //         }
        //         else if (num < lower)
        //         {
        //             lower = num;
        //         }
        //     }
        //     
        //     
        // }

        Console.WriteLine(low.Min());
    }
    
    private string ReadFile()
    {
        string fileName = "/home/gregusio/Documents/advent-of-code/05/source.txt";
        return File.ReadAllText(fileName);
    }
    
    public async Task Run()
    {
        string input = ReadFile();
        await SecondTask(input);

    }
}