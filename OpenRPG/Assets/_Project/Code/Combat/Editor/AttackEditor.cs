using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AttackDefinition))]
public class AttackEditor : Editor
{
    private SerializedProperty _behavior;

    private void OnEnable()
    {
        _behavior = serializedObject.FindProperty("_behavior");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawPropertiesExcluding(
            serializedObject,
            "_behavior"
        );

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Behavior", EditorStyles.boldLabel);

        if (_behavior.objectReferenceValue != null)
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(
                _behavior,
                GUIContent.none
            );

            if (GUILayout.Button("×", GUILayout.Width(20)))
            {
                RemoveBehavior();
            }

            EditorGUILayout.EndHorizontal();
        }
        else
        {
            if (GUILayout.Button("Add Behavior"))
            {
                ShowBehaviorMenu();
            }
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void ShowBehaviorMenu()
    {
        GenericMenu menu = new GenericMenu();

        Type[] behaviorTypes = TypeCache
            .GetTypesDerivedFrom<AttackBehavior>()
            .Where(type =>
                !type.IsAbstract &&
                !type.ContainsGenericParameters)
            .ToArray();

        foreach (Type behaviorType in behaviorTypes)
        {
            menu.AddItem(
                new GUIContent(behaviorType.Name),
                false,
                () => AddBehavior(behaviorType)
            );
        }

        if (behaviorTypes.Length == 0)
        {
            menu.AddDisabledItem(new GUIContent("No available behaviors"));
        }

        menu.ShowAsContext();
    }

    private void AddBehavior(Type behaviorType)
    {
        AttackDefinition definition = (AttackDefinition)target;

        AttackBehavior behavior = (AttackBehavior)CreateInstance(behaviorType);

        behavior.name = behaviorType.Name;

        Undo.RegisterCreatedObjectUndo(behavior, "Create Attack Behavior"
        );

        AssetDatabase.AddObjectToAsset(behavior, definition);

        serializedObject.Update();
        _behavior.objectReferenceValue = behavior;
        serializedObject.ApplyModifiedProperties();

        EditorUtility.SetDirty(definition);
        EditorUtility.SetDirty(behavior);

        AssetDatabase.SaveAssets();
        AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(definition)
        );
    }

    private void RemoveBehavior()
    {
        AttackBehavior behavior =
            _behavior.objectReferenceValue as AttackBehavior;

        if (behavior == null)
            return;

        AttackDefinition definition = (AttackDefinition)target;

        Undo.RecordObject(definition, "Remove Attack Behavior");

        _behavior.objectReferenceValue = null;
        serializedObject.ApplyModifiedProperties();

        AssetDatabase.RemoveObjectFromAsset(behavior);

        EditorUtility.SetDirty(definition);
        AssetDatabase.SaveAssets();
        AssetDatabase.ImportAsset(
            AssetDatabase.GetAssetPath(definition)
        );
    }
}
