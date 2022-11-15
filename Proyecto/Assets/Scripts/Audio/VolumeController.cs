using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private AudioMixer am;

    public void SetMainVolume(float value){
        am.SetFloat("Master", Mathf.Log10(value) * 20);
    }
    public void SetEffectsVolume(float value){
        am.SetFloat("Effects", Mathf.Log10(value) * 20);
    }
    public void SetMusicVolume(float value){
        am.SetFloat("Music", Mathf.Log10(value) * 20);
    }
}
