using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightConstants : CharacterConstants
{
    public GameObject neutralAPrefab;

    void Awake(){
        neutralAStartup = 10;
        neutralAActive = 10;
        neutralARecovery = 10;
        crouchingAStartup = 10;
        crouchingAActive = 10;
        crouchingARecovery = 10;    
        jumpingAStartup = 10;
        jumpingAActive = 10;
        jumpingARecovery = 10;
        neutralBStartup = 10;
        neutralBActive = 10;
        neutralBRecovery = 10;
        crouchingBStartup = 10;
        crouchingBActive = 10;
        crouchingBRecovery = 10;    
        jumpingBStartup = 10;
        jumpingBActive = 10;
        jumpingBRecovery = 10;
        downForwardBStartup = 10;
        downForwardBActive = 10;
        downForwardBRecovery = 10;
        neutralSStartup = 10;
        neutralSActive = 10;
        neutralSRecovery = 10;
        crouchingSStartup = 10;
        crouchingSActive = 10;
        crouchingSRecovery = 10;    
        forwardSStartup = 10;
        forwardSActive = 10;
        forwardSRecovery = 10;
    }

    public override void NeutralA()
    {
        GameObject neutralA = Instantiate(neutralAPrefab,this.transform.position,Quaternion.identity);
        neutralA.transform.parent = this.transform;
        neutralA.transform.localScale = new Vector3(1,1,1);
    }

    public override void CrouchingA()
    {
        
    }

    public override void JumpingA()
    {
        
    }
}
