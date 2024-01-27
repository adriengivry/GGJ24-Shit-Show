using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    [SerializeField]
    private AudioMixer mixer;

    [SerializeField]
    private Slider slider;

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("MasterVol", 0.5f);
        SetLevel(PlayerPrefs.GetFloat("MasterVol", 0.5f));
    }

    public void SetLevel(float lvl) 
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(lvl) * 20);

        PlayerPrefs.SetFloat("MasterVol", lvl);
    }
}
