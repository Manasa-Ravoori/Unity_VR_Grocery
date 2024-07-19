using UnityEngine;
using UnityEngine.AI;

public class cleanup : MonoBehaviour
{
    void Start()
    {
        // Remove all baked NavMesh data
        NavMesh.RemoveAllNavMeshData();
    }
}
