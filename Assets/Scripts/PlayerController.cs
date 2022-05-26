using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    private const int MaxInt = 2147483647;
    private int frameNumber;
    public int speed;
    public int dashSpeed;
    public int airDashSpeed;
    public int jumpSpeed;

    private Rigidbody2D p2Body;
    private Rigidbody2D p1Body;
    private SpriteRenderer p1Sprite;
    private BoxCollider2D p1Collider; 
    private Vector2 movement;
    private bool onGroundState = true;
    private bool airDashBool;
    private bool groundDashBool;
    private bool groundBackdashBool;
    public int backdashDuration;
    public int backdashSpeed;
    private int gravityOn = 0;
    private float gravityScale;
    public int airDashDuration;
    private Vector3 xMidpoint;
    private List<int> frameNumberBuffer = new List<int> {};
    private List<int> inputBuffer = new List<int> {};
    public int inputFrameWindow = 5;
    private int airActions = 1;
    public bool canAirJump;
    public int framesUntilAirJump;
    private int setAirJump = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        p1Body = GetComponent<Rigidbody2D>();
        p1Sprite = GetComponent<SpriteRenderer>();
        p2Body = GameObject.FindGameObjectWithTag("Player2").GetComponent<Rigidbody2D>();
        p1Collider = GetComponent<BoxCollider2D>();
        frameNumber = 0;
        gravityScale = p1Body.gravityScale;
        
        
    }

    

    // Update is called once per frame
    void Update()
    {

        
        
        xMidpoint = new Vector3((p1Body.transform.position.x + p2Body.transform.position.x)/2,0,0);
        if (xMidpoint.x < p1Body.transform.position.x && !p1Sprite.flipX){
            p1Sprite.flipX = true;
            //Debug.Log("facing left");
        } else if (xMidpoint.x > p1Body.transform.position.x && p1Sprite.flipX){
            p1Sprite.flipX = false;
            //Debug.Log("facing right");
        }

        if (frameNumber >= gravityOn){
            p1Body.gravityScale = gravityScale;
        }

        if (frameNumber >= setAirJump){
            //Debug.Log(setAirJump);
            canAirJump = true;
            setAirJump = MaxInt;
        }

    

        switch (movement.y){
            case (1):   
                if (canAirJump && !onGroundState) {
                    AirJump();
                } else if (onGroundState){
                    setAirJump = frameNumber + framesUntilAirJump;
                    Debug.Log(setAirJump);
                    switch (movement.x){
                        case (1) : RightJump(); break;                    
                        case (0) : NeutralJump(); break;
                        case (-1): LeftJump(); break;                    
                    }
                    
                    
                }
                
            break;

            case (0): 
                switch (movement.x){
                    case (1) : RightWalk(); break;                    
                    case (0) : Stop(); break;
                    case (-1): LeftWalk(); break;                    
                }
            break;

            case (-1): 
                switch (movement.x){
                    case (1) : RightCrouch(); break;                    
                    case (0) : NeutralCrouch(); break;
                    case (-1): LeftCrouch(); break;                    
                }
            break;


            
        }

        if (airDashBool){
            p1Body.velocity = Vector2.zero;
            if (p1Sprite.flipX){
                //facing left
                if (movement.x == 1){
                    //backdash
                    AirBackdash(1.0f);
                } else {
                    Airdash(-1.0f);
                }
            } else {
                //facing right
                if (movement.x == -1){
                    //backdash
                    AirBackdash(-1.0f);
                } else {
                    Airdash(1.0f);
                }
            }


        }

        if (groundBackdashBool){
            Debug.Log("backdash");
            GroundBackdash();
        }


        





        frameNumber+=1;

    }


    void OnMove(InputValue value) {
        movement = value.Get<Vector2>();
    }

    void OnDash(){
        if (!onGroundState && airActions >= 1){
            airDashBool = true;
        } else {
            if (movement.x == -1 && !p1Sprite.flipX || movement.x == 1 && p1Sprite.flipX){
                Debug.Log("dash");
                groundBackdashBool = true;
            } else {
                groundDashBool = true;
            }
        }
        //
    }   

    void Airdash (float direction){
        airActions -= 1;
        p1Body.AddForce(new Vector2(direction *airDashSpeed,0) ,ForceMode2D.Impulse);
        p1Body.gravityScale = 0;
        gravityOn = frameNumber + airDashDuration;            
        airDashBool = false;
    }

    void AirBackdash(float direction){
        airActions -= 1;
        p1Body.AddForce(new Vector2(direction * airDashSpeed,0) ,ForceMode2D.Impulse);
        p1Body.gravityScale = 0;
        gravityOn = frameNumber + Mathf.FloorToInt(airDashDuration/2);            
        airDashBool = false;
    }

    void GroundBackdash(){
        Debug.Log("backdash");
        onGroundState = false;
        airActions -= 1;
        p1Body.transform.Translate(new Vector3(0.05f * movement.x,0.05f,0));
        p1Body.gravityScale = 0;
        gravityOn = frameNumber + backdashDuration;        
           
        p1Body.AddForce(Vector2.right * backdashSpeed * movement.x,ForceMode2D.Impulse);
        groundBackdashBool = false;


    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            onGroundState = true;
            canAirJump = false;

            p1Body.velocity = Vector2.zero;
            airActions = 1;
        }
    }
    private void AirJump(){
        if (airActions >= 1){
            p1Body.velocity = Vector2.zero;
            p1Body.AddForce(new Vector2(movement.x * speed * 2/3, jumpSpeed),ForceMode2D.Impulse);
            airActions -= 1;
            canAirJump = false;
            Debug.Log("dj");
            onGroundState = false;
        }

    }
    
    private void LeftJump(){
        if (onGroundState){
            p1Body.AddForce(new Vector2(-speed * 2/3, jumpSpeed),ForceMode2D.Impulse);
            onGroundState = false;
            
        }
    }

    private void NeutralJump(){
        if (onGroundState){
            p1Body.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);            
            onGroundState = false;
            
        }
    }

    private void RightJump(){
        if (onGroundState){
            p1Body.AddForce(new Vector2(speed * 2/3, jumpSpeed), ForceMode2D.Impulse);            
            onGroundState = false;            
        }

    }

    private void LeftWalk(){
        if (onGroundState){
            if(groundDashBool){
                p1Body.AddForce(Vector2.left * dashSpeed);
                if (p1Body.velocity.x > -dashSpeed){
                    p1Body.velocity = Vector2.left * dashSpeed;
                }   
            } else {
                p1Body.AddForce(Vector2.left * speed);
                if (p1Body.velocity.x > -speed){
                    p1Body.velocity = Vector2.left * speed;
                }
            }
        }
        
    }

    private void Stop(){
        if(onGroundState){
            p1Body.velocity = Vector2.zero;
        }
        groundDashBool = false;

    }

    private void RightWalk(){
        if (onGroundState){
            if(groundDashBool){
                p1Body.AddForce(Vector2.right * dashSpeed);
                if (p1Body.velocity.x < dashSpeed){
                    p1Body.velocity = Vector2.right * dashSpeed;
                }
            } else {
                p1Body.AddForce(Vector2.right * speed);
                if (p1Body.velocity.x < speed){
                    p1Body.velocity = Vector2.right * speed;
                }
            }
        }
        
    }

        
    

    private void LeftCrouch(){

    }

    private void NeutralCrouch(){

    }

    private void RightCrouch(){

    }


}
