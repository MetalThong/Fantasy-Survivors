using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class StatEditor : EditorWindow
{
    private GameObject player;
    private StatComponent stat;

    [MenuItem("Tools/Stats Tool")]
    public static void ShowWindow()
    {
        GetWindow<StatEditor>("Stat Tool");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Player"))
        {
            player = GameObject.Find("Player");
        }

        if (player != null)
        {
            GUILayout.Label("Found: " + player.name);

            if (GUILayout.Button("Show stats"))
            {
                stat = player.GetComponent<PlayerEntity>().GetStatComponent();
            }

            if (stat != null)
            {
                GUILayout.Label("Max Health: " + stat.GetMaxHealth());
                GUILayout.Label("Current Health: " + stat.GetCurrentHealth());
                GUILayout.Label("Pickup Range: " + stat.GetPickupRange());
            }
        }
        else
        {
            GUILayout.Label("Player not found!");
        }
    }
}
