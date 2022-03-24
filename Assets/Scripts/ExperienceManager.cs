using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
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
}
