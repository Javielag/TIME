using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour {
    public AudioMixer mixer;
    float volume;
    Slider sl;
    public void Start()
    {
        GameManager.instance.SetVC(this.gameObject);
        sl = GetComponent<Slider>();
        if (PlayerPrefs.HasKey("Volume"))
        {
            volume = PlayerPrefs.GetFloat("Volume");
            sl.value = volume;
        }
    }
    public void SetLevel(float sliderValue)
    {
        volume = sliderValue;
        mixer.SetFloat("Volume", Mathf.Log10(sliderValue)*20);
    }
    public void SaveVolume()
    {
        PlayerPrefs.SetFloat("Volume", volume);
    }
}
