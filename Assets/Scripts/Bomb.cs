using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Bomb : MonoBehaviour
{
    //Passed in time that can be changed
    public float bombTime = 60.0f;
    public GameObject r_player;
    public PlayerController r_playerCon;
    public Text r_text;

    // Use this for initialization
    void Start ()
    {
        r_player = GetComponent<PlayerController>().gameObject;
        r_playerCon = GetComponent<PlayerController>();
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
            r_player.GetComponent<PlayerController>().m_eCurrentPlayerState = r_playerCon.ChangeStateDead();
            bombTime = 0;
        }
    }
}
