using UnityEngine;
using UnityEngine.AI;

public class linedrawer : MonoBehaviour
{
    public Transform shopper; // The shopper GameObject
    public Transform targetShelf; // The destination shelf
    public LineRenderer lineRenderer; // LineRenderer component to draw the line
    public NavMeshAgent shopperAgent; // NavMeshAgent component on the shopper

    private void Start()
    {
        if (shopperAgent == null)
        {
            shopperAgent = shopper.GetComponent<NavMeshAgent>();
        }

        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        DrawPath();
    }

    private void DrawPath()
    {
        NavMeshPath path = new NavMeshPath();
        if (shopperAgent.CalculatePath(targetShelf.position, path) && path.status == NavMeshPathStatus.PathComplete)
        {
            // Ensure we have a valid path
            if (path != null && path.corners.Length > 0)
            {
                lineRenderer.positionCount = path.corners.Length;

                for (int i = 0; i < path.corners.Length; i++)
                {
                    Vector3 position = path.corners[i];
                    position.y = 87.1f; // Keep Y value at 0.1
                    lineRenderer.SetPosition(i, position);
                }
            }
            else
            {
                lineRenderer.positionCount = 0; // Clear the line if no path is found
            }
        }
        else
        {
            Debug.LogWarning("Path to shelf not valid: " + targetShelf.name);
            lineRenderer.positionCount = 0; // Clear the line if path calculation failed
        }
    }
}
