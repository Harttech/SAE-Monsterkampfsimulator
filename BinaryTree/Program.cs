using System.Security.Cryptography;
using BinaryTree;
using static System.Console;
using static SharedUtilities.Extensions.ConsoleHelper;

var rootNode = new Node(0, "Root");

var tree = new Tree(rootNode);

// Print initial menu
Print("> ", ConsoleColor.Yellow, resetToDefault: true);
PrintLine("Statistics", ConsoleColor.Yellow, resetToDefault: true);

Print("> ", ConsoleColor.Yellow, resetToDefault: true);
PrintLine("Add element", resetToDefault: true);

Print("> ", ConsoleColor.Yellow, resetToDefault: true);
PrintLine("Find element", resetToDefault: true);

Print("> ", ConsoleColor.Yellow, resetToDefault: true);
PrintLine("Remove element", resetToDefault: true);

Print("> ", ConsoleColor.Yellow, resetToDefault: true);
PrintLine("Quit", resetToDefault: true);

var optionIndex = 0;
while (true)
{
    SetCursorPosition(0, 0);

    var inputKey = ReadKey().Key;

    if (inputKey == ConsoleKey.UpArrow) // Navigate menu
    {
        if (optionIndex == 0)
            optionIndex = 4;
        else
            optionIndex--;
    }
    else if (inputKey == ConsoleKey.DownArrow) // Navigate menu
    {
        if (optionIndex == 4)
            optionIndex = 0;
        else
            optionIndex++;
    }
    else if (inputKey == ConsoleKey.Enter) // Execute action
    {
        switch (optionIndex)
        {
            case 0:
                {
                    Clear();
                    PrintLine($"Node count:\t{tree.NodeCount}");
                    PrintLine($"Tree depth:\t{tree.DeepestLevel}");
                    PrintEmptyLine();
                    Print("Press key to continue...");
                    WaitForInput();
                    Clear();
                }
                break;

            case 1:
                {
                    Clear();
                    Print("Enter value to add to tree: ");
                    var newValue = ReadLine();
                    var key = BitConverter.ToInt16(RandomNumberGenerator.GetBytes(2));
                    PrintEmptyLine();
                    PrintLine($"Value \"{newValue}\" will be added to tree with key {key}");
                    tree.Insert(key, newValue);
                    PrintEmptyLine();
                    Print("Press key to continue...");
                    WaitForInput();
                    Clear();
                }
                break;

            case 2:
                {
                    Clear();
                    var key = (short)ReadInt32("Enter the key of the node: ", cancelCallword: null, min: short.MinValue, max: short.MaxValue).Value;
                    PrintEmptyLine();

                    var node = tree.Find(key);
                    if (node == null)
                        PrintLine($"Could not find a node with key {key}.");
                    else
                    {
                        PrintLine("Found node:");
                        PrintLine($"- Key: {node.Key}");
                        PrintLine($"- Value: {node.Value}");
                        PrintLine($"- Comparisons: {tree.LastFindComparisonCount}");
                    }

                    PrintEmptyLine();
                    Print("Press key to continue...");
                    WaitForInput();
                    Clear();
                }
                break;

            case 3:
                {
                    Clear();
                    Print("WARNING! ", ConsoleColor.Red, resetToDefault: true);
                    PrintLine("Removing a node will lead to removal of its children as well, should it have more than one. Are you sure you want to continue? (Y/N)");

                    if (ReadKey().Key != ConsoleKey.Y)
                        PrintLine("Removal cancelled.");
                    else
                    {
                        Clear();
                        var key = (short)ReadInt32("Enter the key of the node: ", cancelCallword: null, min: short.MinValue, max: short.MaxValue).Value;
                        tree.Remove(key);
                        PrintLine("Node removed!");
                    }

                    PrintEmptyLine();
                    Print("Press key to continue...");
                    WaitForInput();
                    Clear();
                }
                break;

            case 4:
                Environment.Exit(0);
                break;
        }
    }

    // Print menu, highlighting the current option

    Print("> ", ConsoleColor.Yellow, resetToDefault: true);
    PrintLine("Statistics", optionIndex == 0 ? ConsoleColor.Yellow : ConsoleColor.Gray, resetToDefault: true);

    Print("> ", ConsoleColor.Yellow, resetToDefault: true);
    PrintLine("Add element", optionIndex == 1 ? ConsoleColor.Yellow : ConsoleColor.Gray, resetToDefault: true);

    Print("> ", ConsoleColor.Yellow, resetToDefault: true);
    PrintLine("Find element", optionIndex == 2 ? ConsoleColor.Yellow : ConsoleColor.Gray, resetToDefault: true);

    Print("> ", ConsoleColor.Yellow, resetToDefault: true);
    PrintLine("Remove element", optionIndex == 3 ? ConsoleColor.Yellow : ConsoleColor.Gray, resetToDefault: true);

    Print("> ", ConsoleColor.Yellow, resetToDefault: true);
    PrintLine("Quit", optionIndex == 4 ? ConsoleColor.Yellow : ConsoleColor.Gray, resetToDefault: true);
}