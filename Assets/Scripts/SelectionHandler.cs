using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionHandler : MonoBehaviour
{
    [SerializeField] private List<SavedTextData> textDB = new List<SavedTextData>();

    public void OnTextHighlight(string text)
    {

        foreach(SavedTextData data in textDB)
        {
            if (text == data.SavedText) MakePrefabAppear(data);
        }

    }

    public void MakePrefabAppear( SavedTextData data )
    {
        Instantiate(data.Prefab);
    }
}
