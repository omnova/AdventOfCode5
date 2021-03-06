﻿using System;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode.Year2016.Day11
{
  public class Part2 : IPuzzle
  {
    public object Run(string input)
    {
      var floors = new List<int> { 8, 5, 1, 0 };

      int elevatorFloor = 0;
      int steps = 0;

      while (floors[3] != floors.Sum())
      {
        if (elevatorFloor == 0 || floors[elevatorFloor - 1] == 0)
        {
          floors[elevatorFloor] -= 2;
          elevatorFloor++;
          floors[elevatorFloor] += 2;

          steps++;
        }
        else
        {
          floors[elevatorFloor]++;
          floors[elevatorFloor - 1]--;

          steps += 2;
        }
      }

      return steps.ToString();
    }
  }
}
