using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShrineManager : MonoBehaviour
{
    [SerializeField] private Image shrineDepotBar;
    public float maxShrineDepotProgress;
    [SerializeField] private TextMeshProUGUI shrineDepotText;

    public float currentShrineDepotProgress;

    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private SafeZone safeZone;
    [SerializeField] private LevelPortal levelPortal;

    [SerializeField] private Animator animator;

    [SerializeField] private Material glowMaterial;
    [SerializeField] private MeshRenderer meshRenderer;

    void Start()
    {
        inventoryManager = GameObject.FindWithTag("Player").GetComponent<InventoryManager>();
        safeZone = GameObject.FindWithTag("Safezone").GetComponent<SafeZone>();
        levelPortal = GameObject.FindWithTag("Portal").GetComponent<LevelPortal>();

        currentShrineDepotProgress = 0;
        updateShrineDepotProgress();

        if (currentShrineDepotProgress == 0)
        {
            animator.SetBool("minProg", true);
        }

        // Shrine mesh renderer
        meshRenderer = GetComponent<MeshRenderer>();

    }

    // Update is called once per frame
    void Update()
    {


    }

    public void decreaseShrineDepotProgress(float Amt)
    {
        if (currentShrineDepotProgress > 0)
        {
            currentShrineDepotProgress -= Amt;
            updateShrineDepotProgress();

            if (currentShrineDepotProgress == 0)
            {
                animator.SetBool("minProg", true);
            }

        }

    }

    public void increaseShrineDepotProgress(float Amt)
    {
        if (currentShrineDepotProgress < maxShrineDepotProgress && inventoryManager.currentInventory > 0)
        {
            currentShrineDepotProgress += Amt;

            inventoryManager.removeInventory(1f);

            updateShrineDepotProgress();
            checkDone();

            safeZone.increaseSafeZoneSize();
            levelPortal.checkAndActivatePortal();

            if (currentShrineDepotProgress != 0)
            {
                animator.SetBool("minProg", false);
            }

        }

    }

    private void updateShrineDepotProgress()
    {
        // Clamp Progress 0-max
        currentShrineDepotProgress = Mathf.Clamp(currentShrineDepotProgress, 0, maxShrineDepotProgress);

        // Set Bar
        shrineDepotBar.fillAmount = currentShrineDepotProgress / maxShrineDepotProgress;

        // Set text
        shrineDepotText.text = currentShrineDepotProgress.ToString() + " / " + maxShrineDepotProgress.ToString();

    }

    private void checkDone()
    {
        // Check Done
        if (currentShrineDepotProgress >= maxShrineDepotProgress)
        {
            // Handle Done
            Debug.Log("Level up");

            if (meshRenderer != null)
            {
                // Retrieve the current materials array
                Material[] materials = meshRenderer.materials;

                // Check if there are at least two materials in the array
                if (materials.Length > 1)
                {
                    // Set the second material (index 1) to the new glow material
                    materials[1] = glowMaterial;
                }
                else
                {
                    // If there is only one material, expand the array and add the new material
                    Material[] newMaterials = new Material[materials.Length + 1];
                    for (int i = 0; i < materials.Length; i++)
                    {
                        newMaterials[i] = materials[i];
                    }
                    newMaterials[materials.Length] = glowMaterial;

                    // Apply the updated array back to the MeshRenderer
                    meshRenderer.materials = newMaterials;
                }

                // Optionally, you could directly assign the material array to meshRenderer.materials here as well
                meshRenderer.materials = materials;
            }

        }

    }

}
