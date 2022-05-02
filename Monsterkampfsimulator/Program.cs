using System.Security.Cryptography;
using Monsterkampfsimulator;
using static SharedUtilities.Extensions.ConsoleHelper;

// Collection of body parts, assuming a humanoid anatomy for the monsters. Is it racist to make assumptions about a race's anatomy? I hope not.
var bodyParts = new[]
{
    "head",
    "left shoulder",
    "right shoulder",
    "left arm",
    "right arm",
    "left hand",
    "right hand",
    "left leg",
    "right leg",
    "torso",
    "back",
    "butt",
    "left foot",
    "right foot",
    "chest",
    "stomach",
    "throat",
    "neck"
};

// Collection of attack methods to attack body parts. Technically, this can result in punching someone's foot...which is a weird way to attack but maybe it's just the monster's culture. Let's not judge (my laziness).
var attackMethods = new[]
{
    "stabs",
    "cuts",
    "slices",
    "smashes",
    "burns",
    "scratches",
    "shoots",
    "punches",
    "kicks"
};

PrintHeader();

Monster monster1, monster2;

// Loop to allow the user to correct their input if they aren't satisfied with the monster's configuration
while (true)
{
    PrintLine("Now create the first monster!");
    monster1 = Monster.CreateMonster();

    PrintEmptyLine();

    PrintLine("You created the following monster: ");
    PrintLine(monster1.ToString(), ConsoleColor.DarkYellow);

    PrintEmptyLine();
    PrintLine("Are you happy with this configuration? (Y/N)");
    var key = Console.ReadKey();

    if (key.Key == ConsoleKey.Y)
        break;

    //Technically should check for ConsoleKey.N as well but me lazy :(

    PrintHeader();
}

// Loop to allow the user to correct their input if they aren't satisfied with the monster's configuration
while (true)
{
    PrintHeader();
    PrintLine("Great. Now create the second monster!");
    monster2 = Monster.CreateMonster();

    PrintEmptyLine();

    if (monster2.Race == monster1.Race)
    {
        // I guess 'species' would be a better term than race....
        Print("Monsters are no humans! ", ConsoleColor.Red);
        Print("They do not fight ");
        Print("their own kind. ", ConsoleColor.Red);
        PrintLine("Pick a different race.");
        WaitForInput();
        continue;
    }

    PrintLine("You created the following monster: ");
    PrintLine(monster2.ToString(), ConsoleColor.DarkYellow);

    PrintEmptyLine();
    PrintLine("Are you happy with this configuration? (Y/N)");
    var key = Console.ReadKey();

    if (key.Key == ConsoleKey.Y)
        break;

    //Technically should check for ConsoleKey.N as well but me still lazy :(

    PrintHeader();
}

PrintHeader();

var monster1MaxAtk = monster1.AttackStrength + (monster1.AttackStrength * 0.1);
var monster2MaxAtk = monster2.AttackStrength + (monster2.AttackStrength * 0.1);

if (monster1.Defense > monster2MaxAtk)
{
    PrintLine($"The {monster1.Race}'s defense ({monster1.Defense}) is higher than the {monster2.Race}'s max attack strength ({monster2MaxAtk}). The {monster2.Race} will never hurt the {monster1.Race}. This fight is already over....", ConsoleColor.DarkYellow);
    WaitForInput();
    return;
}
else if (monster2.Defense > monster1MaxAtk)
{
    PrintLine($"The {monster2.Race}'s defense ({monster2.Defense}) is higher than the {monster1.Race}'s max attack strength ({monster1MaxAtk}). The {monster1.Race} will never hurt the {monster2.Race}. This fight is already over....", ConsoleColor.DarkYellow);
    WaitForInput();
    return;
}

PrintLine("Now, let the fights begin! Press a key to begin!", ConsoleColor.DarkYellow);
PrintLine("------------------------------------------------", ConsoleColor.DarkYellow);
PrintEmptyLine();
WaitForInput();

var order = new Monster[2];
if (monster1.Speed > monster2.Speed)
{
    order[0] = monster1;
    order[1] = monster2;
}
else
{
    order[0] = monster2;
    order[1] = monster1;
}

var monsterIndex = 0;

var turnCount = 0;

while (true)
{
    var attacker = order[monsterIndex];

    if (monsterIndex == 1)
        monsterIndex--;
    else
        monsterIndex++;

    var defender = order[monsterIndex];

    var attackerColor = attacker.GetMonsterColor();
    var defenderColor = defender.GetMonsterColor();

    turnCount++;
    PrintLine($"Turn {turnCount}", ConsoleColor.Yellow);
    PrintLine("========", ConsoleColor.Yellow);

    // Attack method and body part are purely cosmetic but spice it a bit up, visually and narratively.
    PrintLine($"The {attacker.Race} {GetRandomAttackMethod()} the {defender.Race}'s {GetRandomBodyPart()}!", attackerColor);

    if (defender.Attack(attacker.AttackStrength, out var damage))
    {
        Print($"The {defender.Race} received {Math.Round(damage, 2)} damage and ", defenderColor);
        PrintLine("perishes!", ConsoleColor.Red);

        PrintEmptyLine();

        Print($"The {attacker.Race} is the ");
        Print("winner! ", ConsoleColor.Green);
        Print("The fight lasted for ");
        Print($"{turnCount} ", ConsoleColor.Yellow);
        Print("rounds.");
        break;
    }

    PrintLine($"The {defender.Race} received {Math.Round(damage, 2)} damage but survived the attack with {Math.Round(defender.Health, 2)} HP.", defenderColor);

    PrintEmptyLines(2);

    // Waiting to make the narrative more interesting than just rushing through the battle in an instant.
    await Task.Delay(1000);
}

PrintEmptyLines(2);
PrintLine("The battle is over. May the victor live in eternal glory and the loser perish in shame!", ConsoleColor.Yellow);
WaitForInput();

// Clear the console and print the header.
void PrintHeader()
{
    Console.Clear();
    PrintLine("============================================================", ConsoleColor.Yellow);
    PrintLine("                 Monsterkampfsimulator V1.0", ConsoleColor.Yellow);
    PrintLine("============================================================", ConsoleColor.Yellow);
    PrintEmptyLines();
}

// Return a random method to attack from a predefined pool
string GetRandomAttackMethod()
{
    var rdm = RandomNumberGenerator.GetInt32(0, attackMethods.Length);
    return attackMethods[rdm];
}

// Return a random body part that is being attacked.
string GetRandomBodyPart()
{
    var rdm = RandomNumberGenerator.GetInt32(0, bodyParts.Length);
    return bodyParts[rdm];
}