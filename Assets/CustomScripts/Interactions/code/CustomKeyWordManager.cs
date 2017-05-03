using System;
using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class CustomKeyWordManager : MonoBehaviour, ISpeechHandler {
    public void OnSpeechKeywordRecognized(SpeechKeywordRecognizedEventData eventData)
    {
        SetPrice(eventData.RecognizedText);
    }

    public void SetPrice(string recognizedText)
    {
        Debug.Log("dfa");
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
