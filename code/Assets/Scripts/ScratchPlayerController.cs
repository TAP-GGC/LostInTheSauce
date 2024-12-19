using UnityEngine;
// Controls player movement, jumping, and wall interactions in a 2D game
public class ScratchPlayerController : MonoBehaviour
{
    //Player movement variables
    public float speed;        //default speed value for player movement
    public float jump;        //default jump value while not on the wall
    float moveVelocity;        //i can't remember what this variable is for but if i figure it out later i'll comment it in herpaderp
    public float moveForce = 200f;          // Amount of force added to move the player left and right.
    public string horiztonal_P1 = "Horizontal_P1";
    
    
    
    //bool Player state variables
    public bool grounded;      //condition for if player is on the ground or not to prevent double jumping
    public bool onWall;        //condition for if player on the wall
    
    //objects
    public Rigidbody2D rgb2d = new Rigidbody2D();
    WallGrab wallgrab = new WallGrab(); //object reference to wallgrab script
    
    // Update is called once per frame
    void Update()
    {
        SoundEffects se = new SoundEffects(); //TAISANN'S EDIT ALLOWS USE OF SOUND EFFECTS
       
       
       
       
        //jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //if (grounded)
            {
                se.soundEffect("JumpSFX");
                rgb2d.linearVelocity = new Vector2(rgb2d.linearVelocity.x, jump);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (onWall)
            {
                se.soundEffect("JumpSFX");
                rgb2d.linearVelocity = new Vector2(rgb2d.linearVelocity.x, jump);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Floor"))
        {
            grounded = true;
        }

        if (col.gameObject.CompareTag("Wall"))
        {
            onWall = true;
        }
    }


    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Floor"))
        {
            grounded = false;
        }

        if (col.gameObject.CompareTag("Wall"))
        {
            onWall = false;
        }
    }
}
