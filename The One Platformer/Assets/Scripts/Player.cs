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
        float xMovement = Input.GetAxis("Horizontal")* speed * Time.deltaTime;
        playerRigidbody.velocity = new Vector3(xMovement, playerRigidbody.velocity.y);
        if (Input.GetKeyDown(KeyCode.F))
        {
            Attack();
        }
        if (playerRigidbody.velocity.x != 0)
        {
            playerAnimation.Play("PlayerWalk");
        }
        else
        {
            playerAnimation.Play("PlayerIdle");
        }
        playerSprite.flipX = playerRigidbody.velocity.x < 0;

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
        if (Input.GetKey(KeyCode.LeftShift) && IsSprinting == false)
        {
            IsSprinting = true;

            speed *= 2;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && IsSprinting == true)
        {
            IsSprinting = false;
            speed /= 2;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Out of Bounds"))
        {
            GameManager.instance.playerDeath();
            Debug.Log("Scream in pain");
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }
    public void Attack()
    {
        //This helps the code play the attack animation.
        playerAnimation.SetTrigger("Attack");
        //Then it will detect the enemy depending on the range of the attack
        //Then it would damage the enemy itself.
    }
    void Jumping()
    {
        currentSetJumps--;
        playerRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
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
