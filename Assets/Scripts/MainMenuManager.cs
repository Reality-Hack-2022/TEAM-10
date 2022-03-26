using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{


    public void ReadingExperienceLaunch()
    {
        SceneManager.LoadScene(1);
    }
    public void ZoneOutLaunch()
    {
        SceneManager.LoadScene(3);
    }
    public void MindPalaceLaunch()
    {
        SceneManager.LoadScene(2);
    }
    public void QuitApplication()
    {
        Application.Quit();
    }

}
