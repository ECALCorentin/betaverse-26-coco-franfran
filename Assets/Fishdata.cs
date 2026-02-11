using UnityEngine;

[CreateAssetMenu(fileName = "NewFishData", menuName = "Fishing/Fish Data")]
public class FishData : ScriptableObject
{
    public string fishName;
    public enum Rarity { Commun, Rare, Epique, Legendaire }
    public Rarity rarity;
    public Color rarityColor = Color.white;
}