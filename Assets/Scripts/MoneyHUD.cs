using UnityEngine;
using TMPro;

public class MoneyHUD : MonoBehaviour
{
    public static MoneyHUD Instance { get; private set; }

    [SerializeField] TextMeshProUGUI balanceText;
    private int balance = 2000;

    private void Awake()
    {
        balance = PlayerPrefs.GetInt("PlayerBalance", 2000); // Load the balance, default to 2000 if not set
        UpdateHUD();
    }


    public int Balance
    {
        get => balance;
        set
        {
            balance = value;
            PlayerPrefs.SetInt("PlayerBalance", balance); // Save the balance
            PlayerPrefs.Save(); // Make sure to save PlayerPrefs
            UpdateHUD();
        }
    }


    private void UpdateHUD()
    {
        if (balanceText != null)
            balanceText.text = balance.ToString();
    }
}
