using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class AudioScript : MonoBehaviour
{

    public AudioMixer masterMixer;
    public void SetSound(float soundLevel)
    {
        masterMixer.SetFloat("MusicVolume", soundLevel);
    }
}