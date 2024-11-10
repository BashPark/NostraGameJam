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
    [SerializeField] private ShrineManager shrineManager;

    public float currentInventory;

    [SerializeField] private Animator animator;

    void Start()
    {
        shrineManager = GameObject.FindWithTag("Shrine").GetComponent<ShrineManager>();

        currentInventory = 0;
        updateInventroy();

        // Start anims
        if (currentInventory == 0)
        {
            animator.SetBool("minInv", true);
        }

    }

    public void removeInventory(float inv)
    {
        if( currentInventory > 0 )
        {
            // Remove Inventory
            currentInventory -= inv;

            // Display Inventory
            updateInventroy();

            if ( currentInventory == 0 )
            {
                animator.SetBool("minInv", true);
            }
            

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

            if (currentInventory != 0)
            {
                animator.SetBool("minInv", false);
            }

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
