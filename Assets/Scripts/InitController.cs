using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitController : MonoBehaviour
{
    public static bool isInit = false;
    // Start is called before the first frame update
    void Start()
    {
        if (!isInit)
        {
            isInit = true;
            PlayerPrefs.SetFloat("GlobalVolume", PlayerPrefs.GetFloat("GlobalVolume", 1.0f));
            Debug.Log("Set Global Volume to " + PlayerPrefs.GetFloat("GlobalVolume", 1.0f));
            AudioListener.volume = PlayerPrefs.GetFloat("GlobalVolume", 1.0f);

            PlayerPrefs.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume", 1.0f));
            Debug.Log("Set Music Volume to " + PlayerPrefs.GetFloat("MusicVolume", 1.0f));

            PlayerPrefs.SetInt("TimeLimit", PlayerPrefs.GetInt("TimeLimit", 99));
            Debug.Log("Set TimeLimit to " + PlayerPrefs.GetInt("TimeLimit", 99));

            PlayerPrefs.SetInt("FirstTo", PlayerPrefs.GetInt("FirstTo", 2));
            Debug.Log("Set FirstTo to " + PlayerPrefs.GetInt("FirstTo", 2));
        }
    }
}
