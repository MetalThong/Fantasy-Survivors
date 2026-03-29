using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GridBrushBase;


public class AugmentEditor : EditorWindow
{
    private GameObject augmentScreen;
    private AugmentManager augmentManager;

    [MenuItem("Tools/Upgrade Editor Tool")]
    public static void ShowWindow()
    {
        GetWindow<AugmentEditor>("Augment Tool");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Find Augment Manager"))
        {
            augmentScreen = GameObject.Find("AugmentManager");
        }

        if (augmentScreen != null)
        {
            GUILayout.Label("Found: " + augmentScreen.name);

            if (GUILayout.Button("Show"))
            {
                augmentScreen.SetActive(true);
            }

            if (GUILayout.Button("Close"))
            {
                augmentScreen.SetActive(false);
            }

            if(GUILayout.Button("Generate Augments"))
            {
                augmentManager = Selection.activeObject.GetComponent<AugmentManager>();
                augmentManager.ShowAugments();
            }

        }
        else
        {
            GUILayout.Label("UpgradeScreen not found!");
        }
    }
}
