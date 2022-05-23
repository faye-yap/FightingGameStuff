using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int frameNumber;
    public int speed;
    public int dashSpeed;
    public int maxHSpeed;
    public int jumpSpeed;
    public int maxVSpeed;

    private Rigidbody2D p1Body;
    private BoxCollider2D p1Collider; 

    private List<int> frameNumberBuffer = new List<int> {};
    private List<int> inputBuffer = new List<int> {};
    public int inputFrameWindow = 5;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        p1Body = GetComponent<Rigidbody2D>();
        p1Collider = GetComponent<BoxCollider2D>();
        frameNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
