using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using TMPro;

using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField]
    private TextMeshProUGUI m_scoreText;


    [SerializeField]

    private Button startGameButton;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            Debug.LogError("There  is more than one   UIManager! " + transform + " - " + Instance);
            return;

        }

    }

    private void Start()
    {
        GameManager.Instance.OnScoreChanged += GameManager_OnScoreChanged;

    }

    public void DiscardButton()
    {
        List<Transform> handList = GameManager.Instance.GetHandList();
        List<Transform> tilesToDiscard = new List<Transform>();

        foreach (Transform tile in handList)
        {
            if (tile.TryGetComponent<SelectableTile>(out SelectableTile selectableTile))
            {
                if (selectableTile.IsSelected())
                {
                    tilesToDiscard.Add(tile);
                }
            }
        }

        if (tilesToDiscard.Count != 1)
        {
            return;
        }


        Transform tilToDiscard = tilesToDiscard[0];
        GameManager.Instance.RemoveFromHand(tilToDiscard);
        tilToDiscard.transform.SetParent(null);
        tilToDiscard.gameObject.SetActive(false);

        Transform tileHand = GameManager.Instance.GetTileHand_1();
        GameManager.Instance.AddOneTileToHand(tileHand);


    }

    public void DropButton()
    {
        List<Transform> handList = GameManager.Instance.GetHandList();

        List<Transform> tilesToDrop = new List<Transform>();
        foreach (Transform tile in handList)
        {
            if (tile.TryGetComponent<SelectableTile>(out SelectableTile selectableTile))
            {
                if (selectableTile.IsSelected())
                {

                    tilesToDrop.Add(tile);

                }
            }
        }

        if (tilesToDrop.Count < 3)
        {
            return;
        }

        if (CheckCombo(tilesToDrop, CheckComboSuccess, CheckComboFail))
        {
            foreach (Transform tile in tilesToDrop)
            {
                GameManager.Instance.RemoveFromHand(tile);
                tile.SetParent(null);
                tile.gameObject.SetActive(false);

                Transform tileHand = GameManager.Instance.GetTileHand_1();
                GameManager.Instance.AddOneTileToHand(tileHand);
            }

        }


    }

    public void StartGameButton()
    {
        GameManager.Instance.StartGame();
        startGameButton.enabled = false;
    }


    private void CheckComboSuccess(int amount)
    {
        GameManager.Instance.UpdateScore(amount);
    }

    private void CheckComboFail()
    {

    }

    private bool CheckCombo(List<Transform> tilesToDrop, Action<int> Success, Action Fail)
    {

        int tileCount = tilesToDrop.Count;


        if (tileCount == 3)
        {

            string tileType_1 = tilesToDrop[0].GetComponent<TileScript>().GetTiletype();
            string tileType_2 = tilesToDrop[1].GetComponent<TileScript>().GetTiletype();
            string tileType_3 = tilesToDrop[2].GetComponent<TileScript>().GetTiletype();

            if (!(tileType_1 == tileType_2 && tileType_2 == tileType_3))
            {
                Fail();
                return false;
            }

            int tileId_1 = tilesToDrop[0].GetComponent<TileScript>().GetTileID();
            int tileId_2 = tilesToDrop[1].GetComponent<TileScript>().GetTileID();
            int tileId_3 = tilesToDrop[2].GetComponent<TileScript>().GetTileID();

            int min = Mathf.Min(tileId_1, tileId_2, tileId_3);
            int max = Mathf.Max(tileId_1, tileId_2, tileId_3);


            if (tileId_1 == tileId_2 && tileId_2 == tileId_3)
            {
                Success(300);
                return true;
            }

            else if (max == min + 2 && (tileId_1 == min + 1 || tileId_2 == min + 1 || tileId_3 == min + 1))
            {
                Success(500);
                return true;
            }

            else
            {

                Fail();
                return false;
            }



        }


        if (tileCount == 4)
        {
            string tileType_1 = tilesToDrop[0].GetComponent<TileScript>().GetTiletype();
            string tileType_2 = tilesToDrop[1].GetComponent<TileScript>().GetTiletype();
            string tileType_3 = tilesToDrop[2].GetComponent<TileScript>().GetTiletype();
            string tileType_4 = tilesToDrop[3].GetComponent<TileScript>().GetTiletype();
            if (!(tileType_1 == tileType_2 && tileType_2 == tileType_3 && tileType_3 == tileType_4))
            {
                Fail();
                return false;
            }

            int tileId_1 = tilesToDrop[0].GetComponent<TileScript>().GetTileID();
            int tileId_2 = tilesToDrop[1].GetComponent<TileScript>().GetTileID();
            int tileId_3 = tilesToDrop[2].GetComponent<TileScript>().GetTileID();
            int tileId_4 = tilesToDrop[3].GetComponent<TileScript>().GetTileID();

            if (!(tileId_1 == tileId_2 && tileId_2 == tileId_3 && tileId_3 == tileId_4))
            {
                Fail();
                return false;
            }

            Success(400);
            return true;
        }

        return false;


    }

    private void GameManager_OnScoreChanged(object sender, int score)
    {
        m_scoreText.text = score.ToString();
    }

}

