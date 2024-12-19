using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for handling 2D physics-based movement and collision
public class PhysicsObject : MonoBehaviour {

    // Base class for handling 2D physics-based movement and collision
    public float minGroundNormalY = .65f;
        
    // Multiplier for gravity; allows customization of how gravity affects the object
    public float gravityModifier = 1f;

    // Target velocity for the object, set by derived classes
    protected Vector2 targetVelocity;
    // Whether the object is currently grounded
    protected bool grounded;
    // Normal vector of the surface the object is currently grounded on
    protected Vector2 groundNormal;
    // Rigidbody2D component for physics simulation
    protected Rigidbody2D rb2d;
    // Current velocity of the object
    protected Vector2 velocity;
    // Contact filter to determine which objects the object interacts with
    protected ContactFilter2D contactFilter;
    // Buffer to store results of collision detection
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    // List version of hitBuffer for easier processing
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D> (16);

    // Minimum distance for movement to be considered; avoids small unnecessary movements
    protected const float minMoveDistance = 0.001f;
    // Small buffer to prevent penetration into colliders
    protected const float shellRadius = 0.01f;

    // Called when the object is enabled; initializes the Rigidbody2D reference
    void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D> ();
    }
    // Called at the start of the game; configures the contact filter
    void Start () 
    {
        contactFilter.useTriggers = false;    // Ignore trigger colliders
        contactFilter.SetLayerMask (Physics2D.GetLayerCollisionMask (gameObject.layer));
        contactFilter.useLayerMask = true;
    }
    // Called every frame to compute movement; left for derived classes to implement
    void Update () 
    {
        targetVelocity = Vector2.zero;    // Reset target velocity
        ComputeVelocity ();              // Custom movement logic defined by derived classes
    }
    // Method intended to be overridden in derived classes for custom movement logic
    protected virtual void ComputeVelocity()
    {
    
    }
    // Called at a fixed time step for consistent physics updates
    void FixedUpdate()
    {
        // Apply gravity to the object's velocity
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        
        // Set horizontal velocity based on the target velocity
        velocity.x = targetVelocity.x;
       
        // Reset grounded state; will be updated during collision checks
        grounded = false;

        // Calculate the total movement based on the velocity
        Vector2 deltaPosition = velocity * Time.deltaTime;

        // Calculate movement along the ground (perpendicular to the surface normal)
        Vector2 moveAlongGround = new Vector2 (groundNormal.y, -groundNormal.x);
       
        // Horizontal movement
        Vector2 move = moveAlongGround * deltaPosition.x;
        Movement (move, false);

        // Vertical movement
        move = Vector2.up * deltaPosition.y;
        Movement (move, true);
    }
    // Handles movement and collision resolution for the object
    void Movement(Vector2 move, bool yMovement)
    {
        // Calculate the distance to move
        float distance = move.magnitude;

        if (distance > minMoveDistance)    // Only proceed if the movement distance is significant
        {
            // Perform a cast to detect potential collisions along the movement path
            int count = rb2d.Cast (move, contactFilter, hitBuffer, distance + shellRadius);
            // Clear the hit buffer list and populate it with valid results
            hitBufferList.Clear ();
            for (int i = 0; i < count; i++) {
                hitBufferList.Add (hitBuffer [i]);
            }
            // Process each collision
            for (int i = 0; i < hitBufferList.Count; i++) 
            {
                Vector2 currentNormal = hitBufferList [i].normal;
                // Check if the object is grounded based on the surface normal
                if (currentNormal.y > minGroundNormalY) 
                {
                    grounded = true;    // Mark the object as grounded
                    if (yMovement) 
                    {
                        // Align ground normal if the collision is vertical
                        groundNormal = currentNormal;
                        currentNormal.x = 0;    // Ignore horizontal component for vertical movement
                    }
                }
                // Adjust velocity to prevent penetration into colliders
                float projection = Vector2.Dot (velocity, currentNormal);
                if (projection < 0) 
                {
                    velocity = velocity - projection * currentNormal;
                }
                // Adjust movement distance based on collision distance
                float modifiedDistance = hitBufferList [i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }


        }
        // Update the object's position based on the resolved movement
        rb2d.position = rb2d.position + move.normalized * distance;
    }

}
