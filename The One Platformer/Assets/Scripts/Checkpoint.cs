using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Once the player collides with the checkpoint it will change the sprite and set the new respawn point.
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.checkpoint = transform.position;
            spriteRenderer.sprite = newSprite;
            Debug.Log("Screams again");

        }

    }
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
