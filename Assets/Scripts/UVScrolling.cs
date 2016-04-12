using UnityEngine;
using System.Collections;

public class UVScrolling : MonoBehaviour {
    public Shader r_ShaderSlime;
    public MeshRenderer r_MeshSlime;
    // Use this for initialization
    void Start () {
        r_ShaderSlime = GetComponent<Shader>();
    }
	
	// Update is called once per frame
	void Update () {
        //r_ShaderSlime.
        for (uint i = 0; i < 100; ++i)
        {
            r_MeshSlime.material.SetTextureOffset("_MainTex", new Vector2(i, 0));
            //if (i > 100)
            //{
            //    i--;
            //}
        }

    }
}
