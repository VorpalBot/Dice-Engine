using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dice_Engine.Dice_Engine
{
    /// <summary>
    /// Implementation of dice-rolling for Vorpal. This interprets and executes commands according to the grammar
    /// defined in the README of this repository.
    /// </summary>
    public static class Dice
    {
        static Random Rng = new Random();

        /// <summary>
        /// Computes the result of basic rolls such a 8d6 and d20. This does not store and return the individual dice rolls 
        /// </summary>
        /// <param name="num">The number of dice being rolled. </param>
        /// <param name="sides">The number of sides of each die. Also the maximum number of an individual die roll. </param>
        /// <returns>The total of rolling n s-sided die. Does not store each roll discretely. </returns>
        private static int basicDiceRoll(int num=1, int sides = 4)
        {
            if (num < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(num), num, "Number of dice being roled must be at least 1.");
            }
            if (sides < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(sides), sides, "Must roll dice with at least 2 sides.");
            }

            return Rng.Next(num, sides * num + 1); 
        }

        /// <summary>
        /// Computes the result of basic dice rolls such as 8d6 and d20. This stores and returns the result of individual dice rolls. 
        /// </summary>
        /// <param name="num">The number of dice being rolled. </param>
        /// <param name="sides">The number of sides of each die. Also the maximum number of an individual die roll. </param>
        /// <returns>An array containing the result of the dice rolls, these can be summed to find the total. </returns>
        private static int[] IndividualDiceRolls(int num = 1, int sides = 4)
        {
            if (num < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(num), num, "Number of dice being rolled must be at least 1. "); 
            }
            if (sides < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(sides), sides, "Must roll dice with at least two sides. "); 
            }
            int[] rolls = new int[num];
            for (int i = 0; i < num; i++)
            {
                rolls[i] = Rng.Next(sides + 1);
            }
            return rolls;
        }


    }
}
