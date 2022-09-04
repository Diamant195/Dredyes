using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dredguy : MonoBehaviour
{
    public float horizontalaxis;
    public float movingspeed;
    public bool isjumping = false;
    public float jumpspeed;
    Rigidbody2D rigidbodys;
    public Animator anim;
    public bool onHelm;
    public int dir;
    void Start()
    {
        rigidbodys = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalaxis = Input.GetAxis("Horizontal");
        if (!onHelm) transform.position = new Vector2(transform.position.x + horizontalaxis * movingspeed * Time.deltaTime, transform.position.y);
            if ((Input.GetKeyDown(KeyCode.Space) && !isjumping) && !onHelm)
        {
            rigidbodys.velocity += Vector2.up * jumpspeed;
            isjumping = true;
            anim.SetBool("isjumping", true);
        }
        anim.SetFloat("horizontal", horizontalaxis);
        if(Input.GetKeyDown(KeyCode.R))
        {
            if (dir != 3) dir++;
            else dir = 0;         
        }
    }
}
