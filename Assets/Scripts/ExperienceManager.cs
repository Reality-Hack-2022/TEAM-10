using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Microsoft.MixedReality.Toolkit.UI;

public class ExperienceManager : MonoBehaviour
{
    [SerializeField] private GameObject SelectionMenu;
    [SerializeField] private GameObject LookUpMenu;
    [SerializeField] private GameObject SearchUpMenu;

    [SerializeField] private Transform wordContainer;
    [SerializeField] private GameObject wordPrefab;

    private Vector3 testLocation = new Vector3(0.1f, 0.1f, 0.1f);
        private Vector3 _offset = new Vector3(0.02f, 0.02f, 0.02f);

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

    void Awake()
    {
        _instance = this;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B)) SelectionHandler("whale", testLocation);
    }

    public void SelectionHandler(string word, Vector3 worldSpaceCoord)
    {
        var label = Instantiate(wordPrefab, worldSpaceCoord, Quaternion.identity, wordContainer);
        label.GetComponent<ButtonConfigHelper>().MainLabelText = word;

        SelectionMenu.SetActive(true);
        SelectionMenu.transform.position = worldSpaceCoord;

    }

    public void OneTapHandler()
    {
        LookUpMenu.SetActive(true);
         LookUpMenu.transform.position = SelectionMenu.transform.position + _offset;
    }

    public void TwoTapHandler()
    {
        SearchUpMenu.SetActive(true);
        SearchUpMenu.transform.position = LookUpMenu.transform.position + _offset;
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void QuitApplication()
    {
        Application.Quit();
    }
}
