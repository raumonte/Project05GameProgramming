using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //This makes the script an instance to be accessable in other scripts
    public GameObject playerPf;         //This stores the gameobject of the player prefab.
    public GameObject playerPawn;       //This stores the player pawn that is set at start.
    public GameObject enemyPawn;        //This sets the gameobject as the current enemy within the game.
    public int enemyHp;                 //This variable controls the hit points of the enemy.
    public int playerHp;                //This is the variable to control the amount of hit points the player has
    public int points;                  //This int variable is used to store the amount of points that the player can get.
    public int lifePoints;              //This in variable is used to tell the current life points.
    public int setLives;                //This int variable is used for a designer set of maximum player lives.
    public Vector3 checkpoint;          //Sets checkpoint as a vector variable.

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
    //This void mostly takes in the damage input of the enemy. Once it activates it should subtract the set hp by one and once it hits zero it would destroy the enemy.
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
    //Once it activates it lessens the amount of hit point set on the player. Once it hits zero it will activate another function that calls player death.
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
    /*Player has a set amount of lives. So everytime a player dies it would minus their amount of lives by one until the player reaches zero. 
     *Once it reaches zero it will destroy the game object and spawn them inside of the Game Over Scene
    */
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
            SceneManager.LoadScene("Game Over");
        }
    }
}
