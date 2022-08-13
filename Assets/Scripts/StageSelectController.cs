using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectController : MonoBehaviour
{
    public PlayerSelectConstants playerSelectConstants;
    private PlayerSelectConstants.CharacterSelection stage;
    private Animator animator;
    private AnimatorOverrideController animatorOverrideController;
    private Dictionary<PlayerSelectConstants.CharacterSelection, AnimationClip> stageAnimationDict;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        stage = playerSelectConstants.stage;
        stageAnimationDict = new Dictionary<PlayerSelectConstants.CharacterSelection, AnimationClip>(){
            {PlayerSelectConstants.CharacterSelection.Pawn, playerSelectConstants.pawnStageAnimation},
            {PlayerSelectConstants.CharacterSelection.Knight, playerSelectConstants.knightStageAnimation},
            {PlayerSelectConstants.CharacterSelection.Bishop, playerSelectConstants.bishopStageAnimation},
            {PlayerSelectConstants.CharacterSelection.Rook, playerSelectConstants.rookStageAnimation},
        };
        Debug.Log(animator.GetCurrentAnimatorClipInfo(0)[0].clip.name);
        animator.runtimeAnimatorController = animatorOverrideController;
        
        animatorOverrideController["blahaj_living_room"] = stageAnimationDict[stage];
    }

    // Update is called once per frame
    void Update()
    {
        if (stage != playerSelectConstants.stage){
            stage = playerSelectConstants.stage;
            animatorOverrideController["blahaj_living_room"] = stageAnimationDict[stage];
        }
    }
}
