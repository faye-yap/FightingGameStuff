using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.

public class OptionsController : MonoBehaviour
{
    public OptionConstants optionConstants;
    public Slider globalVolumeSlider;
    public Slider musicVolumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        globalVolumeSlider.value = optionConstants.GlobalVolume;
        musicVolumeSlider.value = optionConstants.MusicVolume;
    }
}
