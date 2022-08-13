using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnConstants : CharacterConstants
{
    PlayerController controller;

    void Start(){
        controller = GetComponentInParent<PlayerController>();
    }

    public override void NeutralA()
    {
        
    }

    public override void CrouchingA()
    {
        
    }

    public override void JumpingA()
    {
        
    }

    public override void NeutralB()
    {
        
    }

    public override void CrouchingB()
    {
        
    }

    public override void JumpingB()
    {
        
    }

    public override void DownForwardB()
    {
      
    }

    public override void NeutralS()
    {
        
    }

    public override void CrouchingS()
    {
        
    }

    IEnumerator PawnTeleport(){
        Transform playerTransform = transform.GetComponentInParent<PlayerController>().transform;
        Debug.Log(gameObject.name);
        Transform pawnTransform = playerTransform.GetChild(1);
        Transform noteTransform = playerTransform.GetChild(3);
        Vector3 notePos = noteTransform.position;
       
        float facingDirection = playerTransform.localScale.x/Mathf.Abs(playerTransform.localScale.x);
        
        
        SpriteRenderer pawnSprite = pawnTransform.GetComponent<SpriteRenderer>();
        Debug.Log(pawnSprite);
        for (int frame = 0; frame < 45; frame++){
            if(frame >= 15 && frame < 22){
                Color c =  pawnSprite.color;
                c.a -= 0.125f;
                pawnSprite.color = c;
            } else if (frame >= 22 && frame < 26){
                playerTransform.Translate( new Vector3(facingDirection * 1.5f,0,0));
                noteTransform.SetPositionAndRotation(notePos,Quaternion.identity);
                if(playerTransform.localScale.x * facingDirection < 0){
                    noteTransform.localScale = new Vector3(-1,1,1);
                }
                    
                
            } else if (frame >= 26){
                Color c =  pawnSprite.color;
                c.a += 0.125f;
                pawnSprite.color = c;
            }
            yield return null;
        }
    }
    public override void ForwardS()
    {
        StartCoroutine(PawnTeleport());
    }
    public override void Throw()
    {
       
    }

    public override void Super()
    {
       GameObject super = Instantiate(superPrefab,transform.position,Quaternion.identity);
       super.transform.SetParent(controller.transform);
    }
}
