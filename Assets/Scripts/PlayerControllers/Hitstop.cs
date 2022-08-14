using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitstop : MonoBehaviour
{

    private Animator playerAnimator;
    private PlayerController playerController;
    public GameManager gameManager;

    void Start(){
        playerAnimator = GetComponent<Animator>();
        playerController = GetComponentInParent<PlayerController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    // Start is called before the first frame update
    void StopTime(){
        Time.timeScale = 0;
        StartCoroutine(RestartTime());
    }

    IEnumerator RestartTime() {
        for (int i = 0; i< 10 ;i++){
            yield return null;
        }
        Time.timeScale = 1;

    }
    void ReenableMovement(){
        playerController.isIdle = true;
        playerAnimator.SetTrigger("Idle");
        gameManager.ResetComboCounter(playerController.opponentTag);
        //Debug.Log("a");
    }

    void ReenableMovementCrouching(){
        playerController.isIdle = true;
        playerAnimator.SetTrigger("Crouching");
        gameManager.ResetComboCounter(playerController.opponentTag);
    }

    
        
    
}
