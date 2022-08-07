using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerSelectController : MonoBehaviour
{
    public PlayerSelectConstants playerSelectConstants;
    public PlayerSelectConstants.CharacterSelection p1Character;
    public PlayerSelectConstants.CharacterSelection p2Character;
    private SpriteRenderer spriteRenderer;
    private Vector2 movement;
    private Dictionary<PlayerSelectConstants.CharacterSelection, Sprite> spriteDict;
    // private PlayerSelectConstants.CharacterSelection[] charArray = new PlayerSelectConstants.CharacterSelection[3];
    private PlayerSelectConstants.CharacterSelection[] charArray = new PlayerSelectConstants.CharacterSelection[4];
    private string thisPlayerTag;
    private static bool P1selected = false;
    private static bool P2selected = false;
    // Start is called before the first frame update
    void Start()
    {
        thisPlayerTag = this.gameObject.tag;
        spriteRenderer = GetComponent<SpriteRenderer>();
        p1Character = playerSelectConstants.p1Character;
        p2Character = playerSelectConstants.p2Character;
        spriteDict = new Dictionary<PlayerSelectConstants.CharacterSelection, Sprite>(){
            {PlayerSelectConstants.CharacterSelection.Pawn, playerSelectConstants.pawnSprite},
            {PlayerSelectConstants.CharacterSelection.Knight, playerSelectConstants.knightSprite},
            {PlayerSelectConstants.CharacterSelection.Bishop, playerSelectConstants.bishopSprite},
            {PlayerSelectConstants.CharacterSelection.Rook, playerSelectConstants.rookSprite},
        };
        spriteDict.Keys.CopyTo(charArray, 0);
        if (thisPlayerTag == "Player1"){
            spriteRenderer.sprite = spriteDict[p1Character];
        } else if (thisPlayerTag == "Player2"){
            spriteRenderer.sprite = spriteDict[p2Character];
        }
        StartCoroutine(WaitForSelect());
    }

    void OnNavigate(InputValue value)
    {
        int len = charArray.Length;
        
        movement = value.Get<Vector2>();
        if (movement.x > 0){
             if (thisPlayerTag == "Player1" && !P1selected) {
                int index = Array.IndexOf(charArray, p1Character);
                if (index == len - 1) {
                    index = 0;
                } else {
                    index += 1;
                }
                p1Character = charArray[index];
                playerSelectConstants.p1Character = p1Character;
                spriteRenderer.sprite = spriteDict[p1Character];
            } else if (thisPlayerTag == "Player2" && !P2selected) {
                int index = Array.IndexOf(charArray, p2Character);
                if (index == len - 1) {
                    index = 0;
                } else {
                    index += 1;
                }
                p2Character = charArray[index];
                playerSelectConstants.p2Character = p2Character;
                spriteRenderer.sprite = spriteDict[p2Character];
            }
        }else if (movement.x < 0){
            if (thisPlayerTag == "Player1" && !P1selected) {
                int index = Array.IndexOf(charArray, p1Character);
                if (index == 0) {
                    index = len - 1;
                } else {
                    index -= 1;
                }
                p1Character = charArray[index];
                playerSelectConstants.p1Character = p1Character;
                spriteRenderer.sprite = spriteDict[p1Character];
            } else if (thisPlayerTag == "Player2" && !P2selected) {
                int index = Array.IndexOf(charArray, p2Character);
                if (index == 0) {
                    index = len - 1;
                } else {
                    index -= 1;
                }
                p2Character = charArray[index];
                playerSelectConstants.p2Character = p2Character;
                spriteRenderer.sprite = spriteDict[p2Character];
            }
        }
    }

    void OnSelect()
    {
        if (thisPlayerTag == "Player1") {
            P1selected = true;
        } else {
            P2selected = true;
        }
    }

    void OnCancel()
    {
        if (thisPlayerTag == "Player1") {
            P1selected = false;
        } else {
            P2selected = false;
        }
    }

    private IEnumerator WaitForSelect()
    {   
        if (!(P1selected == true && P2selected == true)) {
		    yield  return  new  WaitUntil(() =>  (P1selected == true && P2selected == true));
	    }
        P1selected = false;
        P2selected = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
