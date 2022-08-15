using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; // Required when Using UI elements.
using TMPro;

public class FirstToController : MonoBehaviour, ISelectHandler
{
    public List<Button> firstToButtons;

    public void OnSelect(BaseEventData eventData)
    {
        foreach (var button in firstToButtons){
            if (PlayerPrefs.GetInt("FirstTo") == int.Parse(button.GetComponentInChildren<TextMeshProUGUI>().text)){
                StartCoroutine(selectFirstTo(button));
                return;
            }
        }
        selectFirstTo(firstToButtons[1]);
    }

    private IEnumerator selectFirstTo(Button button){
        yield return null;
        EventSystem.current.SetSelectedGameObject(button.gameObject);
    }
}
