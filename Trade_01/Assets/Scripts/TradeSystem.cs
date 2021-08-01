using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TradeSystem : MonoBehaviour
{
    public List<TradeItemSO> tradeItems = new List<TradeItemSO>();

    [Space(10)]
    public List<Transform> tradeItemTransform = new List<Transform>();

    [HideInInspector]
    public List<TradeItemSO> givenTradeItems = new List<TradeItemSO>();
    public List<GameObject> givenTradeItemsGameobject = new List<GameObject>();

    public void InstantiateItem(int tradeItemIndex)
    {
        GameObject tradeItemPrefab = tradeItems[tradeItemIndex].itemPrefab;
        Transform insTransform = tradeItemTransform[givenTradeItems.Count];
        GameObject item = Instantiate(tradeItemPrefab, insTransform.position, insTransform.rotation, insTransform);
        givenTradeItemsGameobject.Add(item);
        givenTradeItems.Add(tradeItems[tradeItemIndex]);
    }
    public void InstantiateItem(TradeItemSO tradeItem)
    {
        Transform insTransform = tradeItemTransform[givenTradeItems.Count];
        GameObject item = Instantiate(tradeItem.itemPrefab, insTransform.position, insTransform.rotation, insTransform);
        givenTradeItemsGameobject.Add(item);
        givenTradeItems.Add(tradeItem);
    }

    public int GetItemValue()
    {
        int value = 0;
        for (int i = 0; i < givenTradeItems.Count; i++)
        {
            value += givenTradeItems[i].starValue;
        }
        return value;
    }

}
