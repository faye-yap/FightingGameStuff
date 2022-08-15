using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.

public class OptionsController : MonoBehaviour
{
    public Slider globalVolumeSlider;
    public Slider musicVolumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        globalVolumeSlider.value = PlayerPrefs.GetFloat("GlobalVolume", 1.0f);
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
    }
}
