using UnityEditor;
using UnityEngine;
using System.Linq;
using System;


[CustomEditor(typeof(Item))]
public class ItemEditor : Editor
{
    private SerializedProperty _effects;

    private void OnEnable()
    {
        _effects = serializedObject.FindProperty("<Effects>k__BackingField");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawPropertiesExcluding(
            serializedObject,
            "<Effects>k__BackingField"
        );

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Effects", EditorStyles.boldLabel);

        for (int i = 0; i < _effects.arraySize; i++)
        {
            EditorGUILayout.PropertyField(
                _effects.GetArrayElementAtIndex(i)
            );
        }

        if (GUILayout.Button("Add Effect"))
        {
            ShowEffectMenu();
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void ShowEffectMenu()
    {
        GenericMenu menu = new GenericMenu();

        Type[] effectTypes = TypeCache
            .GetTypesDerivedFrom<ItemEffect>()
            .Where(type => !type.IsAbstract)
            .ToArray();

        foreach (Type effectType in effectTypes)
        {
            menu.AddItem(
                new GUIContent(effectType.Name),
                false,
                () => AddEffect(effectType)
            );
        }

        menu.ShowAsContext();
    }

    private void AddEffect(Type effectType)
    {
        Item item = (Item)target;

        ItemEffect effect = (ItemEffect)CreateInstance(effectType);
        effect.name = effectType.Name;

        AssetDatabase.AddObjectToAsset(effect, item);

        int index = _effects.arraySize;

        _effects.InsertArrayElementAtIndex(index);
        _effects.GetArrayElementAtIndex(index).objectReferenceValue = effect;

        serializedObject.ApplyModifiedProperties();

        EditorUtility.SetDirty(item);
        EditorUtility.SetDirty(effect);

        AssetDatabase.SaveAssets();
    }
}
