using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject playerPf;
    public GameObject playerPawn;
    public GameObject enemyPawn;
    public int enemyHp;
    public int playerHp;
    public int points;              // This int variable is used to store the amount of points that the player can get.
    public int lifePoints;         // This in variable is used to tell the current life points.
    public int setLives;          // This int variable is used for a designer set of maximum player lives.
    public Vector3 checkpoint;   // Sets checkpoint as a vector variable.

    private void Awake()
    {
        //When one game manager instance exists it will carry on to other scenes through the game.
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        // When there are more than two game manager it will delete the previous one and warn the developer about the game manager's action.
        else
        {
            Destroy(this);
            Debug.LogError("[GameManager]Attempted to make a second Game Manager.");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //When the game starts it will set the checkpoint at the origin.
        checkpoint = new Vector3(0,0,0);
        //It sets the variable of setLives to the amount of lifePoints.
        instance.setLives = instance.lifePoints;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void enemyDamage()
    {
        if(enemyHp > 0)
        {
            enemyHp--;
        }
        else
        {
            Destroy(enemyPawn);
        }
    }
    public void playerDamage()
    {
        if(playerHp > 0)
        {
            playerHp--;
        }
        else
        {
            playerDeath();
        }   
    }
    public void playerDeath()
    {
        if (lifePoints > 0)
        {
            lifePoints--;
            Instantiate(playerPf, checkpoint, playerPf.transform.rotation);
            Destroy(playerPawn);
        }
        else
        {
            Destroy(playerPawn);
        }
    }
}
