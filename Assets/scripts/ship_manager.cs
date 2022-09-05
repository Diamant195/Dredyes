using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ship_manager : MonoBehaviour
{
    List<List<GameObject>> tiles = new List<List<GameObject>>();
    public int shipheight = 5;
    public int shipwidth = 10;
    public GameObject blcorner, brcorner, tlcorner, trcorner;
    public GameObject boat;
    public GameObject vertpf;
    public GameObject horpf;
    public GameObject borderclones;
    GameObject clonedtile;
    GameObject clonedborder;
    public GameObject bgtilepf;
    public GameObject leftcollider, rightcollider;
    dredguy playerscript;
    public float shipspeed;
    float verticalaxis;
    int dir;
    void Start()
    {
        Debug.Log("Hello World");
        boat = GameObject.FindWithTag("ship");
        playerscript = GameObject.FindGameObjectWithTag("Player").GetComponent<dredguy>();

    }
    private void Update()
    {
        if (playerscript.onHelm)
        {
            verticalaxis = Input.GetAxis("Vertical");
            transform.parent.transform.position = new Vector2(transform.parent.transform.position.x + playerscript.horizontalaxis * shipspeed * Time.deltaTime, transform.parent.transform.position.y + verticalaxis * shipspeed * Time.deltaTime);
        }
    }
    void renderborders()
    {
        if (shipwidth < 3) shipwidth = 3;
        if (shipheight < 3) shipheight = 3;
        blcorner.transform.position = new Vector2(boat.transform.position.x, boat.transform.position.y);
        brcorner.transform.position = new Vector2(boat.transform.position.x + shipwidth, boat.transform.position.y);
        tlcorner.transform.position = new Vector2(boat.transform.position.x, boat.transform.position.y + shipheight);
        trcorner.transform.position = new Vector2(boat.transform.position.x + shipwidth, boat.transform.position.y + shipheight);

        foreach (GameObject clone in GameObject.FindGameObjectsWithTag("border")) Destroy(clone);
        foreach (GameObject clone in GameObject.FindGameObjectsWithTag("jumpy")) Destroy(clone);
        foreach (GameObject clone in GameObject.FindGameObjectsWithTag("rborder")) Destroy(clone);
        foreach (GameObject clone in GameObject.FindGameObjectsWithTag("lborder")) Destroy(clone);
        foreach (GameObject clone in GameObject.FindGameObjectsWithTag("tborder")) Destroy(clone);
        foreach (GameObject clone in GameObject.FindGameObjectsWithTag("btborder")) Destroy(clone);
        foreach (GameObject clone in GameObject.FindGameObjectsWithTag("sub_tile")) Destroy(clone);
        // Generates horizontal borders
        for(int i = 1; i < shipwidth; i++)
        {
            // Generates bottom borders
            clonedborder = Instantiate(horpf);
            clonedborder.transform.parent = borderclones.transform;
            clonedborder.transform.position = new Vector2(blcorner.transform.position.x + i, blcorner.transform.position.y);
            clonedborder.tag = "btborder";
            // Generates top borders
            clonedborder = Instantiate(horpf);
            clonedborder.transform.parent = borderclones.transform;
            clonedborder.transform.position = new Vector2(tlcorner.transform.position.x + i, tlcorner.transform.position.y);
            clonedborder.tag = "tborder";
        }
        // Generates vertical borders
        for (int i = 1; i < shipheight; i++)
        {
            // Generates left borders
            clonedborder = Instantiate(vertpf);
            clonedborder.transform.parent = borderclones.transform;
            clonedborder.transform.position = new Vector2(blcorner.transform.position.x, blcorner.transform.position.y + i);
            clonedborder.tag = "lborder";
            // Generates right borders
            clonedborder = Instantiate(vertpf);
            clonedborder.transform.parent = borderclones.transform;
            clonedborder.transform.position = new Vector2(brcorner.transform.position.x, brcorner.transform.position.y + i);
            clonedborder.tag = "rborder";
        }
        // Generates tiles
        for(int i = 1; i < shipheight; i++)
        {
            for(int t = 1; t < shipwidth; t++)
            {
                clonedtile = Instantiate(bgtilepf);
                clonedtile.transform.parent = borderclones.transform;
                clonedtile.transform.position = new Vector2(blcorner.transform.position.x + i, blcorner.transform.position.y + t);
            }
        }
    }
    public void expand() // dir 0 = horizontal, dir 1 = vertical
    {
        dir = playerscript.dir;
        // Expand width
        if((dir == 0 || dir == 2) && shipwidth < 32)
        {
            shipwidth++;
            clonedtile = Instantiate(horpf);
            clonedtile.transform.parent = borderclones.transform;
            clonedtile.transform.position = new Vector2(blcorner.transform.position.x + shipwidth - 1, blcorner.transform.position.y);
            clonedtile.tag = "btborder";
            clonedtile = Instantiate(horpf);
            clonedtile.transform.parent = borderclones.transform;
            clonedtile.transform.position = new Vector2(tlcorner.transform.position.x + shipwidth - 1, tlcorner.transform.position.y);
            clonedtile.tag = "tborder";
            foreach(GameObject rightborder in GameObject.FindGameObjectsWithTag("rborder"))
            {
                rightborder.transform.position = new Vector2(rightborder.transform.position.x + 1, rightborder.transform.position.y);
            }
            brcorner.transform.position = new Vector2(brcorner.transform.position.x + 1, brcorner.transform.position.y);
            trcorner.transform.position = new Vector2(trcorner.transform.position.x + 1, trcorner.transform.position.y);
            for(int i = 1; i < shipheight; i++)
            {
                clonedtile = Instantiate(bgtilepf);
                clonedtile.transform.parent = borderclones.transform;
                clonedtile.transform.position = new Vector2(blcorner.transform.position.x + shipwidth - 1, blcorner.transform.position.y + i);
            }
        }
        // Expands height
        if ((dir == 1 || dir == 3) && shipheight < 32)
        {
            leftcollider.transform.localScale = new Vector2(leftcollider.transform.localScale.x, leftcollider.transform.localScale.y + 1);
            rightcollider.transform.localScale = new Vector2(rightcollider.transform.localScale.x, rightcollider.transform.localScale.y + 1);
            leftcollider.transform.position = new Vector2(leftcollider.transform.position.x, leftcollider.transform.position.y + 0.5f);
            rightcollider.transform.position = new Vector2(rightcollider.transform.position.x, rightcollider.transform.position.y + 0.5f);
            shipheight++;
            clonedtile = Instantiate(vertpf);
            clonedtile.transform.parent = borderclones.transform;
            clonedtile.transform.position = new Vector2(blcorner.transform.position.x, blcorner.transform.position.y + shipheight - 1);
            clonedtile.tag = "lborder";
            clonedtile = Instantiate(vertpf);
            clonedtile.transform.parent = borderclones.transform;
            clonedtile.transform.position = new Vector2(brcorner.transform.position.x, brcorner.transform.position.y + shipheight - 1);
            clonedtile.tag = "rborder";
            foreach (GameObject topborder in GameObject.FindGameObjectsWithTag("tborder"))
            {
                topborder.transform.position = new Vector2(topborder.transform.position.x, topborder.transform.position.y + 1);
            }
            tlcorner.transform.position = new Vector2(tlcorner.transform.position.x, tlcorner.transform.position.y + 1);
            trcorner.transform.position = new Vector2(trcorner.transform.position.x, trcorner.transform.position.y + 1);
            for (int i = 1; i < shipwidth; i++)
            {
                clonedtile = Instantiate(bgtilepf);
                clonedtile.transform.parent = borderclones.transform;
                clonedtile.transform.position = new Vector2(blcorner.transform.position.x + i, blcorner.transform.position.y + shipheight - 1);
            }
        }
    }
}

