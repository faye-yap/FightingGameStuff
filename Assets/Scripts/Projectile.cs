using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update

    

    private void Start(){
        transform.GetChild(0).tag = transform.tag;
;    }
    private void OnBecameInvisible() {
        Destroy(gameObject);
    }

    private void DestroySelf() {
        Destroy(this.gameObject);
    }
}
