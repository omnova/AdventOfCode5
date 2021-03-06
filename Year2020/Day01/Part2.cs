﻿using System;
using System.Linq;

namespace AdventOfCode.Year2020.Day01
{
  public class Part2 : IPuzzle
  {
    public object Run(string input)
    {
      var expenses = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

      for (int i = 0; i < expenses.Count; i++)
      {
        for (int j = 0; j < expenses.Count; j++)
        {
          if (j == i)
            continue;

          for (int k = 0; k < expenses.Count; k++)
          {
            if (k == i || k == j)
              continue;

            if (expenses[i] + expenses[j] + expenses[k] == 2020)
              return (expenses[i] * expenses[j] * expenses[k]);
          }
        }
      }

      return null;
    }
  }
}
