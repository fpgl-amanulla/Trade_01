using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelTradeScrollView : MonoBehaviour
{
    public Player player;
    public Trader trader;

    public Transform content;
    public TradeItem tradeItemPrefab;


    private List<GameObject> tradeItem = new List<GameObject>();
    public void Start()
    {
        GenerateItem();
    }

    private void GenerateItem()
    {
        for (int i = 0; i < player.tradeItems.Count; i++)
        {
            TradeItem item = Instantiate(tradeItemPrefab, content);
            tradeItem.Add(item.gameObject);
            item.AssignValues(this, i);
        }
    }

    public void SetAllItemInteractableStatus(bool status)
    {
        for (int i = 0; i < tradeItem.Count; i++)
        {
            tradeItem[i].GetComponent<Button>().interactable = status;
        }
    }
}
