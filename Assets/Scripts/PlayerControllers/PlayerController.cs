using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    private const int MaxInt = 2147483647;
    

    private Rigidbody2D opponentBody;
    [HideInInspector]
    public string opponentTag;
    private Dictionary<string,int> opponentDamageValues;
    private Rigidbody2D thisPlayerBody;
    private string thisPlayerTag;
    
    private SpriteRenderer thisPlayerSprite;
    private BoxCollider2D thisPlayerCollider; 
    [HideInInspector]
    public Animator playerAnimator;
    private Vector2 movement;
    private bool onGroundState = true;
    private bool airDashBool;
    private bool groundDashBool;
    private bool groundBackdashBool;
    
    
    private int gravityOn = 0;
    private float gravityScale;    
    private Vector3 xMidpoint;    
    private int airActions = 1;
    private bool canAirJump;
    private int setAirJump = 0;
    [HideInInspector]
    public bool isIdle = true;
    [HideInInspector]
    public bool canDashJumpCancel = true;

    [HideInInspector]
    public bool canACancel = false;
    [HideInInspector]
    public bool canBCancel = false;
    [HideInInspector]
    public bool canSCancel = false;
    private bool canAirNormal = false;
    
    public CharacterConstants characterConstants;
    public GameManager gameManager;
    public PauseMenuController pauseMenuController;
    public GameObject pawnCharacter;
    public GameObject bishopCharacter;
    public GameObject knightCharacter;
    public GameObject rookCharacter;
    private GameObject selectedChar;
    [HideInInspector]
    public bool hasBeenHit = false;
    private bool isStandBlocking = false;
    private bool isCrouchBlocking = false;
    
    

    
    

    //spawn character
    void SelectCharacter(string playerNumber){
        
        Debug.Log(thisPlayerTag + playerNumber);
        switch(playerNumber){
                case "Pawn":
                    selectedChar = Instantiate(pawnCharacter, this.transform.position,Quaternion.identity);
                    selectedChar.transform.parent = gameObject.transform;
                    characterConstants = GameObject.Find("PawnConstants").GetComponent<CharacterConstants>();
                    break;
                
                case "Bishop":
                    selectedChar = Instantiate(bishopCharacter, this.transform.position,Quaternion.identity);
                    selectedChar.transform.parent = gameObject.transform;
                    characterConstants = GameObject.Find("BishopConstants").GetComponent<CharacterConstants>();
                    break;

                case "Rook":
                    selectedChar = Instantiate(rookCharacter, this.transform.position,Quaternion.identity);
                    selectedChar.transform.parent = gameObject.transform;
                    characterConstants = GameObject.Find("RookConstants").GetComponent<CharacterConstants>();
                    break;

                case "Knight":
                    selectedChar = Instantiate(knightCharacter, this.transform.position,Quaternion.identity);
                    selectedChar.transform.parent = gameObject.transform;
                    characterConstants = GameObject.Find("KnightConstants").GetComponent<CharacterConstants>();
                    break;
            }
        selectedChar.transform.localScale = new Vector3(1,1,1);
    }
    

    // Start is called before the first frame update
    void Start()
    {
        
        thisPlayerTag = this.gameObject.tag;
        thisPlayerBody = GetComponent<Rigidbody2D>();
        
        if (thisPlayerTag == "Player1") {
            opponentBody = GameObject.FindGameObjectWithTag("Player2").GetComponent<Rigidbody2D>();
            SelectCharacter(gameManager.p1Character);
            gameManager.p1MaxHP = characterConstants.maxHP;
            gameManager.p1CurrentHP = characterConstants.maxHP;
            opponentDamageValues = GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerController>().characterConstants.damageValues; 

        } else {
            opponentBody = GameObject.FindGameObjectWithTag("Player1").GetComponent<Rigidbody2D>();
            SelectCharacter(gameManager.p2Character);
            gameManager.p2MaxHP = characterConstants.maxHP;
            gameManager.p2CurrentHP = characterConstants.maxHP;
            opponentDamageValues = GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerController>().characterConstants.damageValues; 
        }

        opponentTag = opponentBody.tag;
        
        

        thisPlayerBody = GetComponent<Rigidbody2D>();
        thisPlayerSprite = GetComponentInChildren<SpriteRenderer>();
        thisPlayerCollider = GetComponent<BoxCollider2D>();
        this.transform.GetChild(1).tag = thisPlayerTag; //assign character to player
        this.transform.GetChild(1).transform.GetChild(0).tag = thisPlayerTag; //assign character hurtbox to player
        
        gravityScale = thisPlayerBody.gravityScale;
        playerAnimator = GetComponentInChildren<Animator>();
        Debug.Log(opponentDamageValues);

        
        

        
        
        
    }

    

    // Update is called once per frame
    void Update()
    {

        
        //sets sprites facing each other
        xMidpoint = new Vector3((thisPlayerBody.transform.position.x + opponentBody.transform.position.x)/2,0,0);
        if (xMidpoint.x < thisPlayerBody.transform.position.x && gameObject.transform.localScale.x > 0){
            gameObject.transform.localScale = new Vector3(-gameObject.transform.localScale.x,gameObject.transform.localScale.y,gameObject.transform.localScale.z);
            //Debug.Log("facing left");
        } else if (xMidpoint.x > thisPlayerBody.transform.position.x && gameObject.transform.localScale.x < 0){
            gameObject.transform.localScale = new Vector3(-gameObject.transform.localScale.x,gameObject.transform.localScale.y,gameObject.transform.localScale.z);
            //Debug.Log("facing right");
        }

        //airdashing only
        if (gameManager.frameNumber >= gravityOn){
            thisPlayerBody.gravityScale = gravityScale;
            gravityOn = MaxInt;
        }

        //frames after jumping to be able to jump or jA/jB
        if (gameManager.frameNumber >= setAirJump){
            //Debug.Log(setAirJump);
            canAirJump = true;
            canAirNormal = true;
            setAirJump = MaxInt;
        }

        if (isIdle){
            Move();
        }

        

    }

    

    private void OnTriggerEnter2D(Collider2D c) {
        if (c.gameObject.CompareTag(opponentTag) && c.gameObject.name == "Hitbox"){
            //Debug.Log(c.ToString());
            //Debug.Log(opponentTag);
            string moveName = c.transform.parent.name.Substring(0,c.transform.parent.name.Length - 7);
            PlayerController opponentController = GameObject.FindGameObjectWithTag(opponentTag).GetComponent<PlayerController>();
            switch(moveName.Substring(moveName.Length - 1, 1)){
                case "A":                    
                    opponentController.canACancel = true;
                    opponentController.canBCancel = true;
                    opponentController.canSCancel = true;

                    bool isBlocked;
                    if (moveName.Contains("Neutral") && (isStandBlocking || isCrouchBlocking)){
                        isBlocked = true;
                    } else if (moveName.Contains("Crouching") && isCrouchBlocking) {
                        isBlocked = true;
                    } else if (moveName.Contains("Jumping") && isStandBlocking){
                        isBlocked = true;
                    } else {
                        isBlocked = false;
                    }


                    if (isBlocked) {
                        if(onGroundState) thisPlayerBody.velocity = new Vector2(gameObject.transform.localScale.x * 1.25f * -3.0f,0);
                        else thisPlayerBody.velocity = new Vector2(gameObject.transform.localScale.x * 1.25f * -2.0f,5f);
                        if(Mathf.Abs(thisPlayerBody.transform.position.x) > 12f) opponentBody.velocity = new Vector2(gameObject.transform.localScale.x * 1.25f * 3.0f,0);
                        playerAnimator.Play("Blocked");
                        isIdle = false;
                    } else {
                        if(onGroundState) thisPlayerBody.velocity = new Vector2(gameObject.transform.localScale.x * 1.25f * -5.0f,0);
                        else thisPlayerBody.velocity = new Vector2(gameObject.transform.localScale.x * 1.25f * -4,9);
                        if(Mathf.Abs(thisPlayerBody.transform.position.x) > 12f) opponentBody.velocity = new Vector2(gameObject.transform.localScale.x * 1.25f * 5.0f,0);
                        gameManager.TakeDamage(thisPlayerTag,opponentDamageValues[moveName]);
                        playerAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
                        isIdle = false;
                        playerAnimator.Play("GotHit");
                    }
                    //Debug.Log("A");
                    break;

                case "B":   
                    //if not 3b                 
                    opponentController.canSCancel = true;
                    //Debug.Log("B");
                    break;
            }

            if (!hasBeenHit) {                
                hasBeenHit = true;
            }
        }
    }

    


    void OnMove(InputValue value) {
        if(pauseMenuController.GameIsPaused){
            return;
        }
        movement = value.Get<Vector2>();
    }

    void OnDash(){
        if(pauseMenuController.GameIsPaused){
            return;
        }
        if(canDashJumpCancel){
            if (!onGroundState && airActions >= 1){
                airDashBool = true;
            } else {
                if (movement.x == -1 && gameObject.transform.localScale.x > 0  || movement.x == 1 && gameObject.transform.localScale.x < 0){
                    //Debug.Log("dash");
                    groundBackdashBool = true;
                } else {
                    groundDashBool = true;
                }
            }
        }
        
    }   

    

    void Airdash (float direction){
        airActions -= 1;
        thisPlayerBody.AddForce(new Vector2(direction *characterConstants.airdashSpeed,0) ,ForceMode2D.Impulse);
        thisPlayerBody.gravityScale = 0;
        gravityOn = gameManager.frameNumber + characterConstants.airDashDuration;            
        airDashBool = false;
    }

    void AirBackdash(float direction){
        airActions -= 1;
        thisPlayerBody.AddForce(new Vector2(direction * characterConstants.airdashSpeed,0) ,ForceMode2D.Impulse);
        thisPlayerBody.gravityScale = 0;
        gravityOn = gameManager.frameNumber + Mathf.FloorToInt(characterConstants.airDashDuration/2);            
        airDashBool = false;
    }

    void GroundBackdash(){
        Debug.Log("backdash");
        onGroundState = false;
        airActions -= 1;
        thisPlayerBody.transform.Translate(new Vector3(0.05f * movement.x,0.05f,0));
        thisPlayerBody.gravityScale = 0;
        gravityOn = gameManager.frameNumber + characterConstants.backdashDuration;        
           
        thisPlayerBody.AddForce(Vector2.right * characterConstants.backdashSpeed * movement.x,ForceMode2D.Impulse);
        groundBackdashBool = false;


    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            onGroundState = true;
            canAirJump = false;
            canAirNormal = false;
            thisPlayerBody.velocity = Vector2.zero;
            airActions = 1;

            isIdle = true;
            canDashJumpCancel = true;
        } 
    }

   
    private void AirJump(){
        if (airActions >= 1){
            thisPlayerBody.velocity = Vector2.zero;
            thisPlayerBody.AddForce(new Vector2(movement.x * characterConstants.moveSpeed * 2/3, characterConstants.jumpSpeed),ForceMode2D.Impulse);
            airActions -= 1;
            canAirJump = false;
            //Debug.Log("dj");
            onGroundState = false;
        }

    }
    
    private void LeftJump(){
        if (onGroundState && canDashJumpCancel){
            
            thisPlayerBody.AddForce(new Vector2(-characterConstants.moveSpeed * 2/3, characterConstants.jumpSpeed),ForceMode2D.Impulse);
            onGroundState = false;
            
        }
        if (gameObject.transform.localScale.x > 0){
            isStandBlocking = true;
        } else {
            isStandBlocking = false;
        }
    }

    private void NeutralJump(){
        if (onGroundState && canDashJumpCancel){
            thisPlayerBody.AddForce(new Vector2(0, characterConstants.jumpSpeed), ForceMode2D.Impulse);            
            onGroundState = false;
            
        }
        isStandBlocking  = false;
        isCrouchBlocking = false;
    }

    private void RightJump(){
        if (onGroundState && canDashJumpCancel){
            thisPlayerBody.AddForce(new Vector2(characterConstants.moveSpeed * 2/3, characterConstants.jumpSpeed), ForceMode2D.Impulse);            
            onGroundState = false;            
        }
        if (gameObject.transform.localScale.x < 0){
            isStandBlocking = true;
        } else {
            isStandBlocking = false;
        }

    }

    private void LeftWalk(){
        if (onGroundState){
            if(groundDashBool){
                thisPlayerBody.AddForce(Vector2.left * characterConstants.dashSpeed);
                if (thisPlayerBody.velocity.x > -characterConstants.dashSpeed){
                    thisPlayerBody.velocity = Vector2.left * characterConstants.dashSpeed;
                }   
            } else {
                thisPlayerBody.AddForce(Vector2.left * characterConstants.moveSpeed);
                if (thisPlayerBody.velocity.x > -characterConstants.moveSpeed){
                    thisPlayerBody.velocity = Vector2.left * characterConstants.moveSpeed;
                }
            }
        }
        if (gameObject.transform.localScale.x > 0){
            isStandBlocking = true;
        } else {
            isStandBlocking = false;
        }
        
    }

    private void Stop(){
        //if(onGroundState){ thisPlayerBody.velocity = new Vector2(0,thisPlayerBody); }
        
        groundDashBool = false;
        isStandBlocking  = false;
        isCrouchBlocking = false;

    }

    private void RightWalk(){
        if (onGroundState){
            if(groundDashBool){
                thisPlayerBody.AddForce(Vector2.right * characterConstants.dashSpeed);
                if (thisPlayerBody.velocity.x < characterConstants.dashSpeed){
                    thisPlayerBody.velocity = Vector2.right * characterConstants.dashSpeed;
                }
            } else {
                thisPlayerBody.AddForce(Vector2.right * characterConstants.moveSpeed);
                if (thisPlayerBody.velocity.x < characterConstants.moveSpeed){
                    thisPlayerBody.velocity = Vector2.right * characterConstants.moveSpeed;
                }
            }
        }

        if (gameObject.transform.localScale.x < 0){
            isStandBlocking = true;
        } else {
            isStandBlocking = false;
        }
        
    }

        
    

    private void LeftCrouch(){
        isStandBlocking = false;
        if (gameObject.transform.localScale.x > 0){
            isCrouchBlocking = true;
        } else {
            isCrouchBlocking = false;
        }

    }

    private void NeutralCrouch(){
        isStandBlocking  = false;
        isCrouchBlocking = false;
    }

    private void RightCrouch(){
        isStandBlocking = false;
        if (gameObject.transform.localScale.x < 0){
            isCrouchBlocking = true;
        } else {
            isCrouchBlocking = false;
        }
    }

    private void Move(){
        switch (movement.y){
            case (1):   
                if (canAirJump && !onGroundState) {
                    AirJump();
                } else if (onGroundState){
                    setAirJump = gameManager.frameNumber + characterConstants.framesUntilAirJump;
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
            thisPlayerBody.velocity = Vector2.zero;
            if (gameObject.transform.localScale.x < 0){
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
    }

    void OnANormal(){
        if(pauseMenuController.GameIsPaused){
            return;
        }
        if(onGroundState && (canACancel || isIdle)){

            if(transform.childCount > 2){
            Destroy(transform.GetChild(2).gameObject);
            }

            canDashJumpCancel = false;
            isIdle = false;
            canACancel = false;
            canBCancel = false;
            canSCancel = false;
            if(movement.y == 0){    
                GameObject neutralA = Instantiate(characterConstants.neutralAPrefab,this.transform.position,Quaternion.identity);
                neutralA.transform.parent = this.transform;
                neutralA.transform.localScale = new Vector3(1,1,1);
                playerAnimator.Play("NeutralA");
            } else if (movement.y == -1){
                
                playerAnimator.Play("CrouchingA");
            }

        } else if (!onGroundState && canAirNormal){

            isIdle = false;
            canDashJumpCancel = false;
            playerAnimator.Play("JumpingA");

        }

    }

    void OnBNormal(){
        if(pauseMenuController.GameIsPaused){
            return;
        }
        if(onGroundState && (canBCancel || isIdle)){

            if(transform.childCount > 1){
                Destroy(transform.GetChild(2).gameObject);
            }

            canACancel = false;
            canBCancel = false;
            canSCancel = false;
            canDashJumpCancel = false;
            isIdle = false;
            if(movement.y == 0){
                
                playerAnimator.Play("NeutralB");
            } else if (movement.y == -1){
                
                playerAnimator.Play("CrouchingB");
            }

        } else if (!onGroundState && canAirNormal){
            isIdle = false;
            canDashJumpCancel = false;
            
            playerAnimator.Play("JumpingB");

        }

    }

    void OnSpecial(){
        if(pauseMenuController.GameIsPaused){
            return;
        }
        if(onGroundState && (canSCancel || isIdle)){

            if(transform.childCount > 1){
                Destroy(transform.GetChild(2).gameObject);
            }

            canACancel = false;
            canBCancel = false;
            canSCancel = false;
            canDashJumpCancel = false;
            isIdle = false;
            if(movement == Vector2.zero){                
                playerAnimator.Play("NeutralS");
            } else if ((movement == Vector2.right && gameObject.transform.localScale.x > 0) || gameObject.transform.localScale.x < 0){
                
                playerAnimator.Play("ForwardS");
            } else if (movement.y == -1){                
                playerAnimator.Play("CrouchingS");    
            }

        } 

    }

    public void OnPause(){
        if (pauseMenuController.GameIsPaused){
            pauseMenuController.Resume();
        } else {
            pauseMenuController.Pause();
        }
    }
}
