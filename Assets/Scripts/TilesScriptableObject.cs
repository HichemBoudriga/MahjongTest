using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tiles", menuName = "ScriptableObjects/TilesScriptableObject", order = 1)]
public class TilesScriptableObject : ScriptableObject
{
    public List<Tiles> tiles = new List<Tiles>();
}

[System.Serializable]
public class Tiles
{
    public GameObject tilePrefab;
    public string type;
    public int id;
}
