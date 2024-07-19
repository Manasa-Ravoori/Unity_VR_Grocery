using UnityEngine;

public class PlacePrefabOnShelf : MonoBehaviour
{
    public GameObject prefabToPlace;  // The prefab to place (e.g., Stack of Meat)
    public Transform shelfTransform;  // The shelf's transform (e.g., S128)
    public Transform anchorPoint;     // The designated anchor point on the shelf

    void Start()
    {
        Debug.Log("Script Start() method called");

        if (prefabToPlace == null)
        {
            Debug.LogError("Prefab not set!");
            return;
        }

        if (shelfTransform == null)
        {
            Debug.LogError("Shelf transform not set!");
            return;
        }

        if (anchorPoint == null)
        {
            Debug.LogError("Anchor point not set!");
            return;
        }

        Debug.Log("Instantiating prefab...");
        GameObject placedObject = Instantiate(prefabToPlace, anchorPoint.position, anchorPoint.rotation);

        if (placedObject != null)
        {
            Debug.Log("Prefab instantiated successfully");
            placedObject.transform.SetParent(anchorPoint);

            // Reset the local position, rotation, and scale to align with the anchor point
            placedObject.transform.localPosition = Vector3.zero;
            placedObject.transform.localRotation = Quaternion.identity;
            placedObject.transform.localScale = Vector3.one;

            Debug.Log($"Instantiated Object Local Position: {placedObject.transform.localPosition}, Local Rotation: {placedObject.transform.localRotation}");
        }
        else
        {
            Debug.LogError("Failed to instantiate prefab");
        }
    }

    void OnDrawGizmos()
    {
        if (anchorPoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(anchorPoint.position, new Vector3(0.1f, 0.1f, 0.1f));  // Adjust the size as needed
        }
    }
}
