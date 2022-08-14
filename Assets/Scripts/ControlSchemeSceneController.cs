using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Required when Using UI elements.
using UnityEngine.EventSystems;

public class ControlSchemeSceneController : MonoBehaviour, ICancelHandler
{
    public void MainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 4);
    }

    public void OnCancel(BaseEventData eventData)
    {
        MainMenu();
    }
}
