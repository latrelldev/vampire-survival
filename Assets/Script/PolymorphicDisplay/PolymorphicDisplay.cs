using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class PolymorphicDisplay : PropertyAttribute
{
  [NotNull] public Type baseType;

  public PolymorphicDisplay([NotNull] Type baseType)
  {
    this.baseType = baseType;
  }
}