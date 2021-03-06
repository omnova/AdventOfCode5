﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2017.Day12
{
  public class Part2 : IPuzzle
  {
    public object Run(string input)
    {
      var pipes = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                       .Select(p => p.Split(new string[] { " <-> " }, StringSplitOptions.RemoveEmptyEntries))
                       .Select(p => new
                       {
                         Key = int.Parse(p[0]),
                         Value = p[1].Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()
                       })
                       .ToDictionary(p => p.Key, p => p.Value);

      var groupHashes = new List<string>();

      foreach (var pipe in pipes)
      {
        var group = new List<int> { pipe.Key };

        int lastGroupSize;

        do
        {
          lastGroupSize = group.Count;

          var newProgramIds = new List<int>();

          foreach (int programId in group)
          {
            foreach (int linkedProgramId in pipes[programId])
            {
              if (!group.Contains(linkedProgramId) && !newProgramIds.Contains(linkedProgramId))
                newProgramIds.Add(linkedProgramId);
            }
          }

          group.AddRange(newProgramIds);
          group.Sort();
        }
        while (group.Count != lastGroupSize);

        string groupHash = string.Join(",", group);

        if (!groupHashes.Contains(groupHash))
          groupHashes.Add(groupHash);
      }

      return groupHashes.Count.ToString();
    }
  }
}
