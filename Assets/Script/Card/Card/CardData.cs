using UnityEngine;

[CreateAssetMenu(menuName = "New Card")]
public class CardData: ScriptableObject
{
    public Card Card => card;
    [SerializeReference, PolymorphicDisplay(typeof(Card))]
    public Card card;
}
