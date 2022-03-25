using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    [SerializeField] private GameObject SelectionMenu;
    [SerializeField] private GameObject LookUpMenu;
    [SerializeField] private GameObject SearchUpMenu;

    #region Singleton

    private static ExperienceManager _instance;

    public static ExperienceManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Experience Manager is Null!");
            }

            return _instance;
        }
    }

    #endregion

    public void SelectionHandler()
    {
        SelectionMenu.SetActive(true);
    }

    public void OneTapHandler()
    {
        LookUpMenu.SetActive(true);
    }

    public void TwoTapHandler()
    {
        SearchUpMenu.SetActive(true);
    }
}
