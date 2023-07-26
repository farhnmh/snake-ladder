using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class DiceDetail
{
    public int point;
    public Sprite diceSprite;
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Panel Attribute")]
    public GameObject endingPanel;
    public GameObject multipleChoicePanel;
    public GameObject narrativePanel;

    [Header("Tiles Attribute")]
    public TileObject currTileObject;
    public List<TileObject> tileObjects;
    public List<Transform> nextTiles;

    [Header("Dice Attribute")]
    public float randomDelay;
    public Button diceButton;
    public DiceDetail currDiceDetail;
    public List<DiceDetail> diceDetailList;

    [Header("Debug Attribute")]
    public int maxTargetTile;
    public int lastDicePoint;
    public int healthPoint = 2;
    public bool isGameOver;

    private void Awake()
    {
        instance = this;
        tileObjects = FindObjectsOfType<TileObject>().ToList();
        tileObjects.Sort((t1, t2) => t1.tileId.CompareTo(t2.tileId));
    }

    public void RollDiceBtn()
    {
        StartCoroutine(RollDice());
    }

    public IEnumerator RollDice()
    {
        diceButton.interactable = false;
        for (int i = 0; i < UnityEngine.Random.Range(10, 15); i++) 
        {
            currDiceDetail = diceDetailList[UnityEngine.Random.Range(0, diceDetailList.Count)];
            diceButton.GetComponent<Image>().sprite = currDiceDetail.diceSprite;
            yield return new WaitForSeconds(randomDelay);
        }

        lastDicePoint = currDiceDetail.point;
        maxTargetTile = currTileObject.tileId + lastDicePoint;
        if (maxTargetTile >= tileObjects.Count)
        {
            maxTargetTile = tileObjects.Count - 1;
            isGameOver = true;
        }

        for (int i = currTileObject.tileId; i <= maxTargetTile; i++)
        {
            nextTiles.Add(tileObjects[i].transform);
        }

        PionObject.instance.SetupTargets(nextTiles);
    }

    public void SetupFogInLine(int lineId, bool cond)
    {
        foreach (TileObject tile in tileObjects)
        {
            if (tile.lineId == lineId)
                tile.SetupFogObject(cond);
        }
    }
}
