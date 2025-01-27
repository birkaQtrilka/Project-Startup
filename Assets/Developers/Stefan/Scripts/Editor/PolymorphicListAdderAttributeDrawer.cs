using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(PolymorphicListAdderAttribute))]
public class PolymorphicListAdderAttributeDrawer : PropertyDrawer
{
    string _currentlySelectedType;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        property.NextVisible(true);
        return EditorGUI.GetPropertyHeight(property);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var copy = property.Copy();
        property.NextVisible(true);
        if (GUILayout.Button("Add New"))
        {
            DrawMenu(property);
        }
        EditorGUI.PropertyField(position, property);
        
        property = copy;

        property.serializedObject.ApplyModifiedProperties();
    }

    void DrawMenu(SerializedProperty prop)
    {
        var abstractClass = typeof(BookCondition);

        var types = Assembly.GetAssembly(abstractClass).GetTypes()
            .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(abstractClass));

        GenericMenu menu = new();
        foreach (var type in types)
            AddMenuItem(menu, type, prop);

        menu.ShowAsContext();

    }

    void AddMenuItem(GenericMenu menu, Type type, SerializedProperty prop)
    {
        // the menu item is marked as selected if it matches the current type name
        menu.AddItem
        (
            new GUIContent(type.Name),
            type.Name == _currentlySelectedType,
            OnMenuButtonPress,
            new MenuData(type, prop)
        );
    }

    void OnMenuButtonPress(object menuDataObj)
    {
        MenuData data = menuDataObj as MenuData;
        data.Property.serializedObject.Update();

        //to know what item was selected when creating another menu
        _currentlySelectedType = data.Type.Name;
        SerializedProperty arrayProp = data.Property;

        int newIndex = arrayProp.arraySize;
        arrayProp.InsertArrayElementAtIndex(newIndex);

        SerializedProperty newElement = arrayProp.GetArrayElementAtIndex(newIndex);

        object newStat = Activator.CreateInstance(data.Type);

        newElement.managedReferenceValue = newStat;
        arrayProp.serializedObject.ApplyModifiedProperties();

    }

    class MenuData
    {
        public Type Type;
        public SerializedProperty Property;

        public MenuData(Type type, SerializedProperty statsProperty)
        {
            this.Type = type;
            this.Property = statsProperty;
        }
    }
}
