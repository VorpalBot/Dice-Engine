using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dice_Engine.Dice_Engine
{
    /// <summary>
    /// Implementation of dice-rolling for Vorpal. This interprets and executes commands according to the grammar
    /// defined in the README of this repository.
    /// </summary> 
    public static class DiceRoller
    {
        static Random Rng = new Random();

        public static int[] roll(string command)
        {
            // Getting all modifiers applied to the roll such as advantage, bless, etc.
            Match rollEffects = Regex.Match(command, "{(.*?)}");
            GroupCollection effects = rollEffects.Groups; 

        }

        /// <summary>
        /// Computes the result of basic rolls such a 8d6 and d20. This does not store and return the individual dice rolls. 
        /// This has been written in this form because the minimum number is simply the number of dice and the max is the multiple of the 
        /// number of dice and their sides, meaning only one random number needs to be generated.
        /// </summary>
        /// <param name="num">The number of dice being rolled. </param>
        /// <param name="sides">The number of sides of each die. Also the maximum number of an individual die roll. </param>
        /// <returns>The total of rolling n s-sided die. Does not store each roll discretely. </returns>
        private static int[] rollBasic(int num = 1, int sides = 4) => new int[] { Rng.Next(num, sides * num + 1) };

        /// <summary>
        /// Computes the result of basic dice rolls such as 8d6 and d20. This stores and returns the result of individual dice rolls. 
        /// </summary>
        /// <param name="num">The number of dice being rolled. </param>
        /// <param name="sides">The number of sides of each die. Also the maximum number of an individual die roll. </param>
        /// <returns>An array containing the result of the dice rolls, these can be summed to find the total. </returns>
        private static int[] roll(int num = 1, int sides = 4)
        {
            int[] rolls = new int[num];
            for (int i = 0; i < num; i++)
            {
                rolls[i] = Rng.Next(sides + 1);
            }
            return rolls;
        }


    }
}
