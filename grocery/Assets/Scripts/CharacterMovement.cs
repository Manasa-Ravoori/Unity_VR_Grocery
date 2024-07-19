using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust this to control the movement speed

    void Update()
    {
        // Get the current position of the character
        Vector3 pos = transform.position;

        // Check for arrow key inputs and move the character accordingly
        if (Input.GetKey(KeyCode.UpArrow))
        {
            pos.y += moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            pos.y -= moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            pos.x -= moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            pos.x += moveSpeed * Time.deltaTime;
        }

        // Update the position of the character
        transform.position = pos;
    }
}

