using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Bomb : MonoBehaviour
{
    //Passed in time that can be changed
    public float bombTime = 60.0f;
    public GameObject r_player;
    public GameObject r_gameOverPanel;
    public Text r_text;

    public const uint MAX_PLAYERS = 4; // TODO: change in Player Controller
    public uint randSelection;

    // Use this for initialization
    void Start ()
    {
        // Choose a random player to assign bomb to.
        randSelection = (uint)Random.Range(0, MAX_PLAYERS - 1);
        Debug.Log("Player: " + randSelection);
    }

    void Awake()
    {
    }

    // Update is called once per frame
    void Update ()
    {
        //decrements the timer by deltatime
        bombTime -= Time.deltaTime;

        //Set the text to the time left on thew bomb
        //GetComponent<GUIText>().text = bombTime.ToString("f2");
        r_text.text = "Time Left: " + bombTime.ToString("f2");

        //if the timer is less than 0
        if (bombTime <= 0.01)
        {
            //kill current player
            //PlayerController.E_PLAYER_STATE.E_PLAYER_STATE_DEAD;
            r_player = GameObject.FindGameObjectWithTag("HasBomb"); // GetComponent<PlayerController>().gameObject;
            //TODO: error?
            r_player.GetComponent<PlayerController>().m_eCurrentPlayerState = PlayerController.E_PLAYER_STATE.E_PLAYER_STATE_DEAD;
            r_gameOverPanel.SetActive(true);
            //r_player.GetComponent<PlayerController>().SetPlayerStateDead(3);// m_eCurrentPlayerState = r_playerCon.ChangeStateDead();
            bombTime = 0;
        }
    }
}
