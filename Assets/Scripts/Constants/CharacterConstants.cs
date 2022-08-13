using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterConstants : MonoBehaviour{
    //max health
    public int maxHP = 1000;

    //movement variables
    public int moveSpeed;
    public int dashSpeed;
    public int airdashSpeed;
    public int jumpSpeed;
    public int backdashDuration;
    public int backdashSpeed;
    public int airDashDuration;
    public int framesUntilAirJump = 5;
     //Damage 
    public Dictionary<string,int> damageValues = new Dictionary<string, int>(){
        {"Neutral A", 10},
        {"Crouching A", 50},
        {"Jumping A", 50},
        {"Neutral B", 50},
        {"Crouching B", 50},
        {"Jumping B", 50},
        {"Down Forward B", 50},
        {"Neutral S", 50},
        {"Crouching S", 50},
        {"Forward S", 50},
        {"Throw",100},
        {"Super",250}
    };

 
    
    //A normals

    
    public abstract void NeutralA();
    public abstract void CrouchingA();
    public abstract void JumpingA();
    //B Normals

  
    public abstract void NeutralB();
    public abstract void CrouchingB();
    public abstract void JumpingB();
    public abstract void DownForwardB();
    

    //Specials

    public abstract void NeutralS();
    public abstract void CrouchingS();
    public abstract void ForwardS();

    public abstract void Throw();
    public abstract void Super();


    public GameObject neutralAPrefab;
    public GameObject crouchingAPrefab;
    public GameObject jumpingAPrefab;
    public GameObject neutralBPrefab;
    public GameObject crouchingBPrefab;
    public GameObject jumpingBPrefab;
    public GameObject downForwardBPrefab;
    public GameObject neutralSPrefab;
    public GameObject crouchingSPrefab;
    public GameObject forwardSPrefab;
    public GameObject throwPrefab;
    public GameObject superPrefab;

    
   
}