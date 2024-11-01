using UnityEngine;

public class RotationController : MonoBehaviour
{
    // Boolean variables to control the direction of rotation
    bool right = true; // Indicates if the object is rotating to the right
    bool left = false; // Indicates if the object is rotating to the left

    // Rotation limits
    public float rightLimit = 60f; // Right rotation limit in degrees
    public float leftLimit = 300f; // Left rotation limit in degrees (300 degrees to represent -60 degrees from 360)

    // Booleans to control the hook's state
    public bool shoot = false; // Indicates if the hook is being shot
    public bool pullBackEmpty = false; // Indicates if the hook is being pulled back without an object
    public bool pullBack = false; // Indicates if the hook is being pulled back with an object

    // Speeds for different states of the hook
    public float shootingSpeed = 150.0f; // Speed at which the hook is shot
    public float emptyPullBackSpeed = 175.0f; // Speed at which the hook is pulled back without an object
    public float pullBackSpeed = 0.0f; // Speed at which the hook is pulled back with an object

    // References to other game objects and components
    public Transform hook; // Reference to the hook transform
    public GameManager gameManager; // Reference to the GameManager script
    public Animator playerAnimator; // Reference to the player's animator

    void Update()
    {
        if (gameManager.totalSeconds > 0)
        {
            // Check if the left mouse button is pressed
            if (Input.GetMouseButtonDown(0))
            {
                // If the hook is not currently being shot
                if (!shoot)
                {
                    // Update the player animation to indicate movement
                    playerAnimator.SetBool("isstop", false);
                    // Stop the rotation
                    right = false;
                    left = false;
                    // Set shoot to true to start shooting the hook
                    shoot = true;
                    // Detach the hook from its parent object
                    hook.parent = null;
                }
            }
        }
        else
        {
            hook.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {
        if (gameManager.totalSeconds > 0)
        {
            // Control rotation to the right
            if (right)
            {
                // Rotate the object 2 degrees to the right
                transform.Rotate(0, 0, 2);

                // If the rotation exceeds the right limit and is within the 0 to 180 degree range
                if (transform.rotation.eulerAngles.z > rightLimit && transform.rotation.eulerAngles.z < 180)
                {
                    // Switch direction to left
                    left = true;
                    right = false;
                }
            }

            // Control rotation to the left
            if (left)
            {
                // Rotate the object 2 degrees to the left
                transform.Rotate(0, 0, -2);

                // If the rotation exceeds the left limit and is within the 180 to 360 degree range
                if (transform.rotation.eulerAngles.z < leftLimit && transform.rotation.eulerAngles.z > 180)
                {
                    // Switch direction to right
                    left = false;
                    right = true;
                }
            }

            // Control shooting the hook
            if (shoot)
            {
                // Apply velocity to the hook to move it downwards (relative to the current rotation)
                hook.gameObject.GetComponent<Rigidbody2D>().velocity = -transform.up * Time.deltaTime * shootingSpeed;
            }

            // Control pulling back the hook with an object
            if (pullBack)
            {
                // Apply velocity to the hook to move it upwards (relative to the current rotation)
                hook.gameObject.GetComponent<Rigidbody2D>().velocity = transform.up * Time.deltaTime * pullBackSpeed;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the hook collides with an object tagged as "hook"
        if (collision.gameObject.tag == "hook")
        {
            // Stop the player animation
            playerAnimator.SetBool("isstop", true);
            // Reset the shooting and pulling back states
            shoot = false;
            pullBack = false;
            // Resume rotation to the right
            right = true;
            left = false;
            // Stop the hook's movement
            hook.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            // Reattach the hook to its parent object
            hook.parent = transform;

            // Check if the hook has collected any objects
            if (hook.childCount > 1)
            {
                // Increase the player's money based on the collected object's value
                gameManager.IncreaseMoney(hook.GetChild(1).GetComponent<objects>().moneyValue);
                // Destroy the collected object
                Destroy(hook.GetChild(1).gameObject);
            }

            // Reset the pull back speed for an empty hook
            pullBackSpeed = emptyPullBackSpeed;
        }
    }
}
