using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    public PlayerSelectConstants playerSelectConstants;
    private PlayerSelectConstants.CharacterSelection stage;
    private Animator animator;
    private AnimatorOverrideController animatorOverrideController;
    private AudioSource audioSource;
    private Dictionary<PlayerSelectConstants.CharacterSelection, (AnimationClip, AudioClip, float)> stageAnimationDict;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        audioSource = GetComponent<AudioSource>();
        stage = playerSelectConstants.stage;
        stageAnimationDict = new Dictionary<PlayerSelectConstants.CharacterSelection, (AnimationClip, AudioClip, float)>(){
            {PlayerSelectConstants.CharacterSelection.Pawn, (playerSelectConstants.pawnStageAnimation, playerSelectConstants.pawnStageAudio, playerSelectConstants.pawnStageSpeed)},
            {PlayerSelectConstants.CharacterSelection.Knight, (playerSelectConstants.knightStageAnimation, playerSelectConstants.knightStageAudio, playerSelectConstants.knightStageSpeed)},
            {PlayerSelectConstants.CharacterSelection.Bishop, (playerSelectConstants.bishopStageAnimation, playerSelectConstants.bishopStageAudio, playerSelectConstants.bishopStageSpeed)},
            {PlayerSelectConstants.CharacterSelection.Rook, (playerSelectConstants.rookStageAnimation, playerSelectConstants.rookStageAudio, playerSelectConstants.rookStageSpeed)},
        };
        animator.runtimeAnimatorController = animatorOverrideController;
        animatorOverrideController["blahaj_living_room"] = stageAnimationDict[stage].Item1;
        audioSource.clip = stageAnimationDict[stage].Item2;
        audioSource.volume = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
        audioSource.Play();
        animator.speed = stageAnimationDict[stage].Item3;
    }
}
