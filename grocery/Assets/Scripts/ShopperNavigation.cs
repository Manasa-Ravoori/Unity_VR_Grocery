using UnityEngine;
using TMPro;
using UnityEngine.AI; // Import the NavMesh namespace
using System.Collections.Generic;

public class ShopperNavigation : MonoBehaviour
{
    public GameObject shopper; // The shopper GameObject
    public TMP_Dropdown itemDropdown; // The TMP Dropdown UI element
    public Transform shelvesParent; // Parent transform containing all shelves
    public LineRenderer lineRenderer; // LineRenderer component to draw the line
    public Material targetShelfMaterial; // New material to apply to the target shelf
    public NavMeshAgent shopperAgent; // NavMeshAgent component on the shopper

    private Dictionary<string, string> itemToShelfMap;
    private Dictionary<string, Dictionary<Transform, Material>> originalShelfMaterials; // To store original materials of shelf children

    private void Start()
    {
        // Initialize the item to shelf mapping
        itemToShelfMap = new Dictionary<string, string>
        {
            // { "Fruits", "S104" },
            // { "No Name Bread", "S130" },
            // { "Dairyland Milk", "S110" }
            { "Icecream", "S100" },
            { "Apples and Bananas", "S101" },
            { "", "S102" },
            { "Stew", "S103" },
            { "Watermelon", "S104" },
            { " ", "S105" },
            { "Chips Bag", "S106" },
            { "Alcohol", "S107" },
            { "Cola", "S108" },
            { "Beer", "S109" },
            { "SweetPeppers", "S110" },
            { "Milk", "S111" },
            { "Tomatos and Eggplant", "S112" },
            { "Stew (1)", "S113" },
            { "Soda", "S114" },
            { "Chips", "S115" },
            { "Yogurt", "S116" },
            { "Coffee", "S117" },
            { "Tomato Paste", "S118" },
            { "Meat", "S119" },
            { "Toast", "S120" },
            { "Bread", "S121" },
            { "Muffin", "S122" },
            { "Lentils", "S123" },
            { "Donut", "S124" },
            { "Pasta Sauce", "S125" },
            { "Soap", "S126" },
            { "Cheese", "S127" },
            { "Cleaning Solution", "S128" },
            { "Wine Glasses", "S129" },
            { "Plates", "S130" },
            { "Martini Glasses", "S131" },
            { "Mugs", "S132" }
        };

        //UpdateDropdown(options);

        // Initialize the original materials dictionary
        originalShelfMaterials = new Dictionary<string, Dictionary<Transform, Material>>();

        // Store the original materials of the shelves' children
        foreach (Transform row in shelvesParent)
        {
            foreach (Transform shelf in row)
            {
                Dictionary<Transform, Material> shelfMaterials = new Dictionary<Transform, Material>();
                foreach (Transform child in shelf)
                {
                    Renderer renderer = child.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        shelfMaterials[child] = renderer.material;
                    }
                }
                originalShelfMaterials[shelf.name] = shelfMaterials;
            }
        }

        // Add listener for when the dropdown value changes
        itemDropdown.onValueChanged.AddListener(OnItemSelected);
        lineRenderer.positionCount = 0; // Initially no line segments
    }

    private void OnItemSelected(int index)
    {
        // Get the selected item name from the dropdown
        string selectedItem = itemDropdown.options[index].text;
        ResetShelfMaterials();

        // Find the corresponding shelf by item name
        if (itemToShelfMap.TryGetValue(selectedItem, out string shelfName))
        {
            Transform selectedShelfPlaceholder = FindShelfPlaceholderByName(shelfName);

            if (selectedShelfPlaceholder != null)
            {
                // Move the shopper to the selected shelf placeholder
                MoveToShelf(selectedShelfPlaceholder);

                // Change the material of the selected shelf's children
                ChangeShelfMaterial(selectedShelfPlaceholder.parent, targetShelfMaterial);
            }
            else
            {
                Debug.LogWarning("Shelf placeholder not found: " + shelfName);
            }
        }
        else
        {
            Debug.LogWarning("Item not found in map: " + selectedItem);
        }
    }

    private Transform FindShelfPlaceholderByName(string shelfName)
    {
        foreach (Transform row in shelvesParent)
        {
            Transform shelf = row.Find(shelfName);
            if (shelf != null)
            {
                Transform placeholder = shelf.Find("placeholder");
                if (placeholder != null)
                {
                    return placeholder;
                }
            }
        }
        return null;
    }

    private void MoveToShelf(Transform targetShelfPlaceholder)
    {
        NavMeshPath path = new NavMeshPath();
        if (shopperAgent.CalculatePath(targetShelfPlaceholder.position, path) && path.status == NavMeshPathStatus.PathComplete)
        {
            // Draw the path
            DrawPath(path);

            // Set the shopper agent's path
            shopperAgent.SetPath(path);
        }
        else
        {
            Debug.LogWarning("Path to shelf placeholder not valid: " + targetShelfPlaceholder.name);
        }
    }

    private void DrawPath(NavMeshPath path)
    {
        // Ensure we have a valid path
        if (path != null && path.corners.Length > 0)
        {
            lineRenderer.positionCount = path.corners.Length;

            for (int i = 0; i < path.corners.Length; i++)
            {
                Vector3 position = path.corners[i];
                position.y = 0.1f; // Keep Y value at 0.1
                lineRenderer.SetPosition(i, position);
                Debug.Log("Line Drawn. Corners were " + path.corners.Length);
            }
        }
        else
        {
            lineRenderer.positionCount = 0; // Clear the line if no path is found
        }
    }

    private void ChangeShelfMaterial(Transform shelf, Material newMaterial)
    {
        foreach (Transform child in shelf)
        {
            Renderer childRenderer = child.GetComponent<Renderer>();

            if (childRenderer != null)
            {
                childRenderer.material = newMaterial;
            }
            else
            {
                Debug.LogWarning("Renderer not found on shelf child: " + child.name);
            }
        }
    }

    public void ResetShelfMaterials()
    {
        // Reset all shelves' children to their original materials
        foreach (var shelfEntry in originalShelfMaterials)
        {
            string shelfName = shelfEntry.Key;
            Dictionary<Transform, Material> shelfMaterials = shelfEntry.Value;

            Transform shelf = FindShelfByName(shelfName);
            if (shelf != null)
            {
                foreach (var materialEntry in shelfMaterials)
                {
                    Transform child = materialEntry.Key;
                    Material originalMaterial = materialEntry.Value;

                    Renderer renderer = child.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        renderer.material = originalMaterial;
                    }
                }
            }
        }
    }

    private Transform FindShelfByName(string shelfName)
    {
        foreach (Transform row in shelvesParent)
        {
            Transform shelf = row.Find(shelfName);
            if (shelf != null)
            {
                return shelf;
            }
        }
        return null;
    }
}

//----------------------------------------------------------------------------------------------------------------------------------------