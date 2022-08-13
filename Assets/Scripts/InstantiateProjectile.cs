using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateProjectile : MonoBehaviour
{

    public GameObject projectilePrefab;
    public float initialVelocityMultiplier = 1.25f;
    void InstantiateProj(){
        Debug.Log(transform.parent.name);
        Vector3 initVector = new Vector3(transform.parent.position.x + transform.parent.localScale.x * projectilePrefab.transform.position.x,transform.parent.position.y + projectilePrefab.transform.position.y,transform.parent.position.z + projectilePrefab.transform.position.z);
        GameObject projectile = Instantiate(projectilePrefab,initVector, Quaternion.identity);
        
        projectile.tag = transform.tag;
        Rigidbody2D body = projectile.GetComponent<Rigidbody2D>();
        
        body.velocity = Vector2.right * initialVelocityMultiplier * GetComponentInParent<PlayerController>().transform.localScale.x * 5.0f;
        
    }

}
    
