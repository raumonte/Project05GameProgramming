using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    private Vector3 directionToMove;
    private Vector3 targetPosition;
    public float moveSpeed;
    private void Start()
    {
        GameManager.instance.enemyPawn = this.gameObject;
        //This has code is being used to get the last position of the player to go straight twords it. 
        directionToMove = GameManager.instance.playerPawn.transform.position - transform.position;
        //
        directionToMove.Normalize();
        //Gives the variable targetPosition a value of the last location of the player.
        targetPosition = GameManager.instance.playerPawn.transform.position;
        // Calls the PlayerDied function from the event script to detroy any astroid in the area.
        

    }
    private void Update()
    {
        //
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

    }
    //In this funciton once the astroid gets destroyed it removes that astroid and checks the event handler.
    private void DestroySelf()
    {

    }
    //Whenever an object collides with the astroid it will begin to activate any code within the function.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.enemyDamage();
        }
    }

} 
