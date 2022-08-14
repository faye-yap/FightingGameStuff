using UnityEngine;

[CreateAssetMenu(fileName =  "PlayerSelectConstants", menuName =  "ScriptableObjects/PlayerSelectConstants", order =  1)]
public class PlayerSelectConstants : ScriptableObject
{
	public enum CharacterSelection { Pawn, Bishop, Rook, Knight };

	public CharacterSelection p1Character = CharacterSelection.Knight;
	public CharacterSelection p2Character = CharacterSelection.Knight;

	public RuntimeAnimatorController pawnSprite;
	public RuntimeAnimatorController bishopSprite;
	public RuntimeAnimatorController rookSprite;
	public RuntimeAnimatorController knightSprite;

	public CharacterSelection stage = CharacterSelection.Bishop;

	public AnimationClip pawnStageAnimation;
	public AnimationClip bishopStageAnimation;
	public AnimationClip rookStageAnimation;
	public AnimationClip knightStageAnimation;

	public float pawnStageSpeed;
	public float bishopStageSpeed;
	public float rookStageSpeed;
	public float knightStageSpeed;

	public string pawnStageName;
	public string bishopStageName;
	public string rookStageName;
	public string knightStageName;

	public AudioClip pawnStageAudio;
	public AudioClip bishopStageAudio;
	public AudioClip rookStageAudio;
	public AudioClip knightStageAudio;	
}
