using System.Collections;

public interface ICardTransition
{
    IEnumerator Execute(ViewAnchorReference reference, ICardZoneView from, ICardZoneView to);
    bool ValidateTransition(ICardZoneView from, ICardZoneView to);
}
