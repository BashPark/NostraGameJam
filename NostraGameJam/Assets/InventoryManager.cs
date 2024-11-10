using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Image inventoryBar;
    public float maxInventory;
    [SerializeField] private TextMeshProUGUI inventoryText;

    public float currentInventory;


    void Start()
    {
        currentInventory = 0;
        updateInventroy();

    }

    public void removeInventory(float inv)
    {
        if(currentInventory > 0)
        {
            // Remove Inventory
            currentInventory -= inv;

            // Display Inventory
            updateInventroy();

        }

    }

    public void addInventory(float inv)
    {
        if (currentInventory < maxInventory)
        {
            // Add Inventory
            currentInventory += inv;

            // Display Inventory
            updateInventroy();
        }
    }

    private void updateInventroy()
    {
        // Clamp health 0-max
        currentInventory = Mathf.Clamp(currentInventory, 0, maxInventory);

        // Set Bar
        inventoryBar.fillAmount = currentInventory / maxInventory;

        // Set Text
        inventoryText.text = currentInventory.ToString() + " / " + maxInventory.ToString();

    }

}
