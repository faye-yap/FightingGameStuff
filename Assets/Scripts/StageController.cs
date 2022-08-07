using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    public PlayerSelectConstants playerSelectConstants;
    private PlayerSelectConstants.CharacterSelection stage;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private Dictionary<PlayerSelectConstants.CharacterSelection, (Sprite, AudioClip)> stageSpriteDict;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        stage = playerSelectConstants.stage;
        stageSpriteDict = new Dictionary<PlayerSelectConstants.CharacterSelection, (Sprite, AudioClip)>(){
            // {PlayerSelectConstants.CharacterSelection.Pawn, (playerSelectConstants.pawnStageSprite, playerSelectConstants.pawnStageAudio)},
            // {PlayerSelectConstants.CharacterSelection.Knight, (playerSelectConstants.knightStageSprite, playerSelectConstants.knightStageAudio)},
            {PlayerSelectConstants.CharacterSelection.Bishop, (playerSelectConstants.bishopStageSprite, playerSelectConstants.bishopStageAudio)},
            {PlayerSelectConstants.CharacterSelection.Rook, (playerSelectConstants.rookStageSprite, playerSelectConstants.rookStageAudio)},
        };
        spriteRenderer.sprite = stageSpriteDict[stage].Item1;
        audioSource.clip = stageSpriteDict[stage].Item2;
    }

    // Update is called once per frame
    // void Update()
    // {
    //     if (stage != playerSelectConstants.stage){
    //         stage = playerSelectConstants.stage;
    //         spriteRenderer.sprite = stageSpriteDict[stage].Item1;
    //         audioSource.clip = stageSpriteDict[stage].Item2;
    //     }
    // }
}
