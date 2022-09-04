using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class droppedItem : MonoBehaviour
{
    public Item item; // itemName, size, maxsize, Icon, detailedIcon
    public GameObject text;
    bool hovering;
    GameObject clone;
    TextMeshPro displayText;
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = item.Icon;
    }
    void OnMouseEnter()
    {
        clone = Instantiate(text, transform);
        displayText = clone.GetComponent<TextMeshPro>();
        hovering = true;
    }
    void OnMouseExit()
    {
        hovering = false;
        Destroy(clone);
    }
    private IEnumerator OnMouseDown()
    {
        yield return new WaitForSeconds(0.05f);
        if (GameObject.Find("Inventory").GetComponent<Inventory>().PickUpItem(gameObject)) Destroy(gameObject);
        if (item.size <= 0) Destroy(gameObject);
    }
    public void takeInfo(Item item, int size)
    {
        this.item = Instantiate(item);
        this.item.size = size;
    }
    public void takeInfo(Item item)
    {
        this.item = Instantiate(item);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (item.maxsize > 1 && collision.collider.GetComponent<droppedItem>() != null)
        {
            if(collision.collider.GetComponent<droppedItem>().item.itemName == item.itemName && collision.collider.GetComponent<droppedItem>().item.size + item.size < item.maxsize)
            {
                if (item.itemId > collision.collider.GetComponent<droppedItem>().item.itemId)
                {
                    collision.collider.GetComponent<droppedItem>().item.size += item.size;
                    Destroy(gameObject);
                }
            }
        }
    }
    void Update()
    {
        if (hovering)
        {
            clone.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.75f, clone.transform.position.z);
            clone.transform.eulerAngles = new Vector3(0, 0, 0);
            displayText.text = item.size + "x " + item.itemName;
        }
    }
}
