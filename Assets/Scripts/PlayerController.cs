﻿/// ----------------------------------
/// <summary>
/// Name: PlayerController.cs
/// Author: Joel Hadfield
/// Date Created: 11/4/2016
/// Date Modified: 4/2016
/// ----------------------------------
/// Brief: Player Controller class that controls the player.
/// viewed: https://unity3d.com/learn/tutorials/projects/roll-a-ball/moving-the-player
/// 
/// TODO:
/// - 
/// </summary>
/// ----------------------------------

using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    Rigidbody m_rigidBody;
    public float playerSpeed;
    public float speedBoost;
    private bool m_isDead = false;

    public string verticalAxis = "Vertical";
    public string horizontalAxis = "Horizontal";


    //choosing player states
    enum E_PLAYER_STATE
    {
        E_PLAYER_STATE_ALIVE,
        E_PLAYER_STATE_BOMB,
        E_PLAYER_STATE_DEAD,

        E_PLAYER_STATE_COUNT,
    };

    E_PLAYER_STATE m_eCurrentPlayerState;

    // Use this for initialization
    void Start ()
    {
        m_rigidBody = GetComponent<Rigidbody>();

        //if rigid body == null
        if (!m_rigidBody)
        {
            Debug.LogError("No Rigidbody");
        }

        

        //setting our current state to alive
        m_eCurrentPlayerState = E_PLAYER_STATE.E_PLAYER_STATE_ALIVE;

    }
	
	// Update is called once per frame
	void Update ()
    {
        //creating a variable that gets the input axis
        float moveHorizontal = Input.GetAxis(horizontalAxis);
        float moveVertical = Input.GetAxis(verticalAxis);

        Vector3 movementDirection = new Vector3(moveHorizontal, 0.0f, moveVertical);

        //TODO: don't rotate bomb?
        //GetComponentInChildren<>

        //Switches between player states
        switch (m_eCurrentPlayerState)
        {
            //checks if the player is alive
            case E_PLAYER_STATE.E_PLAYER_STATE_ALIVE:
                {
                    m_rigidBody.AddForce(movementDirection * playerSpeed * Time.deltaTime);
                    //Debug.Log("Alive!");
                    break;
                }
            case E_PLAYER_STATE.E_PLAYER_STATE_BOMB:
                {
                    m_rigidBody.AddForce(movementDirection * (playerSpeed + speedBoost) * Time.deltaTime);
                    Debug.Log("Bomb!");
                    break;
                }
            //if player is dead
            case E_PLAYER_STATE.E_PLAYER_STATE_DEAD:
                {
                    //play death animation;
                    Debug.Log("Dead :(");
                    break;
                }
            default:
                {
                    Debug.LogError("No State Chosen!");
                    break;
                }
        }
	}

    void OnCollisionExit(Collision a_collision)
    {
        //if current players state is "BOMB"
        if (m_eCurrentPlayerState == E_PLAYER_STATE.E_PLAYER_STATE_BOMB)
        {
            //And is colliding with another player
            if (a_collision.collider.tag == "Player")
            {
                //change the state of ourselves to alive
                m_eCurrentPlayerState = E_PLAYER_STATE.E_PLAYER_STATE_ALIVE;
                // change the other players state
                a_collision.collider.GetComponent<PlayerController>().m_eCurrentPlayerState = E_PLAYER_STATE.E_PLAYER_STATE_BOMB;
            }
        }
    }

}
