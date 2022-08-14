using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI; // Required when Using UI elements.
using UnityEngine.EventSystems;

public class WinScreenController : MonoBehaviour
{
    public PlayerSelectConstants playerSelectConstants;
    public GameObject dialog;
    public GameObject p1Dialog;
    public GameObject p2Dialog;
    public GameObject endMenu;
    public Button rematchButton;
    private Dictionary<PlayerSelectConstants.CharacterSelection, string> charTextDict; 
    // Start is called before the first frame update
    void Start()
    {
        charTextDict = new Dictionary<PlayerSelectConstants.CharacterSelection, string>(){
            {PlayerSelectConstants.CharacterSelection.Pawn, playerSelectConstants.pawnWinText},
            {PlayerSelectConstants.CharacterSelection.Knight, playerSelectConstants.knightWinText},
            {PlayerSelectConstants.CharacterSelection.Bishop, playerSelectConstants.bishopWinText},
            {PlayerSelectConstants.CharacterSelection.Rook, playerSelectConstants.rookWinText},
        };
        p1Dialog.GetComponentInChildren<TextMeshProUGUI>().text = charTextDict[playerSelectConstants.p1Character];
        p2Dialog.GetComponentInChildren<TextMeshProUGUI>().text = charTextDict[playerSelectConstants.p2Character];
        if (playerSelectConstants.winner){
            p2Dialog.SetActive(true);
            p1Dialog.SetActive(false);
            p1Dialog.GetComponentInChildren<TextMeshProUGUI>().text = p1Dialog.GetComponentInChildren<TextMeshProUGUI>().text.Substring(0,1) + "...";
        } else {
            p1Dialog.SetActive(true);
            p2Dialog.SetActive(false);
            p2Dialog.GetComponentInChildren<TextMeshProUGUI>().text = p2Dialog.GetComponentInChildren<TextMeshProUGUI>().text.Substring(0,1) + "...";
        }
    }

    public void NextDialog(){
        if (playerSelectConstants.winner){
            if (p2Dialog.activeSelf){
                p2Dialog.SetActive(false);
                p1Dialog.SetActive(true);
            } else {
                dialog.SetActive(false);
                endMenu.SetActive(true);
                StartCoroutine(selectButton(rematchButton));
            }
        } else {
            if (p1Dialog.activeSelf){
                p1Dialog.SetActive(false);
                p2Dialog.SetActive(true);
            } else {
                dialog.SetActive(false);
                endMenu.SetActive(true);
                StartCoroutine(selectButton(rematchButton));
            }
        }
    }

    private IEnumerator selectButton(Button button){
        yield return null;
        EventSystem.current.SetSelectedGameObject(button.gameObject);
        Debug.Log(EventSystem.current.currentSelectedGameObject.name);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void CharacterSelect()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
    }

}
