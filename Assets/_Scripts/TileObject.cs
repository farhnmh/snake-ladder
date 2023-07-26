using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TileObject : MonoBehaviour
{
    [Header("Identity Attribute")]
    public int tileId;
    public int lineId;
    public GameObject fogObject;
    public bool maxLineTileId;

    [Header("Prompt Attribute")]
    public bool isPrompt;
    public int tileEventIndex;
    public List<UnityEvent> tileEvents;

    public void SetupFogObject(bool cond)
    {
        fogObject.SetActive(cond);
    }
}
