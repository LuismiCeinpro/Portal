using Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(PlayerFootstepsScriptableObject))]
public class PlayerFootstepsEditor : Editor
{
    private SerializedProperty _tagProperty;
    private SerializedProperty _walkFootstepsProperty;
    private SerializedProperty _runFootstepsProperty;

    private void OnEnable()
    {
        _tagProperty = serializedObject.FindProperty("_tag");
        _walkFootstepsProperty = serializedObject.FindProperty("_walkFootsteps");
        _runFootstepsProperty = serializedObject.FindProperty("_runFootsteps");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        string[] tags = UnityEditorInternal.InternalEditorUtility.tags;
        int selectedIndex = System.Array.IndexOf(tags, _tagProperty.stringValue);
        if (selectedIndex < 0) selectedIndex = 0;
        selectedIndex = EditorGUILayout.Popup("Footsteps tag", selectedIndex, tags);
        _tagProperty.stringValue = tags[selectedIndex];
        EditorGUILayout.PropertyField(_walkFootstepsProperty);
        EditorGUILayout.PropertyField(_runFootstepsProperty);
        serializedObject.ApplyModifiedProperties();
    }
}
