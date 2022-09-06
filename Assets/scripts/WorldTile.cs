using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WorldTileTemplate", menuName = "WorldTileTemplate")]
public class Tile : ScriptableObject
{
    public GameObject tObject;
    public string type;
}