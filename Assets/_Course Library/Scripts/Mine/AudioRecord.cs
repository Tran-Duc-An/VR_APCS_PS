using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRecord : MonoBehaviour
{
    public AudioSource audioSource;
    private string microphoneName;
    private AudioClip recordedClip;
    private bool isRecording;
    private float recordStartTime;
    private float recordDuration = 10f; // Record for 10 seconds

    void Start()
    {
        // Get available microphones
        if (Microphone.devices.Length > 0)
        {
            microphoneName = Microphone.devices[0]; // Select the first available mic
        }
        else
        {
            Debug.LogError("No microphone detected!");
        }
        StartRecording();
    }

    void Update()
    {
        Debug.LogError(Time.time - recordStartTime);
        if (isRecording && (Time.time - recordStartTime >= recordDuration))
        {
            StopRecording();
            isRecording = false;
            PlayRecording();
        }
    }

    public void StartRecording()
    {
        if (microphoneName != null)
        {
            recordedClip = Microphone.Start(microphoneName, false, 10, 44100);
            recordStartTime = Time.time;
            isRecording = true;
            Debug.Log("Recording Started...");
        }
    }

    public void StopRecording()
    {
        if (Microphone.IsRecording(microphoneName))
        {
            Microphone.End(microphoneName);
            audioSource.clip = recordedClip;
            Debug.Log("Recording Stopped...");
        }
    }

    public void PlayRecording()
    {
        if (audioSource.clip != null)
        {
            audioSource.Play();
            Debug.Log("Playing Recording...");
        }
    }
}
