using System.Text.RegularExpressions;

namespace _03;

public class App
{
    private string ReadFile()
    {
        string fileName = "/home/gregusio/Documents/advent-of-code/03/source.txt";
        return File.ReadAllText(fileName);
    }

    private bool IsNum(char c)
    {
        Regex regex = new Regex("[0-9]");
        return regex.IsMatch(c.ToString());
    }

    private bool IsPart(char[][] array, int i, int j)
    {
        Regex regex = new Regex("[^0-9.]");
        int maxI = i + 3;
        int maxJ = j + 3;
        if (i < 0) i = 0;
        if (j < 0) j = 0;
        for (int k = i;k < array.Length && k < maxI; k++)
        {
            for (int l = j;l < array[k].Length && l < maxJ; l++)
            {
                if (regex.IsMatch(array[k][l].ToString()))
                    return true;
            }
        }

        return false;
    }

    private int CountPartNum(string input)
    {
        var array = input.Split("\n").Select(x => x.ToCharArray()).ToArray();
        int res = 0;
        for (int i = 0; i < array.Length; i++)
        {
            for (int j = 0; j < array[i].Length; j++)
            {
                string num = "";
                bool isPart = false;
                while (j < array[i].Length && IsNum(array[i][j]))
                {
                    num += array[i][j];
                    if (IsPart(array, i - 1, j - 1))
                        isPart = true;
                    j++;
                }

                if (isPart)
                    res += Int32.Parse(num);

            }
        }

        return res;
    }

    private bool IsGear(char[][] array, int i, int j)
    {
        int maxI = i + 3;
        int maxJ = j + 3;
        if (i < 0) i = 0;
        if (j < 0) j = 0;
        int numAmount = 0;
        for (int k = i;k < array.Length && k < maxI; k++)
        {
            for (int l = j;l < array[k].Length && l < maxJ; l++)
            {
                if (IsNum(array[k][l]))
                {
                    numAmount++;
                    l++;
                    if (l < array[k].Length && array[k][l] == '*')
                    {
                        l++;
                        if (l < array[k].Length && IsNum(array[k][l]))
                            numAmount++;
                    }
                    else if (l < array[k].Length && IsNum(array[k][l]))
                        l = maxJ;
                }
            }
        }

        if (numAmount == 2)
            return true;
        
        return false;
    }

    private int[] FindNum(char[][] array, int i, int j)
    {
        int maxI = i + 3;
        int maxJ = j + 3;
        if (i < 0) i = 0;
        if (j < 0) j = 0;
        int[] res = new int[2];
        int m = 0;
        for (int k = i;k < array.Length && k < maxI; k++)
        {
            for (int l = j;l < array[k].Length && l < maxJ; l++)
            {
                if (IsNum(array[k][l]))
                {
                    while (l >= 0 && IsNum(array[k][l]))
                    {
                        l--;
                    }

                    l++;

                    string num = "";
                    while (l < array[k].Length && IsNum(array[k][l]))
                    {
                        num += array[k][l];
                        l++;
                    }

                    res[m++] = Int32.Parse(num);
                }
            }
        }

        return res;
    }

    private int CountGearRatio(string input)
    {
        var array = input.Split("\n").Select(x => x.ToCharArray()).ToArray();
        int res = 0;
        for (int i = 0; i < array.Length; i++)
        {
            for (int j = 0; j < array[i].Length; j++)
            {
                if (array[i][j] == '*')
                    if (IsGear(array, i - 1, j - 1))
                    {
                        var nums = FindNum(array, i - 1, j - 1);
                        res += (nums[0] * nums[1]);
                    }


            }
        }

        return res;
       
    }
    
    public void Run()
    {
        string input = ReadFile();
        //Console.WriteLine(CountPartNum(input));
        Console.WriteLine(CountGearRatio(input));
    }
}