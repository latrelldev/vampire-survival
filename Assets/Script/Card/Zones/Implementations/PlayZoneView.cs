using System;

public class PlayZoneView : CardZoneView<PlayZone>, IDropZone
{
    public void OnCardDrop(CardAnchor cardObject, ICardZoneView from)
    {
        
    }
}

[Serializable]
public class PlayZone:CardZone
{

}

