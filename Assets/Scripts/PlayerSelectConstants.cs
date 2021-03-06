using UnityEngine;

[CreateAssetMenu(fileName =  "PlayerSelectConstants", menuName =  "ScriptableObjects/PlayerSelectConstants", order =  1)]
public class PlayerSelectConstants : ScriptableObject
{
	public enum CharacterSelection { Pawn, Bishop, Rook, Knight };

	public CharacterSelection p1Character = CharacterSelection.Knight;
	public CharacterSelection p2Character = CharacterSelection.Pawn;

	public Sprite pawnSprite;
	public Sprite bishopSprite;
	public Sprite rookSprite;
	public Sprite knightSprite;
}