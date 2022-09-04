using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helm : MonoBehaviour
{
    dredguy playerscript;
    void Start()
    {
        playerscript = GameObject.FindGameObjectWithTag("Player").GetComponent<dredguy>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) ||Input.GetKeyDown(KeyCode.Escape))
        {
            playerscript.onHelm = false;
            playerscript.anim.SetBool("onHelm", false);
        }
    }
    void OnMouseDown()
    {
        playerscript.onHelm = true;
        playerscript.anim.SetBool("onHelm", true);
    }
}
