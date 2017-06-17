using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.VR.WSA.WebCam;
using System;

public class CameraManager : MonoBehaviour
{
    PhotoCapture photoCaptureObject = null;
    Texture2D targetTexture = null;
    GameObject FocusedValue;
    public GameObject lastCreated;
    public ImageDemo azure;
    public GameObject image;
        

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

        byte[] pic= targetTexture.EncodeToJPG();

        if (FocusedValue == null)
        {
            if (lastCreated != null)
                FocusedValue = lastCreated;
            else
                return ;
            
        }

        azure.PutImage(pic,FocusedValue.name +".jpg");
        PlaceImage();



    }

    void OnStoppedPhotoMode(PhotoCapture.PhotoCaptureResult result)
    {
        // Shutdown the photo capture resource
        photoCaptureObject.Dispose();
        photoCaptureObject = null;
    }

    public void setFocus(GameObject obj)
    {
        FocusedValue = obj;
        StartCoroutine(GetTexture(FocusedValue));
        
    }

    private IEnumerator GetTexture(GameObject obj)
    {
        var www = new WWW("https://cmblobs.blob.core.windows.net/image/" + obj.name + ".jpg");
        // Wait for download to complete
        yield return www;

        if (www.texture != null)
        {
            // assign texture
            targetTexture = www.texture;
            PlaceImage();
        }
     
    }

    private void PlaceImage()
    {
        FocusedValue.SetActive(false);
        // Create a GameObject to which the texture can be applied
        //GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        image.transform.position = new Vector3(FocusedValue.transform.position.x, FocusedValue.transform.position.y, FocusedValue.transform.position.z);
        image.transform.rotation = FocusedValue.transform.rotation;

        image.SetActive(true);
        image.GetComponent<Renderer>().material.SetTexture("_MainTex", targetTexture);
    }
}