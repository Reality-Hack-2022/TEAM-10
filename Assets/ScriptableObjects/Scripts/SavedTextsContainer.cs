using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Saved Text Container", menuName = "Scriptable Objects/Text/Saved Text Container", order = 1)]
public class SavedTextsContainer : ScriptableObject
{
    public List<SavedTextData> savedItems = new List<SavedTextData>();
    //public List<SavedTextData> SavedItems => savedItems;
}
