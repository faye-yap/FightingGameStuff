using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBase : MonoBehaviour
{
    // Start is called before the first frame update
    
    public List<Collider2D> collider2Ds;
    public int collidersInParent;
    private PlayerController playerController;
    private PlayerController opponentController;
    private Animator moveAnimator;
    public AudioSource audioOnHit;
    public AudioSource audioOnBlock;
    public AudioSource moveAudio;
    
    void Awake()
    {
        
        GetComponentsInChildren<Collider2D>(collider2Ds);
        collidersInParent = GetComponents<Collider2D>().Length;
        collider2Ds.RemoveRange(0,collidersInParent);
        
        
    }
    void Start(){
        this.gameObject.tag = this.transform.parent.name;
        if (transform.childCount > 0) this.transform.GetChild(0).tag = this.gameObject.tag;
        playerController = GetComponentInParent<PlayerController>();
        //Debug.Log(playerController);
        moveAnimator = GetComponentInParent<Animator>();
        string opponentTag = this.transform.parent.GetComponent<PlayerController>().opponentTag;
        opponentController = GameObject.FindGameObjectWithTag(opponentTag).GetComponent<PlayerController>();
        opponentController.hasBeenHit = false;
        moveAudio = transform.GetChild(transform.childCount - 1).GetComponent<AudioSource>();   
        audioOnHit = transform.GetChild(transform.childCount - 1).GetChild(0).GetComponent<AudioSource>();
        audioOnBlock = transform.GetChild(transform.childCount - 1).GetChild(1).GetComponent<AudioSource>();
    }

    // Update is called once per frame
    
    public void PlayMoveAudio(){
        if(moveAudio.clip!=null) {
            moveAudio.enabled = true;
            moveAudio.Play();

        }
    }

    public void PlayAudioOnHit(){
        if(audioOnHit.clip!=null) {
            audioOnHit.enabled = true;
            audioOnHit.Play();

        }
    }

    public void PlayAudioOnBlock(){
       if(audioOnBlock.clip!=null) {
            audioOnBlock.enabled = true;
            audioOnBlock.Play();

        }
    }

    void DestroySelf(){
        
        playerController.isIdle = true;
        playerController.canDashJumpCancel = true;
        playerController.playerAnimator.SetTrigger("Idle");
        Destroy(this.gameObject);
    }

    void ActivateColliders(){
        //Debug.Log("Colliders Activated");
        foreach (Collider2D collider2D in collider2Ds){
            if (collider2D.gameObject.layer != 9){ // Hurtbox layer
                collider2D.enabled = true;
            }
        }
    }

    void DeactivateColliders(){
        //Debug.Log("Colliders Deactivated");
        foreach (Collider2D collider2D in collider2Ds){
            if (collider2D.gameObject.layer != 9){ // Hurtbox layer
                collider2D.enabled = false;
            }
        }
        opponentController.hasBeenHit = false;
    }

 

    public void ThrowHit(){
        playerController.playerAnimator.Play("ThrowHit");
        moveAnimator.Play("ThrowHit");
        opponentController.isIdle = false;
    }
    
    
    private void ThrowDamage(){
        Debug.Log(opponentController);
        opponentController.TakeDamageFromThrow();
      
    }
}
