using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateProjectile : MonoBehaviour
{

    public GameObject projectilePrefab;
    public float initialVelocityMultiplier = 1.25f;
    void InstantiateProj(){
        GameObject projectile = Instantiate(projectilePrefab,transform.position, Quaternion.identity);
        projectile.transform.SetParent(transform);
        projectile.transform.localScale = new Vector3(1,1,1);
        Rigidbody2D body = projectile.GetComponent<Rigidbody2D>();
        body.velocity = Vector2.right * initialVelocityMultiplier * GetComponentInParent<PlayerController>().transform.localScale * 5;
    }

}
    
