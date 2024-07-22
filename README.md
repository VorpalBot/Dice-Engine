# Dice Engine
A simple C# console application for interacting with the Vorpal dice engine. This engine is responsible for  handling all of the dice rolls within the Vorpal discord bot. 

The grammar of the valid dice commands is given by the EBNF below. Some valid dice rolls include: 
- `"1d20"`, which rolls a 20-sided die once.
- `"3d6+5"`, which rolls a 6-sided die 3 three times, sums each roll and adds 5 to get the final total.
- `"3d6+5{adv}"`, which does the previous dice roll but at advantage. 
```ebnf
(* The structure in which a dice command will be supplied to the engine *)
command = standardRoll, ["{", effect, "}"]; 

(* Effects that can be applied to the result of rolled dice, these manipulate the rolled outputs *). 
effect = "bless" | "adv" | "dis"

(* Defining the types of rolls that the program can interpret *)
standardRoll = basicRoll, [modifier, natural] ; 
basicRoll = natural, d, natural ; 

(* Underlying definitions for defining terminal roll content *)
natural = digitWithoutZero, {digit} ; 
digit = "0" | digitWithoutZero ; 
digitWithoutZero = "1" | "2" | "3" | "4" | "5" | "6" | "7" | "8" | "9" ; 
modifier = "+" | "-" ; 
```

The current effects that can be applied to dice rolls are: 
- `adv`, advantange. Which rolls the dice roll twice and takes the highest result.
- `dis`, disadvantage. Which rolls the dice roll twice and takes the lowest result.
- `bless`, bless. Adds on `1d4` to the total result of the dice roll. 
