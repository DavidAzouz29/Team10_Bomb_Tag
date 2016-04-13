/// ----------------------------------
/// <summary>
/// Name: PlayerController.cs
/// Author: Joel Hadfield and David Azouz
/// Date Created: 11/4/2016
/// Date Modified: 4/2016
/// ----------------------------------
/// Brief: Player Controller class that controls the player.
/// viewed: https://unity3d.com/learn/tutorials/projects/roll-a-ball/moving-the-player
/// 
/// *Edit*
/// - Player state machine - 11/04/2016
/// - More than one player added - 12/4/2016
/// TODO:
/// - 
/// </summary>
/// ----------------------------------

using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public const uint MAX_PLAYERS = 4;

    // PRIVATE VARIABLES
    public float playerSpeed;
    public float speedBoost = 6;
    public GameObject r_bomb;
    public GameObject r_bombParticleEffect;
    public Camera r_camera;
    public string verticalAxis = "P1_Vertical";
    public string horizontalAxis = "P1_Horizontal";

    // A way to identidy players
    [SerializeField] private uint m_playerID = 0;

    // PRIVATE VARIABLES
    Rigidbody m_rigidBody;

    //choosing player states
    [HideInInspector]
    public enum E_PLAYER_STATE
    {
        E_PLAYER_STATE_ALIVE,
        E_PLAYER_STATE_BOMB,
        E_PLAYER_STATE_DEAD,

        E_PLAYER_STATE_COUNT,
    };

    public E_PLAYER_STATE m_eCurrentPlayerState;

    //private bool isBombAllocated = false;

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

        /*int randSelection = (int)Random.Range(0, MAX_PLAYERS - 1);
        Debug.Log("Player: " + randSelection); */

        for (uint i = 0; i < MAX_PLAYERS; ++i)
        {
            if (m_playerID == i)
            {
                verticalAxis = "P" + (i + 1) + "_Vertical";
                horizontalAxis = "P" + (i + 1) + "_Horizontal";

            }
            // Choose someone to allocate the bomb too
            /*if (m_playerID == randSelection && !isBombAllocated)
            {
                m_eCurrentPlayerState = E_PLAYER_STATE.E_PLAYER_STATE_BOMB;
                isBombAllocated = true;
            }*/
        }        
    }
	
	// Update is called once per frame
	void Update ()
    {
        //creating a variable that gets the input axis
        float moveHorizontal = Input.GetAxis(horizontalAxis);
        float moveVertical = Input.GetAxis(verticalAxis);

        Vector3 movementDirection = new Vector3(moveHorizontal, 0.0f, moveVertical);

        //Switches between player states
        switch (m_eCurrentPlayerState)
        {
            //checks if the player is alive
            case E_PLAYER_STATE.E_PLAYER_STATE_ALIVE:
                {
                    m_rigidBody.AddForce(movementDirection * playerSpeed * Time.deltaTime);
                    r_bomb.SetActive(false);
                    //Debug.Log("Alive!");
                    break;
                }
            case E_PLAYER_STATE.E_PLAYER_STATE_BOMB:
                {
                    m_rigidBody.AddForce(movementDirection * (playerSpeed + speedBoost) * Time.deltaTime);
                    r_bomb.SetActive(true);
                    //Debug.Log("Bomb!");
                    break;
                }
            //if player is dead
            case E_PLAYER_STATE.E_PLAYER_STATE_DEAD:
                {
                    // Particle effect bomb (explosion)
                    r_bombParticleEffect.SetActive(true);
                    // actions to perform after a certain time
                    uint uiBombEffectTimer = 6;
                    Invoke("BombEffectDead", uiBombEffectTimer);
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

    void BombEffectDead()
    {
        r_bomb.SetActive(false);
        Destroy(this.gameObject);
    }

    public E_PLAYER_STATE ChangeStateBomb()
    {
        return m_eCurrentPlayerState = E_PLAYER_STATE.E_PLAYER_STATE_BOMB;
    }

    public E_PLAYER_STATE ChangeStateDead()
    {
        return m_eCurrentPlayerState = E_PLAYER_STATE.E_PLAYER_STATE_DEAD;
    }

    public uint GetPlayerID()
    {
        return m_playerID;
    }

    public void SetPlayerID(uint a_uiPlayerID)
    {
        m_playerID = a_uiPlayerID;
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
                r_camera.fieldOfView = Mathf.Lerp(70, 60, Time.time);
                Debug.Log("CHECK IF COLLIDED");
            }
        }
    }

}
