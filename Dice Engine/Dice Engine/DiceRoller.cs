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
            string nonTerminalCommand = computeTerminalRolls(command); 
            
            //Construct AST with the remaining string. 



            GroupCollection effects = rollEffects.Groups;
            return new int[] { 1 }; 
        }

        // Computes the rolls within the dice command that are terminal ie, no complex parameters. This will make it simpler to 
        // process modifiers and effects after with string handling. In the future this can be used to create a simpler tree as 
        // pre-processing for more complex dice commands such as "1d(1d20)" which contains nested rolling. 
        private static string computeTerminalRolls(string command)
        {
            (int, int) multiRoll;
            int singularRoll;
            int rollResult;  
            //Covers dice rolls in the form adb where a = number of dice rolled, b = number of sides on each die. 
            MatchCollection multiRolls = Regex.Matches(command, "([2-9][\\d]*|1)d([4-9]|[1-9][\\d]+)");
            //Covers die rolls where all 1 die is rolled such as "d10", "d6", etc. 
            MatchCollection singularRolls = Regex.Matches(command, "d([4-9]|[1-9][\\d]+)");

            // If there are terminal multi rolls, compute their result and replace the roll in the string. 
            if (multiRolls.Count != 0)
            {
                foreach (Mastch m in multiRolls)
                {
                    multiRoll = processMultiRolls(m.Value);
                    rollResult = rollBasic(multiRoll.Item1, multiRoll.Item2);
                    command = command.Replace(m.Value, rollResult.ToString());
                }
            }

            // If there are terminal singular rolls, compute their result and replace the roll in the string. 
            if (singularRolls.Count != 0)
            {
                foreach (Match m in singularRolls)
                {
                    singularRoll = processSingularRolls(m.Value);
                    rollResult = rollBasic(singularRoll);
                    command = command.Replace(m.Value, rollResult.ToString());
                }
            }

            return command; 
        }

        // Parses terminal singularRolls to get the number of sides of the die being rolled. Input parameter is 
        // always of the form "db" and so can simply parse the substring of the input. 
        private static int processSingularRolls(string roll) => int.Parse(roll.Substring(1, roll.Length - 1));  
        

        // Parses terminal multirolls into a form to be processed and replaced into the string. 
        // Input is always of form "adb" and so we can simply split by 'd' and extract each of the 
        // required ints. 
        private static (int, int) processMultiRolls(string roll)
        {
            string[] rolls = roll.Split('d');
            return (int.Parse(rolls[0]), int.Parse(rolls[1])); 
        }

        /// <summary>
        /// Computes the result of basic rolls such a 8d6 and d20. This does not store and return the individual dice rolls. 
        /// This has been written in this form because the minimum number is simply the number of dice and the max is the multiple of the 
        /// number of dice and their sides, meaning only one random number needs to be generated.
        /// </summary>
        /// <param name="num">The number of dice being rolled. </param>
        /// <param name="sides">The number of sides of each die. Also the maximum number of an individual die roll. </param>
        /// <returns>The total of rolling n s-sided die. Does not store each roll discretely. </returns>
        private static int rollBasic(int num = 1, int sides = 4) => Rng.Next(num, sides * num + 1);

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
