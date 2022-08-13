using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainMenuCharacterController: MonoBehaviour
{
    public PlayerSelectConstants playerSelectConstants;
    public PlayerSelectConstants.CharacterSelection p1Character;
    public PlayerSelectConstants.CharacterSelection p2Character;
    private Animator animator;
    private Dictionary<PlayerSelectConstants.CharacterSelection, RuntimeAnimatorController> charSpriteDict;
    private PlayerSelectConstants.CharacterSelection[] charArray;
    private string thisPlayerTag;
    // Start is called before the first frame update
    void Start()
    {
        thisPlayerTag = this.gameObject.tag;
        animator = GetComponent<Animator>();
        p1Character = playerSelectConstants.p1Character;
        p2Character = playerSelectConstants.p2Character;
        charSpriteDict = new Dictionary<PlayerSelectConstants.CharacterSelection, RuntimeAnimatorController>(){
            {PlayerSelectConstants.CharacterSelection.Pawn, playerSelectConstants.pawnSprite},
            {PlayerSelectConstants.CharacterSelection.Knight, playerSelectConstants.knightSprite},
            {PlayerSelectConstants.CharacterSelection.Bishop, playerSelectConstants.bishopSprite},
            {PlayerSelectConstants.CharacterSelection.Rook, playerSelectConstants.rookSprite},
        };
        charArray = charSpriteDict.Keys.ToArray();
        if (thisPlayerTag == "Player1"){
            animator.runtimeAnimatorController = charSpriteDict[p1Character];
        } else if (thisPlayerTag == "Player2"){
            animator.runtimeAnimatorController = charSpriteDict[p2Character];
        }
    }
}
