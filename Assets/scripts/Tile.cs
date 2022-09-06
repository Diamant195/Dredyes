using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WorldTileTemplate", menuName = "WorldTileTemplate")]
public class WorldTile : ScriptableObject
{
    public GameObject tObject;
    public string type;
}
