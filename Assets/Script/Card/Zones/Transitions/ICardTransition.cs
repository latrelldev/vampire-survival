using System.Collections;
using UnityEngine;

public interface ICardTransition
{
    void Set(CardViewController cardViewController);
    bool ValidateTransition(ICardZoneView from, ICardZoneView to);
    IEnumerator Execute(CardInstance card, ICardZoneView from, ICardZoneView to);
}
