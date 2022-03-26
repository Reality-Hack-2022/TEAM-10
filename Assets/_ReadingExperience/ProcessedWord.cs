using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessedWord
{
    public string word;
    public float[] centerCoordinates;

    public ProcessedWord(string word, float[] centerCoordinates)
    {
        this.word = word;
        this.centerCoordinates = centerCoordinates;
    }
}
