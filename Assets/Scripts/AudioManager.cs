using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<AudioSource> audioList = new List<AudioSource>();

    private void Start()
    {
        for (int i = 0; i < audioList.Count; i++)
        {
            //...play them one at the time?
        }
    }

}
