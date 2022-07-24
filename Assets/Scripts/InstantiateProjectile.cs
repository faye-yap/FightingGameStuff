using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateProjectile : MonoBehaviour
{

    public GameObject projectilePrefab;
    void InstantiateProj(){
        GameObject projectile = Instantiate(projectilePrefab,transform.position, Quaternion.identity);
        Rigidbody2D body = projectile.GetComponent<Rigidbody2D>();
        body.velocity = Vector2.right * 1.25f * GetComponentInParent<PlayerController>().transform.localScale * 5;
    }

}
    
