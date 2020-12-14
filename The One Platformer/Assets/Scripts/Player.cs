using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer playerSprite; //This given variable is to set it as the SpriteRender.
    private Rigidbody2D playerRigidbody; //This Variable is to set the players rigid body.
    private Animator playerAnimation;    //This variable is made to be attached to the animator that is with the player.
    public int setMaxJumps;              //This is a set number of Maximum Jumps by editing in editor.
    public int currentSetJumps;          //This will be used to set the maximum to ristrict the amount of jumps to the player.
    public float heightOfCharacter = 1.8f;      //This will be used to get the exact hight of the player sprite.
    public float speed = 3f;             //This is to set the tright amount for the speed of the player.
    public bool IsSprinting;
    public float jumpForce = 5f;
    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();     //When the game begins the variable for the rigid body will be set to the componenet that should be set to the player.
        playerSprite = GetComponent<SpriteRenderer>();     //When the game begins it should set the variable for the sprite renderer to the sprite renderer on the player.
        playerAnimation = GetComponent<Animator>();        //When the game begins the variable should be set to the component of the animator.
        GameManager.instance.playerPawn = this.gameObject; //Player pawn from the game manager should be set to this game object.
        IsSprinting = false;
        currentSetJumps = setMaxJumps;
    }

    // Update is called once per frame
    void Update()
    {
        //This will get the movement of the player and move on the x-axis when the button for the horizontal axis is pressed.
        float xMovement = Input.GetAxis("Horizontal")* speed * Time.deltaTime;
        //This will get the rigidbody of the player and sets up the velocity of the playerr.
        playerRigidbody.velocity = new Vector3(xMovement, playerRigidbody.velocity.y);
        if (Input.GetKeyDown(KeyCode.F))
        {
            Attack();
        }
        // When the player is moving on any axis it will play the animation for walking.
        if (playerRigidbody.velocity.x != 0)
        {
            playerAnimation.Play("PlayerWalk");
        }
        //if the player doesn't move on the x axis or at all it will play its idle animation.
        else
        {
            playerAnimation.Play("PlayerIdle");
        }
        //Whenever the player moves it will flip it depending on the axis it is walking in.
        playerSprite.flipX = playerRigidbody.velocity.x < 0;
        //When the player presses the Jump button it will give the player the choice to move on the y-axis aka jumping.
        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded())
            {
                currentSetJumps = setMaxJumps;
                Debug.Log("Is Grounded");
            }
            if (currentSetJumps > 0)
            {
                Jumping();
            }
        }
        //It will check if sprinting is false when pressing the left shift button. Once it is being pressed it will double the speed than normal.
        if (Input.GetKey(KeyCode.LeftShift) && IsSprinting == false)
        {
            IsSprinting = true;

            speed *= 2;
        }
        //Once it sees that it has been set true it will set it back to false so it can return the amount of speed back to it was.
        else if (Input.GetKeyUp(KeyCode.LeftShift) && IsSprinting == true)
        {
            IsSprinting = false;
            speed /= 2;
        }

    }
    //When the player exits the play area they will activate their death function.
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Out of Bounds"))
        {
            GameManager.instance.playerDeath();
            Debug.Log("Scream in pain");
        }
    }
   public void Attack()
    {
        //This helps the code play the attack animation.
        playerAnimation.SetTrigger("Attack");
        //Then it will detect the enemy depending on the range of the attack
        //Then it would damage the enemy itself.
    }
   //Whenever the player jumps it will add force to their jump and subtract the amount of set jumps. 
    void Jumping()
    {
        currentSetJumps--;
        playerRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    //It will check if it is grounded.
    public bool IsGrounded()
    {
        //// Get the layers for the Out of Bounds box and the Player
        //int layerMask = 1 << LayerMask.NameToLayer("Out of Bounds") | 1 << LayerMask.NameToLayer("Player");

        //// Invert the layer mask to get every layer except for those two.
        //layerMask = ~layerMask;

        // Instead, we should just be paying attention to the one layer we want, instead of ignoring every other one.
        int layerMask = 1 << LayerMask.NameToLayer("Ground");

        // Shoot a raycast just below our characters feet only viewing things within our layer mask (not the player or bounds box).
        RaycastHit2D hitinfo = Physics2D.Raycast(transform.position, Vector2.down, (heightOfCharacter / 1f), layerMask);

        return (hitinfo.collider != null);
    }
    
}
