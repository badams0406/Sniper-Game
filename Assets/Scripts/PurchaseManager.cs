using UnityEngine;
using UnityEngine.UI;

public static class PurchaseManagerStatic
{
    public static bool suppressorPurchased = false;
    public static bool extendedMagPurchased = false;
    public static bool laserSightPurchased = false;
}
public class PurchaseManager : MonoBehaviour
{
    public Button suppressorButton;
    public Button extendedMagButton;
    public Button laserSightButton;

    private MoneyHUD moneyHUD;

    void Start()
    {
        moneyHUD = FindObjectOfType<MoneyHUD>(); // Find the MoneyHUD component in the scene
        if (moneyHUD == null)
        {
            Debug.LogError("MoneyHUD component not found in the scene!");
            return;
        }

        // Update button visibility based on previous purchases
        suppressorButton.gameObject.SetActive(!PurchaseManagerStatic.suppressorPurchased);
        suppressorButton.gameObject.SetActive(!PurchaseManagerStatic.extendedMagPurchased);
        suppressorButton.gameObject.SetActive(!PurchaseManagerStatic.laserSightPurchased);

        // Add onClick events to the buttons
        suppressorButton.onClick.AddListener(BuySuppressor);
        suppressorButton.onClick.AddListener(BuyExtendedMag);
        suppressorButton.onClick.AddListener(BuyLaserSight);
    }

    void BuySuppressor()
    {
        int suppressorCost = 2000; // Set suppressor cost
        if (!PurchaseManagerStatic.suppressorPurchased && moneyHUD.Balance >= suppressorCost)
        {
            moneyHUD.Balance -= suppressorCost;
            // Perform actions for purchasing suppressor
            Debug.Log("Suppressor purchased!");
            PurchaseManagerStatic.suppressorPurchased = true;
            suppressorButton.gameObject.SetActive(false);

        }
        else if (PurchaseManagerStatic.suppressorPurchased)
        {
            Debug.Log("Suppressor already purchased!");
        }
        else
        {
            Debug.Log("Insufficient funds to buy suppressor!");
        }
    }

void BuyExtendedMag()
    {
        int extendedMagCost = 4000; // Set extended mag cost
        if (!PurchaseManagerStatic.extendedMagPurchased && moneyHUD.Balance >= extendedMagCost)
        {
            moneyHUD.Balance -= extendedMagCost;
            // Perform actions for purchasing extended mag
            Debug.Log("Extended Mag purchased!");
            PurchaseManagerStatic.extendedMagPurchased = true;
            extendedMagButton.gameObject.SetActive(false);
        }
        else if (PurchaseManagerStatic.extendedMagPurchased)
        {
            Debug.Log("Extended Mag already purchased!");
        }
        else
        {
            Debug.Log("Insufficient funds to buy extended mag!");
        }
    }

    void BuyLaserSight()
    {
        int laserSightCost = 2000; // Set laser sight cost
        if (!PurchaseManagerStatic.laserSightPurchased && moneyHUD.Balance >= laserSightCost)
        {
            moneyHUD.Balance -= laserSightCost;
            // Perform actions for purchasing laser sight
            Debug.Log("Laser Sight purchased!");
            PurchaseManagerStatic.laserSightPurchased = true;
            laserSightButton.gameObject.SetActive(false);
        }
        else if (PurchaseManagerStatic.laserSightPurchased)
        {
            Debug.Log("Laser Sight already purchased!");
        }
        else
        {
            Debug.Log("Insufficient funds to buy laser sight!");
        }
    }
}
