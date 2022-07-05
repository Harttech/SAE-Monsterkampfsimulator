using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Item[] items = new Item[30];

    [SerializeField]
    private Item[] itemSelection;

    void Start()
    {
        // Iterate through the itemSelection, select a random item and put it in the inventory at the current position.
        for (int i = 0; i < 30; i++)
        {
            var r = Random.Range(0, itemSelection.Length);
            items[i] = Instantiate(itemSelection[r]);
        }

        // Update the UI
        RenderInventory();
    }

    /// <summary>
    /// Update the inventory UI
    /// </summary>
    public void RenderInventory()
    {
        for (int i = 0; i < 30; i++)
            transform.GetChild(i).GetComponent<Image>().sprite = items[i].icon;
    }
}
