using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class PionObject : MonoBehaviour
{
    public static PionObject instance;

    public float moveSpeed;
    public float minDistance;
    public List<Transform> targetMove;

    [Header("Attribute Checker")]
    public int currTileId;
    public int currLineId;
    public float currDistance;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (targetMove.Count == 0) return;
        Moving();
    }

    public int GetCurrentTile()
    {
        return currTileId;
    }

    public int GetCurrentLine()
    {
        return currLineId;
    }

    public void SetupTargets(List<Transform> target)
    {
        targetMove = target;
    }

    public void Moving()
    {
        if (targetMove.Count == 0) return;
        currDistance = Vector2.Distance(transform.position, targetMove[0].position);

        if (currDistance > minDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetMove[0].position, moveSpeed * Time.deltaTime);
        }
        else
        {
            TileObject currTile = targetMove[0].GetComponent<TileObject>();
            GameManager.instance.currTileObject = currTile;
            currTileId = currTile.tileId;
            currLineId = currTile.lineId;
            if (currTile.maxLineTileId)
            {
                GameManager.instance.SetupFogInLine(currTile.lineId + 1, false);
            }

            targetMove.RemoveAt(0);
            if (targetMove.Count == 0)
            {
                GameManager.instance.nextTiles.Clear();
                if (GameManager.instance.isGameOver)
                {
                    GameManager.instance.endingPanel.SetActive(true);
                }
                else
                {
                    GameManager.instance.diceButton.interactable = true;
                    if (GameManager.instance.currTileObject.isPrompt)
                    {

                    }
                }
            }
        }
    }
}
