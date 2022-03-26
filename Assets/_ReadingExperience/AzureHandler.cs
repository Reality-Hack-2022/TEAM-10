using System.Collections;
using System.Collections.Generic;
using System.Text;
using RestClient.Core;
using RestClient.Core.Models;
using TMPro;
using UnityEngine;
using System.IO;
using System.Drawing;
using System;


public class AzureHandler : MonoBehaviour
{
    [SerializeField]
    private string baseUrl = "https://westcentralus.api.cognitive.microsoft.com/vision/v2.0/ocr?language=unk&detectOrientation=true";

    [SerializeField]
    private string clientId;

    [SerializeField]
    private string clientSecret;

    [SerializeField]
    private string localImageOCR = "";

    Texture2D image;

    Coroutine GetOCRResultCoroutine;

    //RequestHeader clientSecurityHeader = new RequestHeader
    //{
    //    Key = "Ocp-Apim-Subscription-Key",
    //    Value = "e0a4be0afdeb4205b98c51c409584aac"
    //};
    //RequestHeader contentTypeHeader = new RequestHeader
    //{
    //    Key = "Content-Type",
    //    Value = "application/octet-stream"
    //};


    void Start()
    {
        
    }

    public void InitAzureRequest(string address)
    {
        var imageBytes = GetImageAsByteArray(address);
        //var imageBytes = data;
        //Debug.Log("imageBytes length: " + imageBytes.Length);
        // setup the request header
        RequestHeader clientSecurityHeader = new RequestHeader
        {
            Key = clientId,
            Value = clientSecret
        };
        // setup the request header
        RequestHeader contentTypeHeader = new RequestHeader
        {
            Key = "Content-Type",
            Value = "application/octet-stream"
        };

        //StartCoroutine(RestWebClient.Instance.HttpPost(baseUrl, imageBytesString, (r) => OnRequestComplete(r), new List<RequestHeader>
        StartCoroutine(RestWebClient.Instance.HttpPost(baseUrl, imageBytes, (r) => OnRequestComplete(r), new List<RequestHeader>
        {
            clientSecurityHeader,
            contentTypeHeader
        }));
    }

    void OnRequestComplete(Response response)
    {
        Debug.Log($"Status Code: {response.StatusCode}");
        //foreach(string s in response.Headers.Keys)
        //{
        //    string headerData;
        //    response.Headers.TryGetValue(s, out headerData);
        //    Debug.Log($"Header " + s + " : " + headerData);
        //}
        
        if(response.Error != "")
            Debug.Log($"Error: {response.Error}");

        string resultURL = response.Headers["Operation-Location"];
        Debug.Log("URL: " + resultURL);

        GetResult(resultURL);
        

    }

    void GetResult(string resultURL)
    {
        RequestHeader clientSecurityHeader = new RequestHeader
        {
            Key = clientId,
            Value = clientSecret
        };
        RequestHeader contentTypeHeader = new RequestHeader
        {
            Key = "Content-Type",
            Value = "application/octet-stream"
        };

        GetOCRResultCoroutine = StartCoroutine(RestWebClient.Instance.HttpGet(resultURL, "", (r) => OnResultsReceived(r, resultURL), new List<RequestHeader>
        {
            clientSecurityHeader,
            contentTypeHeader
        }));

        
    }

    void OnResultsReceived(Response response, string resultURL)
    {
        Debug.Log("GET call response");
        
        //foreach (string s in response.Headers.Keys)
        //{
        //    string headerData;
        //    response.Headers.TryGetValue(s, out headerData);
        //    Debug.Log($"Header " + s + " : " + headerData);
        //}
        if (string.IsNullOrEmpty(response.Error) && !string.IsNullOrEmpty(response.Data))
        {
            Debug.Log(response.Data);

            AzureResponse azureResponse = JsonUtility.FromJson<AzureResponse>(response.Data);

            switch(azureResponse.status)
            {
                case "notStarted":
                    Debug.Log("operation not started.");
                    break;

                case "running":
                    RequestHeader clientSecurityHeader = new RequestHeader
                    {
                        Key = clientId,
                        Value = clientSecret
                    };
                    RequestHeader contentTypeHeader = new RequestHeader
                    {
                        Key = "Content-Type",
                        Value = "application/octet-stream"
                    };
                    StopCoroutine(GetOCRResultCoroutine);
                    GetOCRResultCoroutine = StartCoroutine(RestWebClient.Instance.HttpGet(resultURL, "", (r) => OnResultsReceived(r, resultURL), new List<RequestHeader>
                    {
                        clientSecurityHeader,
                        contentTypeHeader
                    }));
                    break;

                case "failed":
                    Debug.Log("Operation failed");
                    break;

                case "succeeded":
                    StopCoroutine(GetOCRResultCoroutine);
                    PrintText(azureResponse);
                    break;

            }
        }
    }

    void PrintText(AzureResponse azureResponse)
    {
        //foreach(Line l in azureResponse.analyzeResult[0].readResults[0].lines)
        //{
        //    Debug.Log("Text: " + l.text);
        //}

        List<ProcessedWord> words = new List<ProcessedWord>();

        foreach(ReadResult r in azureResponse.analyzeResult.readResults)
        {
            foreach(Line l in r.lines)
            {
                foreach(Word w in l.words)
                {
                    ProcessedWord p = new ProcessedWord(w.text, CalculateCenter(w.boundingBox));
                    words.Add(p);
                    
                }
            }
        }

        foreach(ProcessedWord p in words)
        {
            Debug.Log(p.word + " : " + p.centerCoordinates[0] + ", " + p.centerCoordinates[1]);
        }
    }

    float[] CalculateCenter(int[] b)
    {
        float avgX = 0, avgY = 0;
        for(int i = 0; i < 8; i++)
        {
            if(i%2 == 0)
            {
                avgX += b[i];
            }
            else
            {
                avgY += b[i];
            } 
        }

        avgX /= 4;
        avgY /= 4;
        float[] result = new float[] { avgX, avgY };
        return result;
    }

    public class ImageUrl 
    {
        public string Url;
    }

    private byte[] GetImageAsByteArray(string imageFilePath)
    {
        FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
        BinaryReader binaryReader = new BinaryReader(fileStream);
        return binaryReader.ReadBytes((int)fileStream.Length);

        //MemoryStream memoryStream = new MemoryStream(imageBytes);
        //Image fullImage = Image.FromStream(memoryStream);
        //Image newImage = fullImage.GetThumbnailImage(1280, 720, null, IntPtr.Zero);
        //MemoryStream myResult = new MemoryStream();
        //newImage.Save(myResult, Imaging.ImageFormat.Png);
        //return myResult.ToArray();
    }


}
