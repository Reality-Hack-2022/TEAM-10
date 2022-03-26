using System;

[Serializable]
public class AzureResponse
{
    public string status;
    public string createdDateTime;
    public string lastUpdatedDateTime;
    public AnalyzeResult analyzeResult;

}

[Serializable]
public class AnalyzeResult
{
    public float version;
    public string modelVersion;
    public ReadResult[] readResults;
}

[Serializable]
public class ReadResult
{
    public int page;
    public float angle;
    public int width;
    public int height;
    public string unit;
    public Line[] lines;
}

[Serializable]
public class Line
{
    public int[] boundingBox;
    public string text;
    public Appearance appearance;
    public Word[] words;
}

[Serializable]
public class Appearance
{
    public string style;
    public float styleConfidence;
}

[Serializable]
public class Word
{
    public int[] boundingBox;
    public string text;
    public float confidence;
}
