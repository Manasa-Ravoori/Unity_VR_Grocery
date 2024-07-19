using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class DropdownManager : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public NavMeshAgent agent;
    public LineRenderer pathRenderer; // Assign LineRenderer in the Inspector

    private Dictionary<string, string> itemToShelfMap;

    void Start()
    {
        if (dropdown == null)
        {
            Debug.LogError("TMP_Dropdown component not assigned in the inspector.");
            return;
        }

        // Map items to their corresponding shelves
        itemToShelfMap = new Dictionary<string, string>
        {
            {"Fruits", "S101"},
            {"No Name Bread", "S110"},
            {"Dairyland Milk", "S103"},
            // Add other mappings here
        };

        // Add listener to the dropdown
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    void OnDropdownValueChanged(int index)
    {
        string selectedItem = dropdown.options[index].text;

        if (itemToShelfMap.TryGetValue(selectedItem, out string shelfName))
        {
            GameObject shelf = GameObject.Find(shelfName);
            if (shelf != null)
            {
                // Calculate path to shelf
                NavMeshPath path = new NavMeshPath();
                agent.CalculatePath(shelf.transform.position, path);

                // Draw path using LineRenderer
                DrawPath(path);

                Debug.Log("Path to " + selectedItem + " is visualized.");
            }
            else
            {
                Debug.LogError("Shelf not found: " + shelfName);
            }
        }
        else
        {
            Debug.LogError("Item not mapped: " + selectedItem);
        }
    }

    void DrawPath(NavMeshPath path)
    {
        if (pathRenderer != null)
        {
            Debug.Log("Drawing path with " + path.corners.Length + " corners.");

            // Set positions for LineRenderer
            pathRenderer.positionCount = path.corners.Length;
            pathRenderer.startColor = Color.red;
            pathRenderer.endColor = Color.red;
            pathRenderer.startWidth = 0.1f;
            pathRenderer.endWidth = 0.1f;

            for (int i = 0; i < path.corners.Length; i++)
            {
                pathRenderer.SetPosition(i, path.corners[i] + Vector3.up * 0.1f); // Offset slightly above ground
                Debug.Log("Corner " + i + ": " + path.corners[i]);
            }
        }
        else
        {
            Debug.LogError("LineRenderer not assigned.");
        }
    }

}

