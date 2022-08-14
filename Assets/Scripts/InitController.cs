using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitController : MonoBehaviour
{
    public static bool isInit = false;
    public OptionConstants optionConstants;
    // Start is called before the first frame update
    void Start()
    {
        if (!isInit)
        {
            isInit = true;
            Debug.Log("Set Global Volume to " + optionConstants.GlobalVolume);
            AudioListener.volume = optionConstants.GlobalVolume;
        }
    }
}
