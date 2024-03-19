using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Choice))]
public class ChoiceEditor : Editor
{
    SerializedProperty choicesProperty;

    private void OnEnable()
    {
        // 'choices' is de veldnaam in je MonoBehaviour/ScriptableObject waar je Choices opslaat
        choicesProperty = serializedObject.FindProperty("choices");
    }
    public override void OnInspectorGUI()
    {
        // Choice choice = (Choice)target;

        DrawDefaultInspector(); //hiermee laat je default alles zien wat niet verborgen moet zijn
        // Toon de 'buttons' lijst alleen als 'EnableChoices' true is
        // if (choice.EnableChoices)
        // {
        //     SerializedProperty buttonsProperty = serializedObject.FindProperty("buttons");
        //     EditorGUILayout.PropertyField(buttonsProperty, new GUIContent("Buttons"), true);
        // }
    }
}
