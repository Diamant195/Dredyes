using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Item[] slots = new Item[5];
    public GameObject[] InventorySlots = new GameObject[5];
    public int selectedSlot = 0;
    dredguy dredguys;
    int tmp;
    public Sprite block;
    GameObject DroppedItem;
    droppedItem discript;
    RaycastHit2D hit;
    public ship_manager shipmanager;
    public GameObject cursor_split, cursor_join;

    KeyCode[] keycodes =
    {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5,
    };
    
    void Start()
    {
        cursor_join.SetActive(false);
        cursor_split.SetActive(false);
        dredguys = GameObject.FindGameObjectWithTag("Player").GetComponent<dredguy>();
        // Assigns starting items for each slot, No item: Resources.Load("No Item") as Item, 0)
        //Slot 1
        slots[0] = (Item)ScriptableObject.CreateInstance("Item");
        slots[0].__init__(Resources.Load("Block") as Item, 7);
        //Slot 2
        slots[1] = (Item)ScriptableObject.CreateInstance("Item");
        slots[1].__init__(Resources.Load("Block") as Item, 7);
        //Slot 3
        slots[2] = (Item)ScriptableObject.CreateInstance("Item");
        slots[2].__init__(Resources.Load("Block") as Item, 7);
        //Slot 4
        slots[3] = (Item)ScriptableObject.CreateInstance("Item");
        slots[3].__init__(Resources.Load("Block") as Item, 7);
        //Slot 5
        slots[4] = (Item)ScriptableObject.CreateInstance("Item");
        slots[4].__init__(Resources.Load("Block") as Item, 7);


        UpdateInventory();
        InventorySlots[selectedSlot].GetComponent<Image>().color = Color.green;
        dredguys.transform.GetChild(0).transform.position = new Vector2(GameObject.FindGameObjectWithTag("Player").transform.position.x - 0.25f, dredguys.transform.GetChild(0).transform.position.y);
        //Summon Item
        SummonItem("Block", 4);
        for(int i = 0; i<30; i++)
        SummonItem("Ship Embiggener", 1);
    }
    public void SummonItem(string ItemName, int amount)
    {
        DroppedItem = (GameObject)Instantiate(Resources.Load("DroppedItem") as GameObject, GameObject.Find("clones").transform);
        DroppedItem.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
        DroppedItem.GetComponent<droppedItem>().takeInfo(Resources.Load(ItemName) as Item, amount);
    }
    public void UpdateInventory()
    {
        for(int j = 0; j < InventorySlots.Length; j++)
        {
            InventorySlots[j].transform.Find("ItemIcon").GetComponent<Image>().sprite = slots[j].Icon;
            if (slots[j].maxsize == 1 || slots[j].size == 0)
                InventorySlots[j].transform.GetChild(1).GetComponent<Text>().text = string.Empty;
            else
                InventorySlots[j].transform.GetChild(1).GetComponent<Text>().text = slots[j].size.ToString();
        }
        
    }

    public void UpdateSelectedSlot(GameObject sender)
    {
        selectedSlot = int.Parse(sender.name);
        foreach (GameObject slot in InventorySlots) slot.GetComponent<Image>().color = Color.white;
        InventorySlots[selectedSlot].GetComponent<Image>().color = Color.green;
    }

    public bool PickUpItem(GameObject requestingItem)
    {
        StartCoroutine(Item.cooldown());
        droppedItem discript = requestingItem.GetComponent<droppedItem>();
        if (slots[selectedSlot].itemName == "No Item")
        {
            slots[selectedSlot] = discript.item;
            UpdateInventory();
            return true;
        }
        if (slots[selectedSlot].itemName == discript.item.itemName)
        {
            while (slots[selectedSlot].size < slots[selectedSlot].maxsize)
            {
                if (discript.item.size <= 0) return true;
                else if (slots[selectedSlot].size == slots[selectedSlot].maxsize) break;
                discript.item.size--;
                slots[selectedSlot].size++;
                UpdateInventory();
            }
            for (int i = 0; i < 5; i++)
            {
                if (discript.item.size <= 0) return true;
                if (slots[i].itemName == discript.item.itemName)
                {
                    tmp = slots[i].size;
                    slots[i] = Instantiate(discript.item);
                    while (slots[i].size < slots[i].maxsize)
                    {
                        if (discript.item.size <= 0)
                        {
                            slots[i].size = tmp;
                            return true;
                        }
                        else if (tmp >= slots[i].maxsize) break;
                        discript.item.size--;
                        tmp++;
                        UpdateInventory();
                    }
                    slots[i].size = tmp;
                }
            }
        }
            for (int i = 0; i < 5; i++)
            {
                if (slots[i].itemName == discript.item.itemName)
                {
                    while (slots[i].size < slots[i].maxsize)
                    {
                        if (discript.item.size <= 0) return true;
                        discript.item.size--;
                        slots[i].size++;
                        UpdateInventory();
                    }
                }
            }
        for (int i = 0; i < 5; i++)
        {
            if (slots[i].itemName == "No Item")
            {
                slots[i] = discript.item;
                UpdateInventory();
                return true;
            }
        }
        UpdateInventory();
        return false;
    }

    public void Drop()
    {
        hit = Physics2D.Raycast(GameObject.FindGameObjectWithTag("Player").transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), 1f);
        if (hit || slots[selectedSlot].itemName == "No Item") return;
        DroppedItem = (GameObject)Instantiate(Resources.Load("DroppedItem") as GameObject, GameObject.Find("clones").transform);
        if(Camera.main.ScreenToWorldPoint(Input.mousePosition).x > GameObject.FindGameObjectWithTag("Player").transform.position.x) DroppedItem.transform.position = new Vector2(GameObject.FindGameObjectWithTag("Player").transform.position.x + 1f, GameObject.FindGameObjectWithTag("Player").transform.position.y);
        else if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < GameObject.FindGameObjectWithTag("Player").transform.position.x) DroppedItem.transform.position = new Vector2(GameObject.FindGameObjectWithTag("Player").transform.position.x -1f, GameObject.FindGameObjectWithTag("Player").transform.position.y);
        DroppedItem.GetComponent<droppedItem>().takeInfo(slots[selectedSlot]);
        DroppedItem.GetComponent<Rigidbody2D>().AddForce(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x * 25, Camera.main.ScreenToWorldPoint(Input.mousePosition).y * 22));
        DroppedItem.GetComponent<Rigidbody2D>().AddTorque(1, ForceMode2D.Impulse);
        tmp = 10;
        for(int i = 0; i<5; i++)
        {
            if (i == selectedSlot || slots[i].itemName != slots[selectedSlot].itemName) continue;
                tmp = i;
                break;
        }
        slots[selectedSlot] = Resources.Load("No Item") as Item;
        if (tmp != 10)
        {
            selectedSlot = tmp;
            foreach (GameObject slot in InventorySlots) slot.GetComponent<Image>().color = Color.white;
            InventorySlots[selectedSlot].GetComponent<Image>().color = Color.green;
        }
        UpdateInventory();
    }
    void Update()
    {
        dredguys.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = slots[selectedSlot].Icon;
        if (dredguys.horizontalaxis > 0.0001)
        {
            dredguys.transform.GetChild(0).transform.position = new Vector2(GameObject.FindGameObjectWithTag("Player").transform.position.x + 0.25f, dredguys.transform.GetChild(0).transform.position.y);
        }
        else if (dredguys.horizontalaxis < -0.0001)
        {
            dredguys.transform.GetChild(0).transform.position = new Vector2(GameObject.FindGameObjectWithTag("Player").transform.position.x - 0.25f, dredguys.transform.GetChild(0).transform.position.y);
        }
        UpdateInventory();
        for (int i = 0; i < 5; i++)
        {
            if (Input.GetKeyDown(keycodes[i]))
            {
                selectedSlot = i;
                foreach (GameObject slot in InventorySlots) slot.GetComponent<Image>().color = Color.white;
                InventorySlots[selectedSlot].GetComponent<Image>().color = Color.green;
            }
        }
        if (Input.GetKeyDown(KeyCode.Q) && !dredguys.onHelm)
        {
            Drop();
        }
        for (int i = 0; i < 5; i++)
            if (slots[i].size == 0 && slots[i].itemName != "No Item")
            {
                for(int j = 0; j < 5; j++)
                {
                    if (slots[selectedSlot].itemName == slots[j].itemName && j != selectedSlot)
                    {
                        selectedSlot = j;
                        foreach (GameObject slot in InventorySlots) slot.GetComponent<Image>().color = Color.white;
                        InventorySlots[selectedSlot].GetComponent<Image>().color = Color.green;
                        break;
                    }
                }
                slots[i] = (Item)ScriptableObject.CreateInstance("Item");
                slots[i].__init__((Item)Resources.Load("No Item") as Item, 0);
            }
        /*Cursor Checks */
        if (slots[selectedSlot].itemName == "Ship Embiggener")
        {
            cursor_join.SetActive(false);
            cursor_split.SetActive(true);
            cursor_split.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, dredguys.dir * 90);
            cursor_split.GetComponent<RectTransform>().position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        else if (slots[selectedSlot].itemName == "Ship Shrinkinator")
        {
            cursor_split.SetActive(false);
            cursor_join.SetActive(true);
            cursor_join.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, dredguys.dir * 90);
            cursor_join.GetComponent<RectTransform>().position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        else
        {
            cursor_split.SetActive(false);
            cursor_join.SetActive(false);
        }
        /*Ship Embiggener*/
        if (Input.GetButtonDown("Fire1") && slots[selectedSlot].itemName == "Ship Embiggener" && Item.consumable)
        {
            slots[selectedSlot].size--;
            UpdateInventory();
            shipmanager.expand();
        }
    }
}
