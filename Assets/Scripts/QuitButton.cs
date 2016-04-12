using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class QuitButton : MonoBehaviour
{
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Application.Quit();
        }
    }

    /*void OnMouseEnter()
    {
        GetComponent<Light>().enabled = true;
    }

    void OnMouseExit()
    {
        GetComponent<Light>().enabled = false;
    }*/
}