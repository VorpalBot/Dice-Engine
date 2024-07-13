# Dice Engine
A simple C# console application for interacting with the Vorpal dice engine. This engine is responsible for  handling all of the dice rolls within the Vorpal discord bot. 


basic dice rolling => adb where a = number of rolls, b = max number of dice. 
	- minimum, maximum dice (roll a d6 with a minimum of 2 => range is 2-6)
	- can get set of rolls and not a total => optional functions for set of rolls, filtering, max, min, unique die, 
	
dice rolling with modifiers => adb+i where i is an integer or decimal number
dice rolling with dnd numbers => proficiency, ability score modifiers, advantage, disadvantage, crits, effects
conditional rolling => [if{adb>5}:5d6][elif{adb>10}:6d6][else:10d6]
