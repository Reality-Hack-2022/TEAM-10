using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

public class IndexPointerTracker : MonoBehaviour
{
    public GameObject calibrationPlane;
    public bool isCalibrated;
    public Vector3 calibrationPosition;
    // Start is called before the first frame update
    void Start()
    {
        isCalibrated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isCalibrated)
        {
            foreach (var source in MixedRealityToolkit.InputSystem.DetectedInputSources)
            {
                // Ignore anything that is not a hand because we want articulated hands
                if (source.SourceType == Microsoft.MixedReality.Toolkit.Input.InputSourceType.Hand)
                {
                    foreach (var p in source.Pointers)
                    {
                        if (p is IMixedRealityNearPointer)
                        {
                            if (p is PokePointer && p.Controller.ControllerHandedness == Microsoft.MixedReality.Toolkit.Utilities.Handedness.Right)
                            {
                                calibrationPlane.transform.position = p.Position;
                            }
                        }

                    }
                }
            }
        }
    }

    public void OnCalibrateConfirm()
    {
        foreach (var source in MixedRealityToolkit.InputSystem.DetectedInputSources)
        {
            // Ignore anything that is not a hand because we want articulated hands
            if (source.SourceType == Microsoft.MixedReality.Toolkit.Input.InputSourceType.Hand)
            {
                foreach (var p in source.Pointers)
                {
                    if (p is IMixedRealityNearPointer)
                    {
                        if (p is PokePointer && p.Controller.ControllerHandedness == Microsoft.MixedReality.Toolkit.Utilities.Handedness.Right)
                        {
                            Debug.Log("calibrated position: " + p.Position);
                            isCalibrated = true;
                            calibrationPosition = p.Position;
                            calibrationPlane.SetActive(false);
                        }
                    }
                    //if (p.Result != null)
                    //{
                    //    var startPoint = p.Position;
                    //    var endPoint = p.Result.Details.Point;
                    //    var hitObject = p.Result.Details.Object;
                    //    if (hitObject)
                    //    {
                    //        //var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    //        //sphere.transform.localScale = Vector3.one * 0.01f;
                    //        //sphere.transform.position = endPoint;

                    //    }
                    //}

                }
            }
        }
    }

    public void OnScanPress()
    {

    }
}
