using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.WebCam;
using System.Linq;
using UnityEngine.UI;
using System.IO;
using System.Drawing;

public class PhotoHandler : MonoBehaviour
{
    AzureHandler azureHandler;
    Resolution cameraResolution;
    string successFilePath;
    // Start is called before the first frame update
    void Start()
    {
        azureHandler = GetComponent<AzureHandler>();
        
    }

    public void OnScanButtonPress()
    {
        Debug.Log("scan starting...");
        PhotoCapture.CreateAsync(false, OnPhotoCaptureCreated);
    }

    private PhotoCapture photoCaptureObject = null;

    void OnPhotoCaptureCreated(PhotoCapture captureObject)
    {
        photoCaptureObject = captureObject;

        cameraResolution = PhotoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).Last();

        //int maxWidth = 1024;
        //int maxHeight = 1024;

        //Resolution cameraResolution = (from res in PhotoCapture.SupportedResolutions
        //                               where res.width <= maxWidth && res.height <= maxHeight
        //                               select res
        //                               ).OrderByDescending((res) => res.width * res.height).First();

        //Debug.Log("camera resolution: " + cameraResolution.width + ", " + cameraResolution.height);



        CameraParameters c = new CameraParameters();
        c.hologramOpacity = 0.0f;
        c.cameraResolutionWidth = cameraResolution.width;
        c.cameraResolutionHeight = cameraResolution.height;
        c.pixelFormat = CapturePixelFormat.BGRA32;

        captureObject.StartPhotoModeAsync(c, OnPhotoModeStarted);
    }

    void OnStoppedPhotoMode(PhotoCapture.PhotoCaptureResult result)
    {
        photoCaptureObject.Dispose();
        photoCaptureObject = null;
    }

    private void OnPhotoModeStarted(PhotoCapture.PhotoCaptureResult result)
    {
        if (result.success)
        {
            string filename = string.Format(@"CapturedImage{0}_n.jpg", Time.time);
            string filePath = System.IO.Path.Combine(Application.persistentDataPath, filename);
            successFilePath = filePath;
            //photoCaptureObject.TakePhotoAsync(filePath, PhotoCaptureFileOutputFormat.PNG, OnCapturedPhotoToDisk);
            photoCaptureObject.TakePhotoAsync(filePath, PhotoCaptureFileOutputFormat.PNG, OnCapturedPhotoToDisk);
        }
        else
        {
            Debug.LogError("Unable to start photo mode!");
        }
    }


    void OnCapturedPhotoToDisk(PhotoCapture.PhotoCaptureResult result)
    {
        if (result.success)
        {
            Debug.Log("Saved Photo to disk: " + successFilePath);
            photoCaptureObject.StopPhotoModeAsync(OnStoppedPhotoMode);
            Texture2D temp = new Texture2D(cameraResolution.width, cameraResolution.height);
            byte[] fileData = File.ReadAllBytes(successFilePath);
            temp.LoadImage(fileData);    
            Texture2D resized = Resize(temp, 1280, 720);
            byte[] resizedBytes = resized.EncodeToJPG();
            File.WriteAllBytes(Application.persistentDataPath + "/resized.jpg", resizedBytes);
            azureHandler.InitAzureRequest(Application.persistentDataPath + "/resized.jpg");
        }
        else
        {
            Debug.Log("Failed to save Photo to disk");
            successFilePath = "";
        }
    }

    Texture2D Resize(Texture2D texture2D, int targetX, int targetY)
    {
        RenderTexture rt = new RenderTexture(targetX, targetY, 24);
        RenderTexture.active = rt;
        UnityEngine.Graphics.Blit(texture2D, rt);
        Texture2D result = new Texture2D(targetX, targetY);
        result.ReadPixels(new Rect(0, 0, targetX, targetY), 0, 0);
        result.Apply();
        return result;
    }

    //private void OnPhotoModeStarted(PhotoCapture.PhotoCaptureResult result)
    //{
    //    if (result.success)
    //    {
    //        photoCaptureObject.TakePhotoAsync(OnCapturedPhotoToMemory);
    //    }
    //    else
    //    {
    //        Debug.LogError("Unable to start photo mode!");
    //    }
    //}
}
