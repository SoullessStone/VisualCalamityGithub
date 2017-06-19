using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.VR.WSA.WebCam;

public class CameraManager : MonoBehaviour
{
    public GameObject lastCreated;
    public ImageDemo azure;
    public GameObject image;

    private PhotoCapture photoCaptureObject = null;
    private Texture2D targetTexture = null;
    private GameObject focusedValue;
    
    private int cameraResolutionWidth;
    private int cameraResolutionHeight;

    // Use this for initialization
    void Start()
    {  
        Resolution cameraResolution = PhotoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();
        cameraResolutionWidth = cameraResolution.width;
        cameraResolutionHeight = cameraResolution.height;
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

    public GameObject getFocusedObject()
    {
        return focusedValue;
    }

    public void takePhoto()
    {
        photoCaptureObject.TakePhotoAsync(OnCapturedPhotoToMemory);
        setFocus(lastCreated, targetTexture);
    }

    void OnCapturedPhotoToMemory(PhotoCapture.PhotoCaptureResult result, PhotoCaptureFrame photoCaptureFrame)
    {
         // Copy the raw image data into the target texture
        photoCaptureFrame.UploadImageDataToTexture(targetTexture);

        byte[] pic= targetTexture.EncodeToJPG();

        azure.PutImage(pic, lastCreated.name +".jpg");
    }

    void OnStoppedPhotoMode(PhotoCapture.PhotoCaptureResult result)
    {
        // Shutdown the photo capture resource
        photoCaptureObject.Dispose();
        photoCaptureObject = null;
    }

    public void setFocus(GameObject obj, Texture2D tex)
    {
        image.SetActive(false);

        //restore the previous object the camera was focused on
        if(focusedValue != null)
        {
            focusedValue.SetActive(true);
        }

        //set the new focuse and load its image there
        focusedValue = obj;
        if (tex == null)
        {
            loadTextureFor(focusedValue);
        } else
        {
            setTextureOnImage(tex);
        }

        // turn of the current object the camera is focused on since 
        // there is the image active for it
        focusedValue.SetActive(false);
    }

    public void loadTextureFor(GameObject obj)
    {
        StartCoroutine(GetTexture(obj));
        //GetTexture(obj);
    }

    private IEnumerator GetTexture(GameObject obj)
    {
        WWW www = new WWW("https://cmblobs.blob.core.windows.net/image/" + obj.name + ".jpg");
        // Wait for download to complete
        yield return www;

        setTextureOnImage(www.texture);           
    }

    private void setTextureOnImage(Texture2D tex)
    {
        image.GetComponent<Renderer>().material.mainTexture = tex;
        
        PlaceImage();  
    }

    private void PlaceImage()
    {
        // Create a GameObject to which the texture can be applied
        //GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        image.transform.position = new Vector3(focusedValue.transform.position.x, focusedValue.transform.position.y, focusedValue.transform.position.z);
        image.transform.rotation = focusedValue.transform.rotation;
        image.SetActive(true);
    }
}