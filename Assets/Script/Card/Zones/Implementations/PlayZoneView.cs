using System;
using UnityEngine;

public class PlayZoneView : CardZoneView<PlayZone>
{
    [SerializeField]
    private DropZone dropZone;

    private void OnEnable()
    {
        dropZone.OnCardDropped += OnCardDropped;
    }

    private void OnDisable()
    {
        dropZone.OnCardDropped -= OnCardDropped;
    }

    private void OnCardDropped(CardInstance instance, ICardZoneView view)
    {
        //if view is Hand
        //if cost is paid for
        viewController.MoveCard<Discard>(instance);
        instance.Card.OnCardPlayed();
    }
}

[Serializable]
public class PlayZone:CardZone
{

}

