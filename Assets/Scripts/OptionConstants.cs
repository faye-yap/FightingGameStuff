using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName =  "OptionConstants", menuName =  "ScriptableObjects/OptionConstants", order =  2)]
public class OptionConstants : ScriptableObject
{
    public float GlobalVolume = 1.0f;
    public float MusicVolume = 1.0f;
    [HideInInspector]
    public List<int> TimeLimits = new List<int>{60, 99, 120};
    public int TimeLimit = 99;
    [HideInInspector]
    public List<int> FirstTos = new List<int>{1, 2, 3};
    public int FirstTo = 2;

    public void OnGlobalVolumeChanged(float newVolume)
    {
        GlobalVolume = newVolume;
    }

    public void OnMusicVolumeChanged(float newVolume)
    {
        MusicVolume = newVolume;
    }

    public void OnTimeLimitChanged(int newTimeLimit)
    {
        TimeLimit = newTimeLimit;
    }
    public void OnFirstToChanged(int newFirstTo)
    {
        FirstTo = newFirstTo;
    }
}
