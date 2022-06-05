using System;
using UnityEngine;
// ReSharper disable InconsistentNaming

public class InventorySorter : MonoBehaviour
{
    public Inventory inventory;
    public int sortBy { get; set; } // 0 = Name, 1 = Price, 2 = Rarity. Use property to bind to UI
    public bool sortAscending { get; set; } = true; // Use property to bind to UI

    /// <summary>
    /// Sort the inventory with the selection sort algorithm.
    /// </summary>
    public void SelectionSort()
    {
        /*
         * The selection sort sorts a selection of elements, by iterating through every element, finding the smallest value that comes afterwards and swaps position with it.
         * This process repeats until every element has been iterated through.
         */

        string smallestString;
        float smallestFloat;

        int smallestIndex;

        for (var i = 0; i < inventory.items.Length; i++)
        {
            smallestString = inventory.items[i].itemName;
            smallestFloat = sortBy == 1 ? inventory.items[i].price : (int)inventory.items[i].rarity;
            smallestIndex = i;

            for (var j = i + 1; j < inventory.items.Length; j++)
            {
                switch (sortBy)
                {
                    case 0:
                        if (IsSmaller(inventory.items[j].itemName, smallestString))
                        {
                            smallestString = inventory.items[j].itemName;
                            smallestIndex = j;
                        }
                        break;

                    case 1:
                        if (IsSmaller(inventory.items[j].price, smallestFloat))
                        {
                            smallestFloat = inventory.items[j].price;
                            smallestIndex = j;
                        }
                        break;

                    case 2:
                        if (IsSmaller((int)inventory.items[j].rarity, smallestFloat))
                        {
                            smallestFloat = (int)inventory.items[j].rarity;
                            smallestIndex = j;
                        }
                        break;
                }
            }

            if (smallestIndex != i)
                (inventory.items[i], inventory.items[smallestIndex]) = (inventory.items[smallestIndex], inventory.items[i]);
        }

        if (!sortAscending)
            Array.Reverse(inventory.items);
        inventory.RenderInventory();
    }

    /// <summary>
    /// Sort the inventory with the insertion sort algorithm.
    /// </summary>
    public void InsertionSort()
    {
        /*
         * Insertion sort sorts a selection of elements, by inserting an evaluated value at the right position.
         * Beginning from the right side and moving to the left, the inserted value switches position with each element until it neighbours with an element that is smaller or equal than itself.
         * This step is repeated until all elements have been evaluated.
         */

        for (int i = 0; i < inventory.items.Length - 1; i++)
        {
            for (int j = i + 1; j > 0; j--)
            {
                if (IsSmaller(inventory.items, j, j - 1))
                    (inventory.items[j], inventory.items[j - 1]) = (inventory.items[j - 1], inventory.items[j]);
            }
        }

        if (!sortAscending)
            Array.Reverse(inventory.items);
        inventory.RenderInventory();
    }

    /// <summary>
    /// Sort the inventory with the bubble sort algorithm. (Maybe I misunderstood something, but isn't this just a reverse insertion sort?)
    /// </summary>
    public void BubbleSort()
    {
        /*
         * Bubble sort sorts a selection of elements, by evaluating two values at once and swap their position if the right side value is lesser than (or the left side is greater than) the other side.
         * Afterwards, the position moves one over (if in step 1, elements 1 and 2 were evaluated, in step 2 it will be element 2 and 3) and repeats the comparison. This effectively pushes the bigger values to the right side with each iteration.
         * In each iteration, the position moves one step less than in the previous iteration. The process repeats until it iterated through all elements.
         */

        for (var i = 0; i < inventory.items.Length; i++)
        {
            for (var j = 1; j < inventory.items.Length; j++)
            {
                if (IsSmaller(inventory.items, j, j - 1))
                    (inventory.items[j], inventory.items[j - 1]) = (inventory.items[j - 1], inventory.items[j]);
            }
        }

        if (!sortAscending)
            Array.Reverse(inventory.items);
        inventory.RenderInventory();
    }

    /// <summary>
    /// Returns whether <see cref="val1"/> is smaller than <see cref="val2"/>
    /// </summary>
    /// <param name="val1">The first value</param>
    /// <param name="val2">The second value</param>
    /// <returns>Whether <see cref="val1"/> is smaller than <see cref="val2"/></returns>
    private bool IsSmaller(float val1, float val2) => val1 < val2;

    /// <summary>
    /// Returns whether <see cref="val1"/> comes before <see cref="val2"/> in an ordinal string comparison.
    /// </summary>
    /// <param name="val1">The first value</param>
    /// <param name="val2">The second value</param>
    /// <returns>Whether <see cref="val1"/> comes before <see cref="val2"/> in an ordinal string comparison</returns>
    private bool IsSmaller(string val1, string val2) => string.Compare(val1, val2, StringComparison.Ordinal) < 0;

    /// <summary>
    /// Compares the property of two items in an array based on <see cref="sortBy"/>.
    /// </summary>
    /// <param name="items">The array of items</param>
    /// <param name="index1">The index of the first item</param>
    /// <param name="index2">The index of the second item</param>
    /// <returns>Whether the item at index <see cref="index1"/> is smaller than the item at index <see cref="index2"/></returns>
    private bool IsSmaller(Item[] items, int index1, int index2)
    {
        var isSmaller = false;
        switch (sortBy)
        {
            case 0:
                isSmaller = IsSmaller(items[index1].itemName, items[index2].itemName);
                break;

            case 1:
                isSmaller = IsSmaller(items[index1].price, items[index2].price);
                break;

            case 2:
                isSmaller = IsSmaller((int)items[index1].rarity, (int)items[index2].rarity);
                break;
        }

        return isSmaller;
    }
}
