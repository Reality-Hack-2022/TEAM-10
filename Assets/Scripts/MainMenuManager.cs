using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z)) ReadingExperienceLaunch();
    }
    public void ReadingExperienceLaunch()
    {
        SceneManager.LoadScene(1);
    }


}
