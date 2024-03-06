using UnityEngine;
using TMPro;

public class MoneyHUD : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI balanceText;

    private int balance = 2000;

    private void Awake()
    {
        UpdateHUD();
    }

    public int Balance
    {
        get
        {
            return balance;
        }
        set
        {
            balance = value;
            UpdateHUD();
        }
    }

    private void UpdateHUD()
    {
        balanceText.text = balance.ToString();
    }
}
