using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerSelectController : MonoBehaviour
{
    public PlayerSelectConstants playerSelectConstants;
    public PlayerSelectConstants.CharacterSelection p1Character;
    public PlayerSelectConstants.CharacterSelection p2Character;
    public PlayerSelectConstants.CharacterSelection stage;
    private Animator animator;
    private Vector2 movement;
    private Dictionary<PlayerSelectConstants.CharacterSelection, RuntimeAnimatorController> charSpriteDict;
    private Dictionary<PlayerSelectConstants.CharacterSelection, AnimationClip> stageAnimationDict;
    private PlayerSelectConstants.CharacterSelection[] charArray;
    private PlayerSelectConstants.CharacterSelection[] stageArray;
    private string thisPlayerTag;
    public static bool P1selected = false;
    public static bool P2selected = false;

    private static bool stageSelected = false;
    public TextMeshProUGUI playerText;
    // Start is called before the first frame update
    void Start()
    {
        thisPlayerTag = this.gameObject.tag;
        animator = GetComponent<Animator>();
        p1Character = playerSelectConstants.p1Character;
        p2Character = playerSelectConstants.p2Character;
        stage = playerSelectConstants.stage;
        charSpriteDict = new Dictionary<PlayerSelectConstants.CharacterSelection, RuntimeAnimatorController>(){
            {PlayerSelectConstants.CharacterSelection.Pawn, playerSelectConstants.pawnSprite},
            {PlayerSelectConstants.CharacterSelection.Knight, playerSelectConstants.knightSprite},
            {PlayerSelectConstants.CharacterSelection.Bishop, playerSelectConstants.bishopSprite},
            {PlayerSelectConstants.CharacterSelection.Rook, playerSelectConstants.rookSprite},
        };
        charArray = charSpriteDict.Keys.ToArray();
        stageAnimationDict = new Dictionary<PlayerSelectConstants.CharacterSelection, AnimationClip>(){
            {PlayerSelectConstants.CharacterSelection.Pawn, playerSelectConstants.pawnStageAnimation},
            {PlayerSelectConstants.CharacterSelection.Knight, playerSelectConstants.knightStageAnimation},
            {PlayerSelectConstants.CharacterSelection.Bishop, playerSelectConstants.bishopStageAnimation},
            {PlayerSelectConstants.CharacterSelection.Rook, playerSelectConstants.rookStageAnimation},
        };
        stageArray = stageAnimationDict.Keys.ToArray();
        if (thisPlayerTag == "Player1"){
            animator.runtimeAnimatorController = charSpriteDict[p1Character];
        } else if (thisPlayerTag == "Player2"){
            animator.runtimeAnimatorController = charSpriteDict[p2Character];
        }

        if (playerText.text == "P1"){
            playerText.color =  PlayerSelectController.P1selected ? Color.green : Color.red;
        }if (playerText.text == "P2"){
            playerText.color =  PlayerSelectController.P2selected ? Color.green : Color.red;
        }

        StartCoroutine(WaitForSelect());
    }

    void OnNavigate(InputValue value)
    {
        int charLen = charArray.Length;
        int stageLen = stageArray.Length;
        
        movement = value.Get<Vector2>();
        if (movement.x > 0){
             if (thisPlayerTag == "Player1" && !P1selected) {
                int index = Array.IndexOf(charArray, p1Character);
                if (index == charLen - 1) {
                    index = 0;
                } else {
                    index += 1;
                }
                p1Character = charArray[index];
                playerSelectConstants.p1Character = p1Character;
                animator.runtimeAnimatorController = charSpriteDict[p1Character];
            } else if (thisPlayerTag == "Player2" && !P2selected) {
                int index = Array.IndexOf(charArray, p2Character);
                if (index == charLen - 1) {
                    index = 0;
                } else {
                    index += 1;
                }
                p2Character = charArray[index];
                playerSelectConstants.p2Character = p2Character;
                animator.runtimeAnimatorController = charSpriteDict[p2Character];
            } else if (P1selected && P2selected){
                int index = Array.IndexOf(stageArray, stage);
                if (index == stageLen - 1) {
                    index = 0;
                } else {
                    index += 1;
                }
                stage = stageArray[index];
                playerSelectConstants.stage = stage;
                // change stage sprite
            }
        }else if (movement.x < 0){
            if (thisPlayerTag == "Player1" && !P1selected) {
                int index = Array.IndexOf(charArray, p1Character);
                if (index == 0) {
                    index = charLen - 1;
                } else {
                    index -= 1;
                }
                p1Character = charArray[index];
                playerSelectConstants.p1Character = p1Character;
                animator.runtimeAnimatorController = charSpriteDict[p1Character];
            } else if (thisPlayerTag == "Player2" && !P2selected) {
                int index = Array.IndexOf(charArray, p2Character);
                if (index == 0) {
                    index = charLen - 1;
                } else {
                    index -= 1;
                }
                p2Character = charArray[index];
                playerSelectConstants.p2Character = p2Character;
                animator.runtimeAnimatorController = charSpriteDict[p2Character];
            } else if (P1selected && P2selected){
                int index = Array.IndexOf(stageArray, stage);
                if (index == 0) {
                    index = stageLen - 1;
                } else {
                    index -= 1;
                }
                stage = stageArray[index];
                playerSelectConstants.stage = stage;
                // change stage sprite
            }
        }
    }

    void OnSelect()
    {
        if (thisPlayerTag == "Player1" && !P1selected) {
            P1selected = true;
            playerText.color =  Color.green;
        } else if (thisPlayerTag == "Player2" && !P2selected) {
            P2selected = true;
            playerText.color =  Color.green;
        } else if (P1selected && P2selected){
            stageSelected = true;
        }
    }

    void OnCancel()
    {
        if (thisPlayerTag == "Player1" && P1selected) {
            P1selected = false;
            playerText.color =  Color.red;
        } else if (thisPlayerTag == "Player2" && P2selected) {
            P2selected = false;
            playerText.color =  Color.red;
        } 
    }

    private IEnumerator WaitForSelect()
    {   
        if (!(P1selected == true && P2selected == true && stageSelected == true)) {
		    yield  return  new  WaitUntil(() =>  (P1selected == true && P2selected == true && stageSelected == true));
	    }

        LoadFight(); 
    }

    void LoadFight()
    {   
        stageSelected = false;
        P1selected = false;
        P2selected = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
