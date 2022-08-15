using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; // Required when Using UI elements.
using TMPro;

public class TimeLimitController : MonoBehaviour, ISelectHandler
{
    public List<Button> timeLimitButtons;

    public void OnSelect(BaseEventData eventData)
    {
        foreach (var button in timeLimitButtons){
            if (PlayerPrefs.GetInt("TimeLimit") == int.Parse(button.GetComponentInChildren<TextMeshProUGUI>().text)){
                StartCoroutine(selectTimeLimit(button));
                return;
            }
        }
        StartCoroutine(selectTimeLimit(timeLimitButtons[1]));
    }

    private IEnumerator selectTimeLimit(Button button){
        yield return null;
        EventSystem.current.SetSelectedGameObject(button.gameObject);
    }
}
