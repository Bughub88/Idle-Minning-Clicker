using UnityEngine;

public class Hook : MonoBehaviour
{
    public LineRenderer lineRenderer; // Reference to the LineRenderer component for drawing the rope
    public Transform roller; // Reference to the roller transform
    public RotationController rotationController; // Reference to the RotationController script
    public GameObject startpoint, bombEffect; // Reference to the start point of the rope
    public GameManager gameManager; // Reference to the GameManager script

    private void Start()
    {
        // Find the "roller" game object and get its RotationController component
        rotationController = GameObject.Find("roller").GetComponent<RotationController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the hook collides with an object tagged as "Objects"
        if (collision.gameObject.tag == "Objects")
        {
            // Start pulling back the hook
            rotationController.pullBack = true;
            rotationController.shoot = false;

            // Get the pull back speed from the collided object
            float pullBackSpeed = collision.gameObject.GetComponent<objects>().pullBackSpeed;
            rotationController.pullBackSpeed = pullBackSpeed;

            // Set a default pull back speed if the obtained speed is less than or equal to zero
            if (rotationController.pullBackSpeed <= 0)
            {
                rotationController.pullBackSpeed = 20.0f;
            }

            // Disable the polygon collider of the collided object
            collision.gameObject.GetComponent<PolygonCollider2D>().enabled = false;

            // Set the collided object's parent to the hook
            collision.gameObject.transform.parent = transform;
            if (collision.gameObject.GetComponent<objects>().isBig)
            {
                collision.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y - 0.6f, transform.position.z);
            }
            else
            {
                collision.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y - 0.4f, transform.position.z);
            }
        }
        // Check if the hook collides with an object tagged as "Border"
        else if (collision.gameObject.tag == "Border")
        {
            rotationController.shoot = false;
            // Start pulling back the hook
            rotationController.pullBack = true;// Set a default pull back speed if the obtained speed is less than or equal to zero
            if (rotationController.pullBackSpeed <= 0)
            {
                rotationController.pullBackSpeed = 175.0f;
            }
        }
        else if(collision.gameObject.tag == "TNT") 
        {
            rotationController.shoot = false;
            // Start pulling back the hook
            rotationController.pullBack = true;// Set a default pull back speed if the obtained speed is less than or equal to zero
            if (rotationController.pullBackSpeed <= 0)
            {
                rotationController.pullBackSpeed = 175.0f;
            }
            bombEffect.SetActive(false);
            bombEffect.SetActive(true);
            collision.gameObject.SetActive(false);
            gameManager.IncreaseMoney(-200);
        }
    }

    void Update()
    {
        // Update the rope drawing every frame
        DrawLine();
    }

    void DrawLine()
    {
        // Set the line renderer's positions to draw the rope from the start point to the roller
        lineRenderer.SetPosition(2, startpoint.transform.position); // Changed to 0 for correct indexing
        lineRenderer.SetPosition(1, roller.position);
    }
}
