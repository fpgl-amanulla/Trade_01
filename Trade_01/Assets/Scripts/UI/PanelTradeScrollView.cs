using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PanelTradeScrollView : MonoBehaviour
{
    public Player player;
    public Trader trader;

    public Transform content;
    public TradeItem tradeItemPrefab;

    [Space(10)]
    public Text txtStartInstruction;

    private List<GameObject> tradeItem = new List<GameObject>();
    public void Start()
    {
        txtStartInstruction.gameObject.SetActive(true);
        txtStartInstruction.rectTransform.DOScale(Vector3.one * 1.2f, 1.0f).SetLoops(-1, LoopType.Yoyo);

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
        if (player.givenTradeItems.Count < player.tradeItemTransform.Count - 1)
        {
            for (int i = 0; i < tradeItem.Count; i++)
            {
                if (player.tradeItems[i].numOfItem > 0)
                    tradeItem[i].GetComponent<Button>().interactable = status;
            }
        }
        player.PlayerButtonInteractiveStatus(status);
    }
}
