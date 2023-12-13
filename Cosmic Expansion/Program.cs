using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        string filename = @"..\..\..\input.txt";
        var lines = ReadInput(filename);

        int R = lines.Count;
        int C = lines[0].Length;

        var galaxies = new HashSet<(int, int)>();
        for (int i = 0; i < lines.Count; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                if (lines[i][j] == '#')
                {
                    galaxies.Add((j, i));
                }
            }
        }

        var emptyRows = new HashSet<int>();
        var emptyCols = new HashSet<int>();

        for (int row = 0; row < lines.Count; row++)
        {
            if (!lines[row].Contains("#"))
            {
                emptyRows.Add(row);
            }
        }

        for (int col = 0; col < C; col++)
        {
            if (!lines.Any(line => line[col] == '#'))
            {
                emptyCols.Add(col);
            }
        }

        Console.WriteLine(GetTotal(galaxies, emptyRows, emptyCols, 2));
        Console.WriteLine(GetTotal(galaxies, emptyRows, emptyCols, 1_000_000));
    }

    static List<string> ReadInput(string filename)
    {
        return File.ReadAllLines(filename).Select(line => line.Trim()).ToList();
    }

    static long GetTotal(HashSet<(int, int)> galaxies, HashSet<int> emptyRows, HashSet<int> emptyCols, int expansion)
    {
        long total = 0;
        var galaxyList = galaxies.ToList();

        for (int i = 0; i < galaxyList.Count; i++)
        {
            for (int j = i + 1; j < galaxyList.Count; j++)
            {
                var (sx, sy) = galaxyList[i];
                var (ex, ey) = galaxyList[j];
                int manDist = Math.Abs(sx - ex) + Math.Abs(sy - ey);

                for (int row = Math.Min(sy, ey) + 1; row < Math.Max(sy, ey); row++)
                {
                    if (emptyRows.Contains(row))
                    {
                        manDist += expansion - 1;
                    }
                }

                for (int col = Math.Min(sx, ex) + 1; col < Math.Max(sx, ex); col++)
                {
                    if (emptyCols.Contains(col))
                    {
                        manDist += expansion - 1;
                    }
                }

                total += manDist;
            }
        }

        return total;
    }
}
