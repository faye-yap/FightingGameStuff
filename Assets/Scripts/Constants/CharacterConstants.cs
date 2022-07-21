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


    //A normals
    public int neutralAStartup = 10;
    public int neutralAActive = 10;
    public int neutralARecovery = 10;
    public int neutralADamage = 1000000;
    public int crouchingAStartup = 10;
    public int crouchingAActive = 10;
    public int crouchingARecovery = 10;  
    public int crouchingADamage = 1000000;  
    public int jumpingAStartup = 10;
    public int jumpingAActive = 10;
    public int jumpingARecovery = 10;
    public int jumpingADamage = 1000000;
    public abstract void NeutralA();
    public abstract void CrouchingA();
    public abstract void JumpingA();

    //B Normals
    public int neutralBStartup = 10;
    public int neutralBActive = 10;
    public int neutralBRecovery = 10;
    public int neutralBDamage = 1000000;
    public int crouchingBStartup = 10;
    public int crouchingBActive = 10;
    public int crouchingBRecovery = 10;   
    public int crouchingBDamage = 1000000; 
    public int jumpingBStartup = 10;
    public int jumpingBActive = 10;
    public int jumpingBRecovery = 10;
    public int jumpingBDamage = 1000000;
    public int downForwardBStartup = 10;
    public int downForwardBActive = 10;
    public int downForwardBRecovery = 10;
    public int downForwardBDamage = 1000000;
    //public abstract void NeutralB();
    //public abstract void CrouchingB();
    //public abstract void JumpingB();
    //public abstract void DownForwardB();
    

    //Specials
    public int neutralSStartup = 10;
    public int neutralSActive = 10;
    public int neutralSRecovery = 10;
    public int neutralSDamage = 1000000;
    public int crouchingSStartup = 10;
    public int crouchingSActive = 10;
    public int crouchingSRecovery = 10;  
    public int crouchingSDamage = 1000000;  
    public int forwardSStartup = 10;
    public int forwardSActive = 10;
    public int forwardSRecovery = 10;
    public int forwardSDamage = 1000000;
    //public abstract void NeutralS();
    //public abstract void CrouchingS();
    //public abstract void ForwardS();
}