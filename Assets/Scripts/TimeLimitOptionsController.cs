using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.
using TMPro;
using UnityEngine.EventSystems;// Required when using Event data.

public class TimeLimitOptionsController : MonoBehaviour, ISelectHandler
{
    public OptionConstants optionConstants;
    private string text;
    void Start()
    {
        text = gameObject.GetComponentInChildren<TextMeshProUGUI>().text;
    }

    public void OnSelect(BaseEventData eventData)
    {
        optionConstants.OnTimeLimitChanged(int.Parse(text));
    }
}
