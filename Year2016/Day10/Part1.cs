﻿using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode.Year2016.Day10
{
  public class Part1 : IPuzzle
  {
    public object Run(string input)
    {
      var bots = Regex.Matches(input, @"bot \d+")
        .Cast<object>()
        .Select(match => int.Parse(match.ToString().Substring(4)))
        .Distinct()
        .OrderBy(b => b)
        .Select(b => new Bot(b))
        .ToList();

      Regex.Matches(input, @"value \d+ goes to bot \d+")
        .Cast<object>()
        .Select(match => match.ToString().Split(' '))
        .Select(line => new { value = int.Parse(line[1]), bot = int.Parse(line[5]) })
        .ToList()
        .ForEach(c => bots.First(b => b.Id == c.bot).TakeChip(c.value));

      var outputs = Regex.Matches(input, @"output \d+")
        .Cast<object>()
        .Select(match => int.Parse(match.ToString().Substring(7)))
        .Distinct()
        .OrderBy(b => b)
        .Select(b => new Output(b))
        .ToList();

      foreach (string instruction in input.Split(new string[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries).Where(i => i.Substring(0, 3) == "bot").ToList())
      {
        var operands = instruction.Split();
        var bot = bots.First(b => b.Id == int.Parse(operands[1]));

        bot.LowTarget = (operands[5] == "bot" ? bots.First(b => b.Id == int.Parse(operands[6])) : outputs.First(o => o.Id == int.Parse(operands[6])) as IChipReceiver);
        bot.HighTarget = (operands[10] == "bot" ? bots.First(b => b.Id == int.Parse(operands[11])) : outputs.First(o => o.Id == int.Parse(operands[11])) as IChipReceiver);
      }

      while (!bots.Any(b => b.Chips.Contains(61) && b.Chips.Contains(17)))
      {
        bots.Where(b => b.Chips.Count() > 1).ToList().ForEach(b => b.DisperseChips());
      }

      return bots.FirstOrDefault(b => b.Chips.Contains(61) && b.Chips.Contains(17)).Id.ToString();
    }
  }
}
