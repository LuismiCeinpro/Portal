using UnityEngine;
using UnityEditor;


public class PackagePostProcessor : AssetPostprocessor
{
    private static string[] _tags = { "NewTag" };
    private static LayerInfo[] _layers = { new LayerInfo() { index = 8, name = "Interactable" } };

    [MenuItem("Portal/Check for needed tags && layers")]
    private static void CheckTags()
    {
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        AddTags(tagManager);
        AddLayers(tagManager);
        tagManager.ApplyModifiedProperties();
    }

    private static void AddTags(SerializedObject tagManager)
    {
        SerializedProperty tags = tagManager.FindProperty("tags");
        foreach (string tagToAdd in _tags)
        {
            bool hasToAddTag = true;
            for (int i = 0; i < tags.arraySize; i++)
            {
                SerializedProperty t = tags.GetArrayElementAtIndex(i);
                if (t.stringValue == tagToAdd)
                {
                    hasToAddTag = false;
                    break;
                }
            }
            if (hasToAddTag)
            {
                tags.InsertArrayElementAtIndex(tags.arraySize);
                SerializedProperty newTagPropety = tags.GetArrayElementAtIndex(tags.arraySize - 1);
                newTagPropety.stringValue = tagToAdd;
            }
        }
    }
    
    private static void AddLayers(SerializedObject tagManager)
    {
        SerializedProperty layers = tagManager.FindProperty("layers");
        foreach (LayerInfo layerToAdd in _layers)
        {
            bool hasToAddTag = true;
            for (int i = 0; i < layers.arraySize; i++)
            {
                SerializedProperty l = layers.GetArrayElementAtIndex(i);
                if (l.stringValue.Equals(layerToAdd))
                {
                    hasToAddTag = false;
                    break;
                }
            }
            if (hasToAddTag)
            {
                SerializedProperty layerProperty = layers.GetArrayElementAtIndex(layerToAdd.index);
                if (layerProperty.stringValue != string.Empty && layerProperty.stringValue != layerToAdd.name) Debug.LogError($"Cannot add layer {layerToAdd.name} at index {layerToAdd.index}. {layerProperty.stringValue} was found in that position");
                else
                {
                    layerProperty.stringValue = layerToAdd.name;
                }
            }
        }
    }

    private struct LayerInfo
    {
        public int index;
        public string name;
    }
}
