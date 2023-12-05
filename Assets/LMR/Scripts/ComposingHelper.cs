using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Gameplay;
using Testing;

public class ComposingHelper : MonoBehaviour
{
    [MenuItem("Portal/Add inventory helper")]
    private static void AddInventoryHelper()
    {
        InventoryHelper prefab = Resources.Load<InventoryHelper>("prefab_inventory_helper");
        InventoryHelper instance = Instantiate(prefab);
        instance.name = "Inventory Helper";
    }

    [MenuItem("Portal/Add start position")]
    private static void AddStartPosition()
    {
        TestingStartPosition prefab = Resources.Load<TestingStartPosition>("prefab_test_start_position");
        TestingStartPosition instance = Instantiate(prefab);
        instance.name = "Player Start Position";
    }
}
