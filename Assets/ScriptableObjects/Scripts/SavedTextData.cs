using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Saved Text Data", menuName = "Scriptable Objects/Text/Saved Text Data", order = 0)]
public class SavedTextData : ScriptableObject
{
    [Header("Display Properties")]
    [SerializeField] private float timestamp;
    public float Timestamp => timestamp;   
    
    [SerializeField] private string savedText;
    public string SavedText => savedText;

    [SerializeField] private GameObject book;
    public GameObject Book => book;

    [SerializeField] private GameObject threeDRepresentation;
    public GameObject ThreeDRepresentation => threeDRepresentation;



}
