using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TilesScriptableObject m_tilesData;

    [SerializeField]
    private Transform m_tilesParent;


    [SerializeField]
    private List<Transform> m_tilesWallList;

    [SerializeField]
    private Transform m_tileHand_1;

    [SerializeField]
    private List<Transform> m_tilesHandList;

    [SerializeField]
    private int m_score;

    public EventHandler<int> OnScoreChanged;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            Debug.LogError("There  is more than one GameManager! " + transform + " - " + Instance);
            return;

        }

        Instance = this;


        m_tilesWallList = new List<Transform>();
        m_tilesHandList = new List<Transform>();

    }
    private void CreateTiles()
    {
        foreach (Tiles tile in m_tilesData.tiles)
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject tilePrefab = Instantiate(tile.tilePrefab, m_tilesParent);
                InitTileData(tilePrefab, tile);
            }

        }
    }

    private void InitTileData(GameObject tileObject, Tiles tileData)
    {
        TileScript tileScript = tileObject.GetComponent<TileScript>();
        tileScript.SetTileType(tileData.type);
        tileScript.SetTileID(tileData.id);
        if (tileObject.TryGetComponent<Draggable>(out Draggable draggable))
        {
            draggable.SetDraggable(false);
          }

    }

    private void AddTilesToWallList()
    {
        foreach (Transform child in m_tilesParent)
        {
            m_tilesWallList.Add(child);
        }
    }


    private void RandomizeTilesInWallList()
    {
        m_tilesWallList.Shuffle(m_tilesWallList.Count);
    }



    private void OrganizeTilesPositionInWall()
    {
        float x = 0.2f;
        float z = 0.17f;
        int index = 0;
        foreach (Transform child in m_tilesWallList)
        {
            if (index >= 0 && index < 15)
            {
                child.localPosition = new Vector3(x, 0.0075f, z);

                index++;
                x -= 0.03f;
            }
            else
            {
                index = 0;
                z -= 0.04f;
                x = 0.2f;
            }

            child.localEulerAngles = new Vector3(90, 0, 0);
        }
    }

    private void GiveHand(Transform tileHand, int tileNumbers)
    {
        if (m_tilesWallList.Count < tileNumbers)
        {
            return;
        }



        if (m_tileHand_1.childCount > tileNumbers)
        {
            return;
        }


        m_tilesHandList.Clear();

        for (int i = 0; i < tileNumbers; i++)
        {
            Transform tile = m_tilesWallList[0];
            m_tilesWallList.RemoveAt(0);
            tile.parent = null;
            m_tilesHandList.Add(tile);

        }



        for (int i = 0; i < m_tilesHandList.Count; i++)
        {
            Transform tile = m_tilesHandList[i];
            tile.parent = m_tileHand_1.GetChild(i);
            tile.localPosition = Vector3.zero;
            tile.localRotation = Quaternion.Euler(-90, 0, 0);

        }





    }

    public void AddOneTileToHand(Transform tileHand)
    {
        if(m_tilesWallList.Count==0)
        {
            return;
        }

        Transform tile = m_tilesWallList[0];
        m_tilesWallList.RemoveAt(0);
        tile.parent = null;
        m_tilesHandList.Add(tile);


        for (int i = 0; i < m_tilesHandList.Count; i++)
        {
            Transform tile_ = m_tilesHandList[i];
            tile_.parent = null;
            tile_.parent = tileHand.GetChild(i);
            tile_.localPosition = Vector3.zero;
            tile_.localRotation = Quaternion.Euler(-90, 0, 0);

        }
    }


    public List<Transform> GetHandList()
    {
        return m_tilesHandList;
    }

    

    


    public void RemoveFromHand(Transform tile )
    {
        m_tilesHandList.Remove(tile);
    }

    public Transform GetTileHand_1()
    {
        return m_tileHand_1;
    }

    public void UpdateScore(int amount)
    {
        m_score += amount;
        OnScoreChanged?.Invoke(this, m_score);
    }


    // Start is called before the first frame update
   private  void Start()
    {
        
    }

   public void StartGame()
    {
        CreateTiles();
        AddTilesToWallList();
        RandomizeTilesInWallList();
        OrganizeTilesPositionInWall();

        GiveHand(m_tileHand_1, 14);
    }

}
