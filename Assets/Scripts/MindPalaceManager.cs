using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MindPalaceManager : MonoBehaviour
{
    [SerializeField] SavedTextsContainer savedItemsContainer;
    [SerializeField] private GameObject textPrefab; 
    [SerializeField] private Transform container;

    private Vector3 _userLocation = new Vector3(0f, 0f, 0f);

    private void Start()
    {

        foreach (var item in savedItemsContainer.savedItems)
        { 

            switch (item.Kind)
	        {
                case ItemKind.Book:
                    CreateBook(item);
                        break;
                case ItemKind.Quote:
                    CreateQuote(item);
                    break;
                case ItemKind.Prefab:
                    CreatePrefab(item);
                    break;
		        default:
                break;
	        }
	    }
    }

    private void CreateBook(SavedTextData item)
    {
        Instantiate(item.Book, item.Placement, Quaternion.identity, container);
    }
    private void CreateQuote(SavedTextData item)
    {
      var element = Instantiate(textPrefab, item.Placement, Quaternion.identity, container);
        element.GetComponentInChildren<TMP_Text>().text = item.SavedText;
        element.transform.LookAt(_userLocation);
    }
    private void CreatePrefab(SavedTextData item)
    {
        Instantiate(item.ThreeDRepresentation, item.Placement, Quaternion.identity, container);
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
