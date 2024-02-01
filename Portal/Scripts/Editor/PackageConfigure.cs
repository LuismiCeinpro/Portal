using UnityEngine;
using UnityEditor;


public class PackageConfigure
{
    private static string[] _tags = { "BlueGem","OrangeGem","screen","CinemachineTarget","draggable","Gembag","HexagonTile","PinkGem","PurpleGem", "Esfera", "Desactivador", "Negativo", "Base", "Footsteps Grass", "Footsteps Gravel", "Footsteps Metal", "Footsteps Rock", "Footsteps Sand", "Footsteps Snow", "Footsteps Tile", "Footsteps Water", "Footsteps Wood" };
    private static LayerInfo[] _layers = { new LayerInfo(3, "PlacedGem"), new LayerInfo(6, "Interactable"), new LayerInfo(26, "Terrain"), new LayerInfo(27, "Stencil1"), new LayerInfo(28, "Stencil2"), new LayerInfo(29, "TransparentFX2"), new LayerInfo(30, "BolaDia"), new LayerInfo(31, "BolaNoche") };

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
        public LayerInfo(int index, string name)
        {
            this.index = index;
            this.name = name;
        }
        public int index;
        public string name;
    }
}
