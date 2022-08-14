using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageSelectController : MonoBehaviour
{
    public PlayerSelectConstants playerSelectConstants;
    private PlayerSelectConstants.CharacterSelection stage;
    private Animator animator;
    private AnimatorOverrideController animatorOverrideController;
    private Dictionary<PlayerSelectConstants.CharacterSelection, (AnimationClip, float, string)> stageAnimationDict;
    public TextMeshProUGUI stageText;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        stage = playerSelectConstants.stage;
        stageAnimationDict = new Dictionary<PlayerSelectConstants.CharacterSelection, (AnimationClip, float, string)>(){
            {PlayerSelectConstants.CharacterSelection.Pawn, (playerSelectConstants.pawnStageAnimation, playerSelectConstants.pawnStageSpeed, playerSelectConstants.pawnStageName)},
            {PlayerSelectConstants.CharacterSelection.Knight, (playerSelectConstants.knightStageAnimation, playerSelectConstants.knightStageSpeed, playerSelectConstants.knightStageName)},
            {PlayerSelectConstants.CharacterSelection.Bishop, (playerSelectConstants.bishopStageAnimation, playerSelectConstants.bishopStageSpeed, playerSelectConstants.bishopStageName)},
            {PlayerSelectConstants.CharacterSelection.Rook, (playerSelectConstants.rookStageAnimation, playerSelectConstants.rookStageSpeed, playerSelectConstants.rookStageName)},
        };
        animator.runtimeAnimatorController = animatorOverrideController;
        animatorOverrideController["blahaj_living_room"] = stageAnimationDict[stage].Item1;
        animator.speed = stageAnimationDict[stage].Item2;
        stageText.text = "Stage: " + stageAnimationDict[stage].Item3;
    }

    // Update is called once per frame
    void Update()
    {
        if (stage != playerSelectConstants.stage){
            stage = playerSelectConstants.stage;
            animatorOverrideController["blahaj_living_room"] = stageAnimationDict[stage].Item1;
            animator.speed = stageAnimationDict[stage].Item2;
            stageText.text = "Stage: " + stageAnimationDict[stage].Item3;
        }
    }
}
