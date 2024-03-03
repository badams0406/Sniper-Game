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
        SceneManager.LoadScene(5);
    }
    public void onUpgradeButton()
    {
        SceneManager.LoadScene(4);
    }
    public void onOptionsButton()
    {
        SceneManager.LoadScene(6);
    }
    public void onQuitButton()
    {
        Application.Quit();
    }

    public void onBackButton()
    {
        SceneManager.LoadScene(0);
    }

    public void onLevel1Button()
    {
        SceneManager.LoadScene(1);
    }
    public void onLevel2Button()
    {
        SceneManager.LoadScene(2);
    }
    public void onLevel3Button()
    {
        SceneManager.LoadScene(3);
    }
}
