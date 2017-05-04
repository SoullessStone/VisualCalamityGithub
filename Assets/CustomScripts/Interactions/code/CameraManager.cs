using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.VR.WSA.WebCam;
using System;

public class CameraManager : MonoBehaviour
{
    PhotoCapture photoCaptureObject = null;
    Texture2D targetTexture = null;
    GameObject lastCreatedPrice;

    // Use this for initialization
    void Start()
    {
        Resolution cameraResolution = PhotoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();
        targetTexture = new Texture2D(cameraResolution.width, cameraResolution.height);

        // Create a PhotoCapture object
        PhotoCapture.CreateAsync(false, delegate (PhotoCapture captureObject) {
            photoCaptureObject = captureObject;
            CameraParameters cameraParameters = new CameraParameters();
            cameraParameters.hologramOpacity = 0.0f;
            cameraParameters.cameraResolutionWidth = cameraResolution.width;
            cameraParameters.cameraResolutionHeight = cameraResolution.height;
            cameraParameters.pixelFormat = CapturePixelFormat.BGRA32;

            // Activate the camera
            photoCaptureObject.StartPhotoModeAsync(cameraParameters, delegate (PhotoCapture.PhotoCaptureResult result) {
                // Take a picture
            });
        });
    }

    public void takePhoto()
    {
        photoCaptureObject.TakePhotoAsync(OnCapturedPhotoToMemory);
    }

    void OnCapturedPhotoToMemory(PhotoCapture.PhotoCaptureResult result, PhotoCaptureFrame photoCaptureFrame)
    {
        // Copy the raw image data into the target texture
        photoCaptureFrame.UploadImageDataToTexture(targetTexture);

        targetTexture.EncodeToJPG();

        // Create a GameObject to which the texture can be applied
        //GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);

        if (lastCreatedPrice == null)
        {
            return;
        }
        Renderer quadRenderer = lastCreatedPrice.GetComponent<Renderer>() as Renderer;
        //quadRenderer.material = new Material(Shader.Find("Standard"));
  
        quadRenderer.material = new Material(Shader.Find("Standard"));
        //It is due to the the same name of the Material...Find and rename the material of the new object then 
        //remove the mesh renderer component and add it back.. and Now add the material to mesh renderer.. 

        //quad.transform.parent = this.transform;
        //quad.transform.localPosition = new Vector3(0.0f, 0.0f, 3.0f);

        quadRenderer.material.SetTexture("_MainTex", targetTexture);
        
        // Deactivate the camera
        //photoCaptureObject.StopPhotoModeAsync(OnStoppedPhotoMode);
    }

    void OnStoppedPhotoMode(PhotoCapture.PhotoCaptureResult result)
    {
        // Shutdown the photo capture resource
        photoCaptureObject.Dispose();
        photoCaptureObject = null;
    }

    public void setLastCreated(GameObject obj)
    {
        if(lastCreatedPrice!=null)
            lastCreatedPrice.GetComponent<Renderer>().material = new Material(Shader.Find("VertexLit"));
       
        lastCreatedPrice = obj;
    }
}