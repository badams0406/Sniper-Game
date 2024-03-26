using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public bool boughtSuppressor1;
    public bool boughtExtendedMag1;
    public bool boughtLaserAttach1;
    public float timer;
    public MoneyHUD moneyHUD;

    public bool isLevel;
    public GameObject Supressor;

    public Button suppressorButton;
    public Button extendedMagButton;
    public Button laserSightButton;


    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        boughtSuppressor1 = data.boughtSuppressor;
        boughtExtendedMag1 = data.boughtExtendedMag;
        boughtLaserAttach1 = data.boughtLaserAttach;
    }
    public void Start()
    {
        LoadPlayer();

        // Update button visibility based on previous purchases
        suppressorButton.gameObject.SetActive(!PurchaseManagerStatic.suppressorPurchased);
        extendedMagButton.gameObject.SetActive(!PurchaseManagerStatic.extendedMagPurchased);
        laserSightButton.gameObject.SetActive(!PurchaseManagerStatic.laserSightPurchased);

        // Add onClick events to the buttons
        suppressorButton.onClick.AddListener(BuySuppressor);
        extendedMagButton.onClick.AddListener(BuyExtendedMag);
        laserSightButton.onClick.AddListener(BuyLaserSight);
    }

    public void Level()
    {
        if (isLevel == true)
        {
            if (boughtSuppressor1 == true)
            {
                Supressor.gameObject.SetActive(true);
            }
        }
        else
        {
            if (Supressor.gameObject == null) 
            {
                return;
            }
        }
    }

    IEnumerator SaveTimer()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 )
        {
            SavePlayer();
            timer = 10;
        }
        yield return new WaitForSeconds(10);
        yield return null;
    }

    public void Update()
    {
        SaveTimer();
        Level();
    }
    public void BuySuppressor()
    {
        int suppressorCost = 2000; // Set suppressor cost
        if (!PurchaseManagerStatic.suppressorPurchased && moneyHUD.Balance >= suppressorCost)
        {
            moneyHUD.Balance -= suppressorCost;
            // Perform actions for purchasing suppressor
            Debug.Log("Suppressor purchased!");
            PurchaseManagerStatic.suppressorPurchased = true;
            suppressorButton.gameObject.SetActive(false);
            boughtSuppressor1 = true;
            SavePlayer();
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

    public void BuyExtendedMag()
    {
        int extendedMagCost = 4000; // Set extended mag cost
        if (!PurchaseManagerStatic.extendedMagPurchased && moneyHUD.Balance >= extendedMagCost)
        {
            moneyHUD.Balance -= extendedMagCost;
            // Perform actions for purchasing extended mag
            Debug.Log("Extended Mag purchased!");
            PurchaseManagerStatic.extendedMagPurchased = true;
            extendedMagButton.gameObject.SetActive(false);
            boughtExtendedMag1 = true;
            SavePlayer();
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

    public void BuyLaserSight()
    {
        int laserSightCost = 2000; // Set laser sight cost
        if (!PurchaseManagerStatic.laserSightPurchased && moneyHUD.Balance >= laserSightCost)
        {
            moneyHUD.Balance -= laserSightCost;
            // Perform actions for purchasing laser sight
            Debug.Log("Laser Sight purchased!");
            PurchaseManagerStatic.laserSightPurchased = true;
            laserSightButton.gameObject.SetActive(false);
            boughtLaserAttach1 = true;
            SavePlayer();
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
