using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(PolymorphicDisplay))]
public class PolymorphicDisplayEditor : PropertyDrawer
{
    private readonly Dictionary<Type, DerivedTypeDropdown> selectorCache = new();

    private const float SPACING = 2.0f;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var lineSize = EditorGUIUtility.singleLineHeight;
        var fieldSpacing = EditorGUIUtility.standardVerticalSpacing;

        EditorGUI.BeginProperty(position, label, property);
        position.x -= SPACING;
        position.width += SPACING * 2.0f;
        position.y -= SPACING;
        position.height += SPACING * 2.0f;

        EditorGUI.DrawRect(position, new Color(0.28f, 0.28f, 0.28f));
        position.x += SPACING;
        position.y += SPACING;

        position.width -= SPACING * 2.0f;
        position.height -= SPACING * 2.0f;

        var customAttribute = (PolymorphicDisplay)this.attribute;

        var selectType = customAttribute.baseType;

        if (!this.selectorCache.TryGetValue(selectType, out var selector))
        {
            selector = new DerivedTypeDropdown(selectType);
            this.selectorCache.Add(selectType, selector);
        }

        selector.RefreshSelection(property.managedReferenceValue?.GetType());

        position.height = lineSize;

        if (selector.ChangeCheck(position))
        {
            //var oldObj = property.managedReferenceValue;
            var newObj = selector.CreateInstance(property.managedReferenceValue);
            //ReflectionHelper.CopyFields(oldObj, newObj);
            property.managedReferenceValue = newObj;
            property.serializedObject.ApplyModifiedProperties();
        }
        else
        {
            position.y += lineSize;
            EditorGUI.PropertyField(position, property, true);
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, true) + EditorGUIUtility.singleLineHeight;
    }
}

public class DerivedTypeDropdown
{
    private readonly List<Type> subTypes;
    private readonly string[] subTypeNames;
    private int selectedIndex = -1;


    public Type SelectedType => subTypes.ElementAtOrDefault(Math.Max(0, selectedIndex));

    public DerivedTypeDropdown(Type targetType, Type currentType = null)
    {
        subTypes = new List<Type>();

        if (!targetType.IsAbstract)
        {
            subTypes.Add(targetType);
        }

        IEnumerable<Type> foundClasses = TypeCache.GetTypesDerivedFrom(targetType);
        subTypes.AddRange(foundClasses.Where(t => !t.ContainsGenericParameters).ToList());
        subTypeNames = subTypes.Select(x => x.Name).ToArray();
        selectedIndex = subTypes.IndexOf(currentType);
    }

    public void RefreshSelection(Type currentType)
    {
        selectedIndex = subTypes.IndexOf(currentType);
    }

    public bool ChangeCheck(Rect position)
    {
        int oldIndex = selectedIndex;
        int newIndex = EditorGUI.Popup(position, Math.Max(0, selectedIndex), subTypeNames);
        selectedIndex = newIndex;

        return oldIndex != newIndex;
    }

    public bool DrawLayout()
    {
        int oldIndex = selectedIndex;
        int newIndex = EditorGUILayout.Popup(Math.Max(0, selectedIndex), subTypeNames);
        selectedIndex = newIndex;
        return oldIndex != newIndex;
    }

    public object CreateInstance(object oldValue = null)
    {
        return Activator.CreateInstance(SelectedType);
    }
}