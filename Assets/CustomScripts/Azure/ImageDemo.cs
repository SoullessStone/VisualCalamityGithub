using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Unity3dAzure.StorageServices;
using System.IO;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ImageDemo : MonoBehaviour
{

	[Header ("Azure Storage Service")]
	[SerializeField]
	public  string storageAccount;
	[SerializeField]
    public string accessKey;
	[SerializeField]
    public string container;

	private StorageServiceClient client;
	private BlobService blobService;

	

	private bool isCaptured = false;

	private string localPath;

	void Start ()
	{


		client = new StorageServiceClient (storageAccount, accessKey);
		blobService = client.GetBlobService ();

	}


	public void PutImage (byte[] imageBytes,string filename)
	{
		StartCoroutine (blobService.PutImageBlob (PutImageCompleted, imageBytes, container, filename, "image/png"));
	}

	private void PutImageCompleted (RestResponse response)
	{
		if (response.IsError) {
			Debug.Log( "Error putting blob audio:" + response.Content);
			return;
		}
        Debug.Log( "Put image blob:" + response.Content);
	}

	

/*	public IEnumerator LoadImageURL (string url)
	{
		UnityWebRequest www = UnityWebRequest.GetTexture (url);
		yield return www.Send ();
		if (www.isError) {
            Debug.Log("Failed to load image: " + url);
		} else {
			Texture texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
			ChangeImage (texture);
		}
        */
	}



	



	



