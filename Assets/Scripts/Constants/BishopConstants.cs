using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BishopConstants : CharacterConstants
{
    PlayerController controller;

    void Start(){
        controller = GetComponentInParent<PlayerController>();
    }

    public override void NeutralA()
    {
        Vector3 initVector = new Vector3(transform.parent.position.x + transform.parent.localScale.x * neutralAPrefab.transform.position.x,transform.position.y + neutralAPrefab.transform.position.y,transform.position.z + neutralAPrefab.transform.position.z);
        GameObject neutralA = Instantiate(neutralAPrefab,initVector,Quaternion.identity);
        neutralA.transform.SetParent(transform.parent);
        neutralA.transform.localScale = new Vector3(1,1,1);
    }

    public override void CrouchingA()
    {   
        Vector3 initVector = new Vector3(transform.parent.position.x + transform.parent.localScale.x * crouchingAPrefab.transform.position.x,transform.position.y + crouchingAPrefab.transform.position.y,transform.position.z + crouchingAPrefab.transform.position.z);
        GameObject crouchingA = Instantiate(crouchingAPrefab,initVector,Quaternion.identity);
        crouchingA.transform.SetParent(transform.parent);
        crouchingA.transform.localScale = new Vector3(1,1,1);
    }

    public override void JumpingA()
    {
        Vector3 initVector = new Vector3(transform.parent.position.x + transform.parent.localScale.x * jumpingAPrefab.transform.position.x,transform.position.y + jumpingAPrefab.transform.position.y,transform.position.z + jumpingAPrefab.transform.position.z);
        GameObject jumpingA = Instantiate(jumpingAPrefab,initVector,Quaternion.identity);
        jumpingA.transform.SetParent(transform.parent);
        jumpingA.transform.localScale = new Vector3(1,1,1);
    }

    public override void NeutralB()
    {
        Vector3 initVector = new Vector3(transform.parent.position.x + transform.parent.localScale.x * neutralBPrefab.transform.position.x,transform.position.y + neutralBPrefab.transform.position.y,transform.position.z + neutralBPrefab.transform.position.z);
        GameObject neutralB = Instantiate(neutralBPrefab,initVector,Quaternion.identity);
        neutralB.transform.SetParent(transform.parent);
        neutralB.transform.localScale = new Vector3(1,1,1);
    }

    public override void CrouchingB()
    {
        Vector3 initVector = new Vector3(transform.parent.position.x + transform.parent.localScale.x * crouchingBPrefab.transform.position.x,transform.position.y + crouchingBPrefab.transform.position.y,transform.position.z + crouchingBPrefab.transform.position.z);
        GameObject crouchingB = Instantiate(crouchingBPrefab,initVector,Quaternion.identity);
        crouchingB.transform.SetParent(transform.parent);
        crouchingB.transform.localScale = new Vector3(1,1,1);
    }

    public override void JumpingB()
    {
        Vector3 initVector = new Vector3(transform.parent.position.x + transform.parent.localScale.x * jumpingBPrefab.transform.position.x,transform.position.y + jumpingBPrefab.transform.position.y,transform.position.z + jumpingBPrefab.transform.position.z);
        GameObject jumpingB = Instantiate(jumpingBPrefab,initVector,Quaternion.identity);
        jumpingB.transform.SetParent(transform.parent);
        jumpingB.transform.localScale = new Vector3(1,1,1);
    }

    public override void DownForwardB()
    {
        Vector3 initVector = new Vector3(transform.parent.position.x + transform.parent.localScale.x * downForwardBPrefab.transform.position.x,transform.position.y + downForwardBPrefab.transform.position.y,transform.position.z + downForwardBPrefab.transform.position.z);

        GameObject downForwardB = Instantiate(downForwardBPrefab,initVector,Quaternion.identity);
        downForwardB.transform.SetParent(transform.parent);
        downForwardB.transform.localScale = new Vector3(1,1,1);
    }

    public override void NeutralS()
    {
        Vector3 initVector = new Vector3(transform.parent.position.x + transform.parent.localScale.x * neutralSPrefab.transform.position.x,transform.position.y + neutralSPrefab.transform.position.y,transform.position.z + neutralSPrefab.transform.position.z);
        GameObject neutralS = Instantiate(neutralSPrefab,initVector,Quaternion.identity);
        neutralS.transform.SetParent(transform.parent);
        neutralS.transform.localScale = new Vector3(1,1,1);
    }

    public override void CrouchingS()
    {
        
        Vector3 initVector = new Vector3(transform.parent.position.x + transform.parent.localScale.x * crouchingSPrefab.transform.position.x,transform.position.y + crouchingSPrefab.transform.position.y,transform.position.z + crouchingSPrefab.transform.position.z);
        GameObject crouchingS = Instantiate(crouchingSPrefab,initVector,Quaternion.identity);
        crouchingS.transform.SetParent(transform.parent);
        crouchingS.transform.localScale = new Vector3(1,1,1);
    }

    
    public override void ForwardS()
    {   
        Vector3 initVector = new Vector3(transform.position.x + transform.parent.localScale.x * forwardSPrefab.transform.position.x,transform.position.y + forwardSPrefab.transform.position.y,transform.position.z + forwardSPrefab.transform.position.z);
        GameObject forwardS = Instantiate(forwardSPrefab,initVector,Quaternion.identity);
        forwardS.transform.SetParent(transform.parent);
        forwardS.transform.localScale = new Vector3(transform.localScale.x/Mathf.Abs(transform.localScale.x),1,1);
        
    }
    public override void Throw()
    {
        Vector3 initVector = new Vector3(transform.parent.position.x + transform.parent.localScale.x * throwPrefab.transform.position.x,transform.position.y + throwPrefab.transform.position.y,transform.position.z + throwPrefab.transform.position.z);
        GameObject throwButton = Instantiate(throwPrefab,initVector,Quaternion.identity);
        throwButton.transform.SetParent(transform.parent);
        throwButton.transform.localScale = new Vector3(1,1,1);
    }

    public override void Super()
    {
        Vector3 initVector = new Vector3(transform.parent.position.x +  superPrefab.transform.position.x,transform.position.y + superPrefab.transform.position.y,transform.position.z + superPrefab.transform.position.z);
        GameObject super = Instantiate(superPrefab,initVector,Quaternion.identity);
        super.transform.SetParent(transform.parent);
        super.transform.localScale = new Vector3(1,1,1);
    }
}
