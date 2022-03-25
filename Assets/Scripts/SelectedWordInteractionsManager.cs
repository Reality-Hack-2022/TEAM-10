using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedWordInteractionsManager : MonoBehaviour
{
    private Vector3 _offset = new Vector3(0.02f, 0.02f, 0.02f);

    private bool _tapped = false;

    public void ClickHandler()
    {
        if(_tapped) ExperienceManager.Instance.OneTapHandler();
        else ExperienceManager.Instance.TwoTapHandler();
    }
}
