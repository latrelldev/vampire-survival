﻿using System.Collections;
using UnityEngine;

public abstract class CardTransition<TFrom, TTo> : MonoBehaviour, ICardTransition where TFrom : ICardZoneView where TTo : ICardZoneView
{
    protected CardViewController viewController;

    public void Set(CardViewController cardViewController)
    {
        viewController = cardViewController;
    }


    public virtual bool ValidateTransition(ICardZoneView from, ICardZoneView to)
    {
        return (from is TFrom) && (to is TTo);
    }

    public IEnumerator Execute(CardInstance card, ICardZoneView from, ICardZoneView to)
    {
        return ExecuteTransition(card, (TFrom)from, (TTo)to);
    }

    protected abstract IEnumerator ExecuteTransition(CardInstance card, TFrom from, TTo to);

}
