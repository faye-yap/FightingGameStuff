using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectController : MonoBehaviour
{
    public PlayerSelectConstants playerSelectConstants;
    private PlayerSelectConstants.CharacterSelection stage;
    private SpriteRenderer spriteRenderer;
    private Dictionary<PlayerSelectConstants.CharacterSelection, Sprite> stageSpriteDict;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        stage = playerSelectConstants.stage;
        stageSpriteDict = new Dictionary<PlayerSelectConstants.CharacterSelection, Sprite>(){
            // {PlayerSelectConstants.CharacterSelection.Pawn, playerSelectConstants.pawnStageSprite},
            // {PlayerSelectConstants.CharacterSelection.Knight, playerSelectConstants.knightStageSprite},
            {PlayerSelectConstants.CharacterSelection.Bishop, playerSelectConstants.bishopStageSprite},
            {PlayerSelectConstants.CharacterSelection.Rook, playerSelectConstants.rookStageSprite},
        };
        spriteRenderer.sprite = stageSpriteDict[stage];
    }

    // Update is called once per frame
    void Update()
    {
        if (stage != playerSelectConstants.stage){
            stage = playerSelectConstants.stage;
            spriteRenderer.sprite = stageSpriteDict[playerSelectConstants.stage];
        }
    }
}
