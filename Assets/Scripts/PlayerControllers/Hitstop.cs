using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitstop : MonoBehaviour
{

    private Animator playerAnimator;
    private PlayerController playerController;

    void Start(){
        playerAnimator = GetComponent<Animator>();
        playerController = GetComponentInParent<PlayerController>();
    }
    // Start is called before the first frame update
    void StopTime(){
        Time.timeScale = 0;
    }

    void StartTime(){
        Time.timeScale = 1;
        playerAnimator.updateMode = AnimatorUpdateMode.Normal;
    }

    void ReenableMovement(){
        playerController.isIdle = true;
    }
        
    
}
