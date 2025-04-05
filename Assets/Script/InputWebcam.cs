using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputWebcam : MonoBehaviour
{

    public RenderTexture renderTexture;
    public string[] availableWebcams;
    public int currentWebcamIndex = 0;

    private WebCamTexture webcamTexture;

    void Start()
    {

        SetWebcam(currentWebcamIndex);
    }
    void Update()
    {
        if (webcamTexture.didUpdateThisFrame && renderTexture != null)
        {
            Graphics.Blit(webcamTexture, renderTexture);
        }
    }

    public void SetWebcam(int index)
    {
      
        if (webcamTexture != null)
        {
            webcamTexture.Stop();
            Destroy(webcamTexture);
            webcamTexture = null;
        }

        
        currentWebcamIndex = index;
        StartWebcam();
    }

    private void StartWebcam()
    {
      
        if (currentWebcamIndex >= 0 && currentWebcamIndex < WebCamTexture.devices.Length)
        {
            
            webcamTexture = new WebCamTexture(WebCamTexture.devices[currentWebcamIndex].name);

            
            webcamTexture.Play();
        }
        else
        {
            Debug.LogError("유효하지 않은 웹캠 인덱스입니다.");
        }
    }

    private void OnValidate()
    {
        InitializeWebcamList();
    }

    private void InitializeWebcamList()
    {
       
        availableWebcams = new string[WebCamTexture.devices.Length];
        for (int i = 0; i < WebCamTexture.devices.Length; i++)
        {
            availableWebcams[i] = WebCamTexture.devices[i].name;
        }
    }


}
