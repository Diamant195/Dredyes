using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foot : MonoBehaviour
{
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "jumpy")
        {
            transform.parent.GetComponent<dredguy>().isjumping = true;
            transform.parent.GetComponent<dredguy>().anim.SetBool("isjumping", true);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "jumpy" || collision.collider.tag == "btborder")
        {
            transform.parent.GetComponent<dredguy>().isjumping = false;
            transform.parent.GetComponent<dredguy>().anim.SetBool("isjumping", false);
        }
    }
}
