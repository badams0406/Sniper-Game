using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void onPlayButton()
    {
        SceneManager.LoadScene(1);
    }
    public void onLevelSelectButton()
    {

    }
    public void onUpgradeButton()
    {
        SceneManager.LoadScene(4);
    }
    public void onOptionsButton()
    {

    }
    public void onQuitButton()
    {
        Application.Quit();
    }

    public void onBackButton()
    {
        SceneManager.LoadScene(0);
    }
}
