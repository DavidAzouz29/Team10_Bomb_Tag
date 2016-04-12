/// <summary>
/// Author: 		David Azouz
/// Date Created: 	12/04/16
/// Date Modified: 	12/04/16
/// --------------------------------------------------
/// Brief: A Player Manager class that handles players.
/// viewed https://drive.google.com/drive/folders/0B67Mvyh-0w-RTlhLX1lsOHRfdDA
/// https://unity3d.com/learn/tutorials/projects/survival-shooter/more-enemies
/// 
/// ***EDIT***
/// - 	- David Azouz 11/04/16
/// -  - David Azouz 11/04/16
/// -  - David Azouz 12/04/16
/// 
/// TODO:
/// - change remove const from MAX_PLAYERS
/// </summary>

using UnityEngine;
using System.Collections;

//#define MAX_PLAYERS 4

public class PlayerManager : MonoBehaviour
{
    //----------------------------------
    // PUBLIC VARIABLES
    //----------------------------------
    public const uint MAX_PLAYERS = 4;
    public GameObject r_Player; // Referance to a player.
    public Vector3 v3PlayerPosition = Vector3.zero;

    public Color[] colorsArray;

    //----------------------------------
    // PRIVATE VARIABLES
    //----------------------------------
    private uint[] uiPlayerArray = new uint[MAX_PLAYERS];

    // Use this for initialization
    void Start ()
    {
        v3PlayerPosition.y = 0.39f;

        // Array of colors
        colorsArray[0] = Color.red;
        colorsArray[1] = Color.yellow;
        colorsArray[2] = Color.green;
        colorsArray[3] = Color.blue;

        //Loop through and create our players.
        for (uint i = 0; i <= MAX_PLAYERS; ++i)
        {
            v3PlayerPosition.x = Random.Range(0, 20); //
            v3PlayerPosition.z = Random.Range(0, 20); //
            Object j = Instantiate(r_Player, v3PlayerPosition, r_Player.transform.rotation);
            MeshRenderer mesh = ((GameObject)j).GetComponentInChildren<MeshRenderer>();
            // Set Color to color in array
            mesh.sharedMaterial.SetColor("_Color", colorsArray[i]);
            PlayerController playerController = ((GameObject)j).GetComponent<PlayerController>();
            playerController.SetPlayerID(i);
            Debug.Log(v3PlayerPosition);
            
            /* r_PlayerController = GetComponent<PlayerController>();
            r_PlayerMesh = r_PlayerController.GetComponentInChildren<MeshRenderer>(); */
            // Set Color to color in array
            //r_PlayerMesh.sharedMaterial.SetColor("_Color", colorsArray[i]);
            //r_PlayerController.SetPlayerID(i);            
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
	    
	}
}
