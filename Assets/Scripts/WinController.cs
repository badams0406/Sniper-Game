using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject winMessage;
    public GameObject loseMessage;

    void Start()
    {
        winMessage.SetActive(false);
        loseMessage.SetActive(false);
    }

    public void ShowWinMessage()
    {
        winMessage.SetActive(true);
        Invoke("GoToMainMenu", 5f); // Adjust delay as needed
    }

    public void ShowLoseMessage()
    {
        loseMessage.SetActive(true);
        Invoke("GoToMainMenu", 5f); // Adjust delay as needed
    }

    void GoToMainMenu()
    {
        SceneManager.LoadScene("main_menu");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
