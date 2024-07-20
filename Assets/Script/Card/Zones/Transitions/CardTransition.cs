using System.Collections;

public abstract class CardTransition<TFrom, TTo> : ICardTransition where TFrom : ICardZoneView where TTo : ICardZoneView
{
    public virtual bool ValidateTransition(ICardZoneView from, ICardZoneView to)
    {
        return (from is TFrom) && (to is TTo);
    }

    public IEnumerator Execute(ViewAnchorReference reference, ICardZoneView from, ICardZoneView to)
    {
        return ExecuteTransition(reference, (TFrom)from, (TTo)to);
    }

    protected abstract IEnumerator ExecuteTransition(ViewAnchorReference reference, TFrom from, TTo to);
}
