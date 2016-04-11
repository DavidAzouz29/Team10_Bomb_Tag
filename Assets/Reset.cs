using UnityEngine;
using System.Collections;

public class Reset : MonoBehaviour {

    public Vector3 DefaultPos = Vector3.zero;
    public GameObject r_player;

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ResetPos()
    {
        DefaultPos.y = 0.6f;
        r_player.transform.position = DefaultPos;
        //r_player.transform.rotation = DefaultPos;
    }
}
