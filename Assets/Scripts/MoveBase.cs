using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBase : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Collider2D[] collider2Ds;
    void Awake()
    {
        collider2Ds = GetComponentsInChildren<Collider2D>();
    }

    // Update is called once per frame
    

    void DestroySelf(){
        Destroy(gameObject);
    }

    void ActivateColliders(){
        foreach (Collider2D collider2D in collider2Ds){
            collider2D.enabled = true;
        }
    }

    void DeactivateColliders(){
        foreach (Collider2D collider2D in collider2Ds){
            collider2D.enabled = false;
        }
    }
}
