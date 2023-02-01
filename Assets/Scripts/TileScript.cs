using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    
    [SerializeField]
    private string m_type;
    [SerializeField]
    private int m_id;

    public string GetTiletype()
    {

        return m_type;
    }

    public int GetTileID()
    {
        return m_id;
    }

    public void SetTileType(string type )
    {
        m_type = type;
    }
    
    public void SetTileID(int id)
    {
        m_id = id;
    }

}
