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

    [SerializeField] private GameObject fishesContainer;
    private Vector3 _rotAng = new Vector3(0, 5, 0);

    private Vector3 testLocation = new Vector3(0.2f, 0.2f, 0.2f);
    private Vector3 _offset = new Vector3(0.02f, 0.02f, 0.02f);

    public Transform ARContainer;
    public GameObject WhalePrefab;
    public Camera playerPos;
    private Vector3 _playerOffset = new Vector3(0f, 0.000f, 2f);

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

        fishesContainer.transform.Rotate(_rotAng * Time.deltaTime);
    }

    public void SelectionHandler(string word, Vector3 worldSpaceCoord)
    {
        var label = Instantiate(wordPrefab, worldSpaceCoord, Quaternion.identity, wordContainer);
        label.GetComponent<ButtonConfigHelper>().MainLabelText = word;

        SelectionMenu.SetActive(true);
        SelectionMenu.transform.position = label.transform.position + _offset;

    }

    public void OneTapHandler()
    {
        LookUpMenu.SetActive(true);
         LookUpMenu.transform.position = SelectionMenu.transform.position + _offset;

        SelectionMenu.SetActive(false);
    }

    public void TwoTapHandler()
    {
        SearchUpMenu.SetActive(true);
        SearchUpMenu.transform.position = SelectionMenu.transform.position + _offset * 2;

        SelectionMenu.SetActive(false);
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void QuitApplication()
    {
        Application.Quit();
    }

    public void CreateWhale()
    {
        var model = Instantiate(WhalePrefab, ARContainer);
        model.transform.position = playerPos.transform.position + _playerOffset;

    }
}
