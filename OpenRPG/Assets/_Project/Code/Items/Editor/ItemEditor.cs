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
        _effects = serializedObject.FindProperty("_effects");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawPropertiesExcluding(
            serializedObject,
            "_effects"
        );

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Effects", EditorStyles.boldLabel);

        for (int i = 0; i < _effects.arraySize; i++)
        {
            SerializedProperty effect =
                _effects.GetArrayElementAtIndex(i);

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(effect);

            if (GUILayout.Button("×", GUILayout.Width(20)))
            {
                UnityEngine.Object effectObject =
                    effect.objectReferenceValue;
                
                AssetDatabase.RemoveObjectFromAsset(effectObject);
                
                _effects.DeleteArrayElementAtIndex(i);
                
                AssetDatabase.SaveAssets();
            }

            EditorGUILayout.EndHorizontal();
            
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
