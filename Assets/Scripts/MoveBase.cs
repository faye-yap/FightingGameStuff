using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBase : MonoBehaviour
{
    // Start is called before the first frame update
    
    public List<Collider2D> collider2Ds;
    public int collidersInParent;
    private PlayerController playerController;
    
    void Awake()
    {
        
        GetComponentsInChildren<Collider2D>(collider2Ds);
        collidersInParent = GetComponents<Collider2D>().Length;
        collider2Ds.RemoveRange(0,collidersInParent);
        
    }
    void Start(){
        this.gameObject.tag = this.transform.parent.name;
        this.transform.GetChild(0).tag = this.gameObject.tag;
        playerController = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    

    void DestroySelf(){
        //Debug.Log("Destroy Self");
        playerController.isIdle = true;
        playerController.canDashJumpCancel = true;
        playerController.playerAnimator.SetTrigger("Idle");
        Destroy(this.gameObject);
    }

    void ActivateColliders(){
        //Debug.Log("Colliders Activated");
        foreach (Collider2D collider2D in collider2Ds){
            collider2D.enabled = true;
        }
    }

    void DeactivateColliders(){
        //Debug.Log("Colliders Deactivated");
        foreach (Collider2D collider2D in collider2Ds){
            collider2D.enabled = false;
        }
    }

    private void OnDestroy() {
        string opponentTag = this.transform.parent.GetComponent<PlayerController>().opponentTag;
        GameObject.FindGameObjectWithTag(opponentTag).GetComponent<PlayerController>().hasBeenHit = false;
    }

    
}
