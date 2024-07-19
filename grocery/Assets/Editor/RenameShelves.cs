using UnityEngine;
using UnityEditor;

public class RenameShelves : EditorWindow
{
    [MenuItem("Tools/Rename Shelves")]
    public static void ShowWindow()
    {
        GetWindow<RenameShelves>("Rename Shelves");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Rename Shelves"))
        {
            RenameShelfObjects();
        }
    }

    private void RenameShelfObjects()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        int counter = 100;

        foreach (GameObject obj in allObjects)
        {
            if (obj.name.StartsWith("Shelf ("))
            {
                obj.name = "S" + counter;
                counter++;
            }

            if (counter > 200)
            {
                Debug.LogWarning("More than 100 shelves found. Renaming stopped at S200.");
                break;
            }
        }

        Debug.Log("Shelf renaming complete.");
    }
}
