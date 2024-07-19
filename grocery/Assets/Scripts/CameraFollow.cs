using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;       // The player's Transform
    public Vector3 offset;         // Offset from the player's position

    private void Start()
    {
        // Optionally, set the initial offset if needed
        offset = transform.position - player.position;
    }

    private void LateUpdate()
    {
        // Set the camera's position to follow the player while maintaining the offset
        Vector3 newPosition = player.position + offset;
        transform.position = newPosition;

        // Optionally, keep the camera's rotation fixed
        // Uncomment the following line if you want to keep the camera's rotation fixed
        // transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}

